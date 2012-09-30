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
using MCAdmin.Utility;
using MCAdmin.WebAccess;

namespace MCAdmin
{
    public static class Program
    {
        public delegate void SingleObjectEvent<T>(T obj);
        public static event SingleObjectEvent<string> MinecraftServerMessageRaised;

        private static ProcessStartInfo procStartInfo = new ProcessStartInfo("java", "-jar server.jar nogui");
        private static Process proc = new Process();
        private static bool fileDownloaded = File.Exists("server.jar");
        private static object lockObj = new object();

        private static void Main(string[] args)
        {
            Console.WindowWidth = 100;
            Console.WindowHeight = 40;
            try
            {
                if (!File.Exists("server.jar"))
                {
                    WebClient cl = new WebClient();
                    cl.DownloadProgressChanged += cl_DownloadProgressChanged;
                    cl.DownloadFileCompleted += cl_DownloadFileCompleted;
                    cl.DownloadFileAsync(new Uri("https://s3.amazonaws.com/MinecraftDownload/launcher/minecraft_server.jar"), "server.jar");
                    while (!fileDownloaded) Thread.Sleep(100);
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
                proc.ErrorDataReceived += proc_DataReceived;
                proc.OutputDataReceived += proc_DataReceived;
                proc.Start();
                Webserver.Start();
                new Thread(HandleCommand).Start();
                while (!proc.HasExited) ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void proc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            if (MinecraftServerMessageRaised != null)
            {
                MinecraftServerMessageRaised.Invoke(e.Data);
            }
        }

        private static void cl_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Thread.Sleep(3000);
            fileDownloaded = true;
            Console.Clear();
        }

        private static void cl_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            lock (lockObj)
            {
                Console.Clear();
                Console.WriteLine(("Download Progress").Wrap("-", Console.WindowWidth));
                Console.WriteLine(("=").Repeat(e.ProgressPercentage - 1) + ">");
                Console.WriteLine("{0}%", e.ProgressPercentage);
                Console.WriteLine(("-").Repeat(Console.WindowWidth));
            }
        }
        private static void HandleCommand()
        {
            while (!proc.HasExited)
            {

            }
        }
    }
}
