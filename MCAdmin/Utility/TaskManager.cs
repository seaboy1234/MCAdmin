//    Application: MCAdmin
//    File:        TaskManager.cs
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using MCAdmin.Extensions;

namespace MCAdmin.Utility
{
    /// <summary>
    /// Manages tasks within the program.
    /// </summary>
    public class TaskManager
    {
        private  List<TaskThread> _tasks;
        private Thread _taskThread;
        private object _taskLock;
        private int _tickRate;

        private Stopwatch _tickRecorder;

        public TaskManager()
        {
            _tasks = new List<TaskThread>();
            _taskThread = new Thread(new ThreadStart(Tick));
            _taskThread.IsBackground = true;
            _taskLock = new object();
            _tickRecorder = new Stopwatch();
            _tickRecorder.Start();
            _taskThread.Start();
            
        }

        /// <summary>
        /// Adds a new task object to the queue.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="delay">The amount of time in milliseconds to wait before executing the task.</param>
        public void Add(TaskObject task, int delay = 0)
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }
            lock(_taskLock)
            {
                _tasks.Add(new TaskThread(task, delay));
            }
        }

        private void Tick()
        {
            int ticks = 0;
            while (true)
            {
                if (ticks == 20)
                {
                    _tickRecorder.Stop();
                    _tickRate = (int)_tickRecorder.ElapsedMilliseconds; // Must cast directly to an int.
                    _tickRecorder.Reset();
                    _tickRecorder.Start();
                    ticks = 0;
                    Console.WriteLine("Tick avg at " + (double)(_tickRate / 20));
                }
                lock (_taskLock)
                {
                    foreach (TaskThread t in _tasks.ToArray())
                    {
                        if (t.Delay >= 20)
                        {
                            t.Delay -= 20;
                        }
                        else
                        {
                            t.Invoke();
                            _tasks.Remove(t);
                        }
                    }
                }
                ticks++;
            }
        }
    }
}
