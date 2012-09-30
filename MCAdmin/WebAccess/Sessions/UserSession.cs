//    Application: MCAdmin
//    File:        UserSession.cs
//    Copyright:   Copyright (C) 2012 Christian Wilson. All rights reserved.
//    Website:     https://github.com/seaboy1234/
//    Description: A Minecraft Server Wrapper
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpServer;
using HttpServer.Sessions;

namespace MCAdmin.WebAccess.Sessions
{
    [Serializable]
    public class UserSession : MCAdminSession
    {
        public UserSession(Server server, bool autoCreate) : base(server, autoCreate, typeof(UserSession))
        {
        }

        /// <summary>
        /// Gets the current active session.
        /// </summary>
        public static UserSession CurrentSession
        {
            get
            {
                UserSession session = _sessionProvider.CurrentSession as UserSession;
                return session;
            }
        }
        /// <summary>
        /// Gets or sets the username of the client.
        /// </summary>
        public string Username
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the User Id of the client.
        /// </summary>
        public int UserId
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new UserSession.
        /// </summary>
        /// <param name="server">The server holding the session</param>
        /// <param name="userId">The Id of the client</param>
        /// <param name="username">TEMPOARY: The username of the client</param>
        /// <returns></returns>
        public static UserSession Create(Server server, int userId, string username)
        {
            //TODO: Get user information from database
            UserSession session = _sessionProvider.Create(typeof(UserSession)) as UserSession;
            if (session == null)
            {
                throw new Exception("Session is null!  Check your code!");
            }
            session.UserId = userId;
            session.Username = username;
            return session;
        }

        public static void Init(Server server, bool autoCreate)
        {
            new UserSession(server, autoCreate);
        }
    }
}
