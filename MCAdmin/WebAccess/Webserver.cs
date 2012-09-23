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
using HttpServer;
using HttpServer.Routing;
using MCAdmin.WebAccess.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using MCAdmin.Extensions;
using HttpListener = HttpServer.HttpListener;

namespace MCAdmin.WebAccess
{
    internal class Webserver
    {
        private static object _lockobj = new object();
        public static void Start()
        {
            Server s = new Server();
            s.Add(HttpListener.Create(IPAddress.Any, 8080));
            s.PrepareRequest += OnRequest;
            s.Add(new StaticResourceHandler());
            s.Add(new PageHandler());
            s.Add(new SimpleRouter("/", "index.htm"));
            s.Start(0);
        }

        private static void OnRequest(object sender, RequestEventArgs e)
        {
            lock (_lockobj)
            {
                Console.WriteLine(("Begin Request").Wrap("-", Console.WindowWidth));
                Console.WriteLine(String.Format("Request from {0}", e.Context.RemoteEndPoint.Address.ToString()));
                if (e.Response.Status == HttpStatusCode.OK)
                {
                    Console.WriteLine(String.Format("The request for {0} was successful", e.Request.Uri.ToString()));
                }
                else
                {
                    Console.WriteLine(String.Format("The request for {0} failed", e.Request.Uri.ToString()));
                }
                Console.WriteLine(("End Request").Wrap("-", Console.WindowWidth));
            }
        }
    }
}
