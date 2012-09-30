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
