//    Application: MCAdmin
//    File:        Page.cs
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
using MCAdmin.WebAccess.Pages.Template;
using MCAdmin.WebAccess.Sessions;

namespace MCAdmin.WebAccess.Pages
{
    public abstract class Page
    {
        public abstract string Path { get; }

        protected abstract string Render();

        public virtual string Template { get { return "Data/templates/" + Path; } set { } }

        protected virtual string ApplyTemplate(RequestContext request, string template)
        {
            return template;
        }

        public string PageName { get; protected set; }
        protected Dictionary<string, IDynamicTemplate> templates = new Dictionary<string, IDynamicTemplate>();

        public Page()
        {
            foreach (Type t in from t in Assembly.GetExecutingAssembly().GetTypes()
                               where
                                   !t.IsAbstract && t.IsClass && t.GetInterfaces().Contains(typeof(IDynamicTemplate))
                               select t)
            {
                IDynamicTemplate item = (IDynamicTemplate)t.GetConstructor(new Type[] { }).Invoke(null);
                item.CurrentPage = this;
                templates.Add(item.Name, item);
            }
        }




        public string RenderPage(RequestContext request)
        {
            if (File.Exists(Template))
            {
                string page = ApplyTemplate(request, File.ReadAllText(Template));
                //Global tags
                //Do page parts first.
                Scripting.ApplyTag("section:Header", File.ReadAllText("Data/templates/section.header.html"), ref page);
                Scripting.ApplyTag("section:Footer", File.ReadAllText("Data/templates/section.footer.html"), ref page);
                //Then persistance objects.
                foreach (var v in Persistance.API)
                {
                    Scripting.ApplyTag("persistance:" + v.Key, v.Value.ToString(), ref page);
                }
                //User tags.
                Scripting.ApplyTag("user:Id", UserSession.CurrentSession.UserId.ToString(), ref page);
                Scripting.ApplyTag("user:Name", UserSession.CurrentSession.Username, ref page);
                Scripting.ApplyTag("session:Id", UserSession.CurrentSession.SessionId.ToString(), ref page);
                //Then the nav.
                Scripting.ApplyTag("nav", ApplyNav(), ref page);
                //End
                
                return page;
            }
            else
            {
                return Render();
            }
        }

        protected string ApplyNav()
        {
            IDynamicTemplate t = templates["nav"];
            string nav = "";
            foreach (Page p in PageHandler.Pages)
            {
                if (p.PageName != "")
                {

                }
                nav += t.ApplyTemplate(p, p.PageName);
            }
            return nav;
        }
        
    }
}
