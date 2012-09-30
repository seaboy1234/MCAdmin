//    Application: MCAdmin
//    File:        StaticResourceHandler.cs
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
using System.IO;
using HttpServer;
using HttpServer.Headers;
using HttpServer.Modules;

namespace MCAdmin.WebAccess
{
    internal class StaticResourceHandler : IModule
    {
        public ProcessingResult Process(RequestContext context)
        {
            // Init our vars.
            IRequest request = context.Request;
            IResponse response = context.Response;
            // Get the page.
            byte[] resource;
            string uri = request.Uri.AbsolutePath.Remove(0, 1);
            // Check if it exists.
            if (!File.Exists("Data/static/" + uri))
            {
                return ProcessingResult.Continue;
            }
            resource = File.ReadAllBytes("Data/static/" + uri);
            response.Add(new StringHeader("Cache-Control", "max-age=28800"));
            response.Add(new StringHeader("X-Content-Class", "Static"));
            response.Body.Write(resource, 0, resource.Length);
            response.ContentType = new ContentTypeHeader(MIMEAssistant.GetMIMEType("Data/static/" + uri));
            return ProcessingResult.SendResponse;
        }
    }
}
