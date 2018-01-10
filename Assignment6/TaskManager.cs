using System;
using System.Collections.Generic;

/// <summary>
/// Author Tomas Perers
/// Date: 2017-12-08
/// </summary>
namespace SmallToDoApp
{
    /// <summary>
    /// TaskManger class. Holds a list of all tasks that should be done.
    /// </summary>
    [Serializable]
    public class TaskManager
    {
        private List<Task> taskList;
        /// <summary>
        /// Creats a new taskList and initialize the object.
        /// </summary>
        public TaskManager()
        {
            taskList = new List<Task>();
        }

        /// <summary>
        /// How many tasks do we have in the list?
        /// </summary>
        public int Count => taskList.Count;

        /// <summary>
        /// Add a task to the list.
        /// </summary>
        /// <param name="task">Task to add</param>
        public void AddTask(Task task)
        {
            taskList.Add(task);
        }

        /// <summary>
        /// Removes a task from the list.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool RemoveTask(int index)
        {
            if (ValidateIndex(index))
            {
                taskList.Remove(GetTaskAtPosition(index));
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Retuns the Taks at specefied index.
        /// </summary>
        /// <param name="index">Index in taskList</param>
        /// <returns>Task</returns>
        public Task GetTaskAtPosition(int index)
        {
            
            if (ValidateIndex(index))
            {
                Task tmpTask = new Task(taskList[index]);
                return tmpTask;
            }
            else
                return null;
        }

        /// <summary>
        /// Validates that the index in within the list.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>boolean true or false</returns>
        public bool ValidateIndex(int index)
        {
            if (index > -1 && index < taskList.Count)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Replaces task at index.
        /// </summary>
        /// <param name="task">Task</param>
        /// <param name="index">int</param>
        /// <returns>boolean</returns>
        public bool ReplaceTask(Task task, int index)
        {
            if (ValidateIndex(index))
            {
                taskList[index] = task;
                return true;
            }
            else
                return false;
        }

    }
}
