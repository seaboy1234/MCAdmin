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
