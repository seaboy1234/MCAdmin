//    Application: MCAdmin
//    File:        MinecraftServer.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MCAdmin
{
    //TODO: Fully implement this class.
    internal class MinecraftServer : IMinecraftServer
    {
        private List<string> _players;
        private Process _server;
        private ProcessStartInfo _serverStartInfo;
        private Dictionary<string, string> _config;
        private Hashtable _eventManager;

#region IMinecraftServer
        event Program.SingleObjectEvent<string> IMinecraftServer.PlayerJoin
        {
            add
            {
                _eventManager["PlayerJoin"] = (_eventManager["PlayerJoin"] as Program.SingleObjectEvent<string>) + value;
            }
            remove
            {
                _eventManager["PlayerJoin"] = (_eventManager["PlayerJoin"] as Program.SingleObjectEvent<string>) - value;
            }
        }
        event Program.SingleObjectEvent<string> IMinecraftServer.PlayerLeave
        {
            add
            {
                _eventManager["PlayerLeave"] = (_eventManager["PlayerLeave"] as Program.SingleObjectEvent<string>) + value;
            }
            remove
            {
                _eventManager["PlayerLeave"] = (_eventManager["PlayerLeave"] as Program.SingleObjectEvent<string>) - value;
            }
        }
        event Program.SingleObjectEvent<string> IMinecraftServer.PlayerChat
        {
            add
            {
                _eventManager["PlayerChat"] = (_eventManager["PlayerChat"] as Program.SingleObjectEvent<string>) + value;
            }
            remove
            {
                _eventManager["PlayerChat"] = (_eventManager["PlayerChat"] as Program.SingleObjectEvent<string>) - value;
            }
        }

        string[] IMinecraftServer.Players
        {
            get
            {
                return _players.ToArray();
            }
        }

        void IMinecraftServer.Start()
        {
            
        }

        void IMinecraftServer.Stop()
        {
            
        }
#endregion

       
    }
}
