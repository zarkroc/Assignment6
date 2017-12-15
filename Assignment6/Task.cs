using System;

/// <summary>
/// Author: Tomas Perers
/// Date: 2017-12-08
/// </summary>

namespace SmallToDoApp
{
    /// <summary>
    /// Representation of a Task.
    /// </summary>
    [Serializable]
    public class Task
    {
        private string description;
        private PriorityLevel priority;
        private DateTime date;

        /// <summary>
        /// Initialize a new task with default values.
        /// </summary>
        public Task()
        {
            this.description = String.Empty;
            this.priority = PriorityLevel.Low;
            this.date = DateTime.Now;
        }

        /// <summary>
        /// Initialize a task with specefied values.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="priority"></param>
        /// <param name="date"></param>
        public Task(string description, PriorityLevel priority, DateTime date)
        {
            this.Description = description;
            this.Priority = priority;
            this.Date = date;
        }

        public DateTime Date { get => date; set => date = value; }
        public string Description { get => description; set => description = value; }
        public PriorityLevel Priority { get => priority; set => priority = value; }

        /// <summary>
        /// Overrides the ToString and returns a formattaded string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0,-15} {1,-10} {2}", date.ToString("yyyy-MM-dd"), priority, description);
        }
    }
}
