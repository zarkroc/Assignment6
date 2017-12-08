using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Author: Tomas Perers
/// Date: 2017-12-08
/// </summary>

namespace Assignment6
{
    class Task
    {
        private string description;
        private PriorityLevel priority;
        private DateTime date;

        public Task(string description, PriorityLevel priority, DateTime date)
        {
            this.Description = description;
            this.Priority = priority;
            this.Date = date;
        }

        public DateTime Date { get => date; set => date = value; }
        public string Description { get => description; set => description = value; }
        public PriorityLevel Priority { get => priority; set => priority = value; }

        public override string ToString()
        {
            
            string output = String.Format("{0,10} {1,10} {2}", date, priority, description);
            
            return output;
        }
    }
}
