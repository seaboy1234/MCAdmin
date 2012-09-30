using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpServer;
using HttpServer.Sessions;

namespace MCAdmin.WebAccess.Sessions
{
    public abstract class MCAdminSession
    {
        protected static MCAdminSessionProvider _sessionProvider;

        protected Guid _sessId;

        public MCAdminSession(Server server, bool autoCreate, Type type)
        {
            if (_sessionProvider == null)
            {
                _sessionProvider = new MCAdminSessionProvider(server, autoCreate, type);
            }
            else if (_sessionProvider.Server != server)
            {
                throw new InvalidOperationException("Server differs from that of the Session Provider.");
            }
            Server = server;
            _sessId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the server of the client.
        /// </summary>
        public Server Server
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Session Id of the client.
        /// </summary>
        public Guid SessionId
        {
            get
            {
                return _sessId;
            }
        }
    }
}
