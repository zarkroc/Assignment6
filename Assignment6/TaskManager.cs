using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Author Tomas Perers
/// Date: 2017-12-08
/// </summary>
namespace Assignment6
{
    class TaskManager
    {
        private List<Task> taskList;

        public TaskManager()
        {
            taskList = new List<Task>();
        }

        public int Count => taskList.Count;

        public void AddTask(Task task)
        {
            taskList.Add(task);
        }

        public bool RemoveTask(int index)
        {
            bool returnValue = false;
            if (index > -1 && index < taskList.Count)
            {
                taskList.Remove(GetTaskAtPosition(index));
                returnValue = true;
            }
                return returnValue;
        }

        public Task GetTaskAtPosition(int index)
        {
            if (index > -1 && index < taskList.Count)
            {
                return taskList[index];
            }
            else
                return null;
        }

        public bool ReplaceTask(Task task, int index)
        {
            bool returnValue = false;
            if (index > -1 && index < taskList.Count)
            {
                taskList[index] = task;
                returnValue = true;
            }
            return returnValue;
        }
    }
}
