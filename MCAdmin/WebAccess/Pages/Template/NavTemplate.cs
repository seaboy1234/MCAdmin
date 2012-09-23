//    Application: MCAdmin
//    File:        NavTemplate.cs
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

namespace MCAdmin.WebAccess.Pages.Template
{
    class NavTemplate : IDynamicTemplate
    {
        public string Name
        {
            get { return "nav"; }
        }
        public Page CurrentPage { get; set; }

        public string ApplyTemplate(Page page, string item)
        {
            bool currentPage = false;
            string output;
            if (item == CurrentPage.PageName)
            {
                currentPage = true;
            }
            if (currentPage)
            {
                output = "<li class=\"current_page_item\"><a href=\"" + page.Path + "\">" + item + "</a></li>";
            }
            else
            {
               output = "<li><a href=\"" + page.Path + "\">" + item + "</a></li>";
            }
            return output;
        }
    }
}
