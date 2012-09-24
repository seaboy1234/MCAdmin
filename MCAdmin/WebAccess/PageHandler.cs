//    Application: MCAdmin
//    File:        PageHandler.cs
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
using System.IO;
using System.Linq;
using System.Reflection;
using HttpServer;
using HttpServer.Modules;
using MCAdmin.WebAccess.Pages;

namespace MCAdmin.WebAccess
{
    internal class PageHandler : IModule
    {
        
        private static PageHandler _instance;
        private Dictionary<string, Page> _pages = new Dictionary<string, Page>();
        public PageHandler()
        {
            if (_instance == null) _instance = this;
            foreach (Type t in from t in Assembly.GetExecutingAssembly().GetTypes()
                               where
                                   t.BaseType == typeof(Page) && !t.IsAbstract
                               select t)
            {
                Page p = (Page)t.GetConstructor(new Type[] { }).Invoke(null);
                _pages.Add(p.Path, p);
            }
        }

        /// <summary>
        /// Gets an array of all pages.
        /// </summary>
        public static Page[] Pages 
        { 
            get 
            { 
                return _instance._pages.Values.ToArray(); 
            } 
        }

        public ProcessingResult Process(RequestContext context)
        {
            // Init our vars.
            IRequest request = context.Request;
            IResponse response = context.Response;
            StreamWriter sw = new StreamWriter(response.Body);
            sw.AutoFlush = true;
            // Get the page.
            Page page;
            string uri = request.Uri.AbsolutePath.Remove(0, 1);
            // Check if it exists.
            if (!_pages.ContainsKey(uri))
            {
                return ProcessingResult.Continue;
            }
            // Handle content.
            page = _pages[uri];
            if ((request.Method == Method.Post
                || (request.Method == Method.Get && request.QueryString.Count > 0))
                && page is IPostHandler)
            {
                sw.WriteLine(((IPostHandler)page).Post(request.Parameters));
            }
            else
            {
                sw.Write(page.RenderPage(context));
            }
            return ProcessingResult.SendResponse;
        }
    }
}
