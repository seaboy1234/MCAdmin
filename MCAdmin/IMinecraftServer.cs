//    Application: MCAdmin
//    File:        IMinecraftServer.cs
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

namespace MCAdmin
{
    public interface IMinecraftServer
    {
        /// <summary>
        /// Raised when a player joins.
        /// </summary>
        event Program.SingleObjectEvent<string> PlayerJoin;
        /// <summary>
        /// Raised when a player leaves.
        /// </summary>
        event Program.SingleObjectEvent<string> PlayerLeave;
        /// <summary>
        /// Raised when a player sends a chat message.
        /// </summary>
        event Program.SingleObjectEvent<string> PlayerChat;

        /// <summary>
        /// Gets a list of players currently on the server
        /// </summary>
        string[] Players
        {
            get;
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
        void Start();
        /// <summary>
        /// Stops the server.
        /// </summary>
        void Stop();
        
    }
}
