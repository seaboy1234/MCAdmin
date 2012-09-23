//    Application: MCAdmin
//    File:        Program.cs
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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using MCAdmin.Extensions;
using MCAdmin.WebAccess;

namespace MCAdmin
{
    internal static class Program
    {
        private static ProcessStartInfo procStartInfo = new ProcessStartInfo("java", "-Xmx1024M -Xms1024M -jar minecraft_server.jar nogui");
        private static Process proc = new Process();
        private static bool fileDownloaded = File.Exists("minecraft_server.jar");

        private static void Main(string[] args)
        {
            try
            {
                if (!File.Exists("minecraft_server.jar"))
                {
                    WebClient cl = new WebClient();
                    cl.DownloadProgressChanged += cl_DownloadProgressChanged;
                    cl.DownloadFileCompleted += cl_DownloadFileCompleted;
                    cl.DownloadFileAsync(new Uri("https://s3.amazonaws.com/MinecraftDownload/launcher/minecraft_server.jar"), "minecraft_server.jar");
                    
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                return;
            }
            try
            {
                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = false;
                proc.StartInfo = procStartInfo;
                procStartInfo.WorkingDirectory = Environment.CurrentDirectory + "Minecraft";
                proc.Start();
                Webserver.Start();
                new Thread(HandleCommand).Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    Console.WriteLine(proc.StandardOutput.ReadLine());
                    //Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void cl_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        private static void cl_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine(("Download Progress").Wrap("-", Console.WindowWidth));
        }
        private static void HandleCommand()
        {
            while (!proc.HasExited)
            {

            }
        }
    }
}
