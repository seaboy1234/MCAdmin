//    Application: MCAdmin
//    File:        StringExtionsions.cs
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

namespace MCAdmin.Extensions
{
    public static class StringExtionsions
    {
        public static string Repeat(this string str, int num)
        {
            string output = "";
            for (int i = 1; i < num; i++)
            {
                output += str;
            }
            return output;
        }
        /// <summary>
        /// Wraps str2 around the string.
        /// </summary>
        /// <param name="str2">The wrapper string.</param>
        /// <param name="num1">how many times to repeat the wapper.</param>
        /// <returns></returns>
        public static string Wrap(this string str1, string str2, int num1)
        {
            int num2, num3;
            string output = "";
            num3 = num1 / 2;
            num2 = (num1 / 2) - str1.Length;
            for (int i = 0; i < num2; i++)
            {
                output += str2;
            }
            output += str1;
            for (int i = 0; i < num3; i++)
            {
                output += str2;
            }
            return output;
        }
    }
}
