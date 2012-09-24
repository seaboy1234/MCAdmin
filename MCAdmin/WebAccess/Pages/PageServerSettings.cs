//    Application: MCAdmin
//    File:        PageServerSettings.cs
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
using System.Text;
using HttpServer;

namespace MCAdmin.WebAccess.Pages
{
    internal class PageServerSettings : Page
    {
        public PageServerSettings()
        {
            PageName = "Server Settings";
        }

        public override string Path
        {
            get { return "settings.htm"; }
        }

        protected override string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<div id='page'>");
            sb.AppendLine("<form action='process.htm' method='POST'>");
            sb.AppendLine("Key: <input type='text' name='var' /><br />");
            sb.AppendLine("Value: <input type='text' name='value' /> <br />");
            sb.AppendLine("<input type='submit' value='Submit' />");
            sb.AppendLine("<input type='hidden' name='page' value='" + Path + "' />");
            sb.AppendLine("</div>");
            return sb.ToString();
        }
        protected override string ApplyTemplate(RequestContext request, string template)
        {
            return template;
        }

    }
}
