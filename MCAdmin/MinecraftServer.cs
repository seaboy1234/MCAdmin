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
