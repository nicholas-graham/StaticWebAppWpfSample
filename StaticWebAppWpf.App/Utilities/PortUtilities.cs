using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.Utilities
{
    public static class PortUtilities
    {
        public const int DevPort = 60000;
        private static readonly IPEndPoint _defaultLoopbackEndpoint = new IPEndPoint(IPAddress.Loopback, port: 0);

        /// <summary>
        /// Retrieves a dynamic available port for serving static files, or provided the dev port if we are in active development.
        /// </summary>
        /// <param name="args">The startup arguments for the application</param>
        /// <returns>The integer port value.</returns>
        /// <exception cref="NullReferenceException">Thrown if there is an error retrieving the port.</exception>
        public static int GetPort(string[] args)
        {
            if (args.Contains("-dev"))
                return DevPort;

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(_defaultLoopbackEndpoint);
                return ((IPEndPoint?)socket.LocalEndPoint)?.Port ??
                    throw new NullReferenceException("Could not get dynamically assigned port");
            }
        }
    }
}
