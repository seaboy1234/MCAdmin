using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpServer;
using HttpServer.Headers;
using HttpServer.Messages;
using HttpServer.Sessions;

namespace MCAdmin.WebAccess.Sessions
{
    /// <summary>
    /// Replacement for <see cref="HttpServer.Sessions.SessionProvider"/>.
    /// </summary>
    public class MCAdminSessionProvider
    {
        private readonly Server _server;
        private List<MCAdminSession> _sessions;
        private bool _autoCreate;
        private Type _sessionType;

        public MCAdminSessionProvider(Server server, bool autoCreate, Type sessionType)
        {
            while (sessionType.BaseType != typeof(MCAdminSession))
            {
                if (sessionType.BaseType == typeof(object)) //Avoid infinite loops.
                {
                    throw new InvalidOperationException("Argument \"sessionType\" must contain the base type MCAdmin.WebAccess.Sessions.MCAdminSession.");
                }
            }

            _server = server;
            _sessions = new List<MCAdminSession>();
            _autoCreate = autoCreate;
            _sessionType = sessionType;

            server.PrepareRequest += OnRequest;
        }

        /// <summary>
        /// Gets the Server this session provider is attached to.
        /// </summary>
        public Server Server
        {
            get
            {
                return _server;
            }
        }
        /// <summary>
        /// Gets the current session.
        /// </summary>
        public MCAdminSession CurrentSession
        {
            get;
            private set;
        }

        /// <summary>
        /// Stores a session in memory.
        /// </summary>
        /// <param name="session"></param>
        public void StoreSession(MCAdminSession session)
        {
            _sessions.Add(session);
        }

        /// <summary>
        /// Creates a new session using the type provided.
        /// </summary>
        /// <param name="session"></param>
        /// <exception cref="System.InvalidOperationException" />
        /// <exception cref="System.Exception" />
        /// <returns></returns>
        public MCAdminSession Create(Type session)
        {
            if(session == null)
            {
                throw new ArgumentNullException("session");
            }
            //Make sure the type is an MCAdminSession.
            //The reason we do this is because a class might implement a class that implements 
            //the MCAdminSession type.
            while(session.BaseType != typeof(MCAdminSession))
            {
                if (session.BaseType == typeof(object)) //Avoid infinite loops.
                {
                    throw new InvalidOperationException("Argument \"session\" must contain the base type MCAdmin.WebAccess.Sessions.MCAdminSession.");
                }
            }
            MCAdminSession sessionObj = session.GetConstructor(new Type[] { typeof(Server), typeof(bool) }).Invoke(new object[] { _server, _autoCreate }) as MCAdminSession;
            if (sessionObj == null)
            {
                throw new Exception("Unable to create session object!  Check your code.");
            }
            StoreSession(sessionObj);
            return sessionObj;
        }

        private void OnRequest(object sender, RequestEventArgs e)
        {
            if (e.Request.Cookies["seSession_" + _sessionType.Name] != null)
            {
                CurrentSession = (from s in _sessions.ToArray()
                                  where
                                      s.SessionId.ToString() == e.Request.Cookies["csSession"].Value
                                  select s).FirstOrDefault();
            }
            else if (_autoCreate)
            {
                CurrentSession = Create(_sessionType);
                e.Response.Cookies.Add(new ResponseCookie("csSession_" + _sessionType.Name, CurrentSession.SessionId.ToString(), DateTime.Now.AddHours(8)));
            }
        }
    }
}
