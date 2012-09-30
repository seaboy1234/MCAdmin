//    Application: MCAdmin
//    File:        Webserver.cs
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
using System.Net;
using HttpServer;
using HttpServer.Routing;
using MCAdmin.Extensions;
using MCAdmin.WebAccess.Sessions;
using HttpListener = HttpServer.HttpListener;

namespace MCAdmin.WebAccess
{
    internal class Webserver
    {
        private static object _lockObj = new object();
        private static Server _webServer;

        public static Server Server
        {
            get
            {
                return _webServer;
            }
        }

        public static void Start()
        {
            if (_webServer != null)
            {
                _webServer.Start(0);
                return;
            }
            _webServer = new Server();
            _webServer.Add(HttpListener.Create(IPAddress.Any, 8080));
            _webServer.PrepareRequest += OnRequest;
            _webServer.Add(new StaticResourceHandler());
            _webServer.Add(new PageHandler());
            _webServer.Add(new SimpleRouter("/", "index.htm"));
            UserSession.Init(_webServer, true);
            _webServer.Start(0);
        }
        public static void Stop()
        {
            _webServer.Stop(false);
        }

        private static void OnRequest(object sender, RequestEventArgs e)
        {
#if DEBUG
            lock (_lockObj)
            {
                Console.WriteLine(("Begin Request").Wrap("-", Console.WindowWidth));
                Console.WriteLine(string.Format("Request from {0}", e.Context.RemoteEndPoint.Address.ToString()));
                if (e.Response.Status == HttpStatusCode.OK)
                {
                    Console.WriteLine(string.Format("The request for {0} was successful", e.Request.Uri.ToString()));
                }
                else
                {
                    Console.WriteLine(string.Format("The request for {0} failed", e.Request.Uri.ToString()));
                }
                Console.WriteLine(("End Request").Wrap("-", Console.WindowWidth));
            }
#endif
        }
    }
}
