//    Application: MCAdmin
//    File:        TaskThread.cs
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
using System.Threading;

namespace MCAdmin.Utility
{
    /// <summary>
    /// Represents a task object.
    /// </summary>
    public class TaskThread // Can't be a struct because members cannot be modified.
    {
        private TaskObject _task;

        /// <summary>
        /// The amount of time left before the task excutes.
        /// </summary>
        public int Delay
        {
            get;
            set;
        }

        public TaskThread(TaskObject task, int delay)
        {
            _task = task;
            Delay = delay;
        }

        /// <summary>
        /// Invokes the task on the current thread.
        /// </summary>
        public void Invoke()
        {
            _task.Invoke();
        }
    }
}
