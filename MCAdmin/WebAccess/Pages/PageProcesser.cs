//    Application: MCAdmin
//    File:        PageProcesser.cs
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
using System.Reflection;
using HttpServer;

namespace MCAdmin.WebAccess.Pages
{
    internal class PageProcesser : Page, IPostHandler
    {
        public override string Path
        {
            get { return "process.htm"; }
        }

        #region IPostHandler
        public string Post(IParameterCollection form)
        {
            if (form["page"] == "settings.htm")
            {
                if (!Persistance.API.ContainsKey(form["var"]))
                {
                    Persistance.API.Add(form["var"], form["value"]);
                }
                else
                {
                    Persistance.API[form["var"]] = form["value"];
                }
                return "Return to <a href='settings.htm'>settings.htm</a>";
            }
            return "";
        }
        #endregion

        protected override string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Please do not directly access this page.");
            return sb.ToString();
        }
    }
}
