using System;
using System.IO;
using System.Windows;
/// <summary>
/// Author: Tomas Perers
/// Date: 2017-12-12
/// </summary>
namespace SmallToDoApp
{
    /// <summary>
    /// File handler class to handle saving and loading of TaskManager object.
    /// </summary>
    public class FileHandler
    {
        /// <summary>
        /// Save TaskManager object to file.
        /// </summary>
        /// <param name="filePath">File path to save object in</param>
        /// <param name="taskManager">TaskManager object to save.</param>
        public void SaveFile(string filePath, TaskManager taskManager)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, taskManager);
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Failed to save to file\n" + ex);
            }
        }

        /// <summary>
        /// Read TaskManager object from file.
        /// </summary>
        /// <param name="filePath">File path to load object from</param>
        /// <param name="taskManager">Object to store read object in</param>
        /// <returns>TaskManager</returns>
        public TaskManager LoadFile(string filePath, TaskManager taskManager)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    taskManager = (TaskManager)binaryFormatter.Deserialize(stream);
                    return taskManager;
                }
            } catch (Exception ex) {
                MessageBox.Show("Failed to load file\n" +ex);
            }
            return taskManager;
        }
    }
}
