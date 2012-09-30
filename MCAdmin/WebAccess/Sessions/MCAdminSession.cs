//    Application: MCAdmin
//    File:        MCAdminSession.cs
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
