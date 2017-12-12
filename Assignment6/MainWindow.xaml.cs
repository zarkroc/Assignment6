using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace SmallToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskManager taskManager;
        private Task task;
        private bool exitProram = false;

        public MainWindow()
        {
            InitializeComponent();
            taskManager = new TaskManager();

            UpdateGUI();
            cBoxPriority.Items.Add(PriorityLevel.High);
            cBoxPriority.Items.Add(PriorityLevel.Medium);
            cBoxPriority.Items.Add(PriorityLevel.Low);
        }
        private void InitializeGUI()
        {
            txtTodo.Text = "";
            lstToDo.Items.Clear();
            cBoxPriority.SelectedIndex = -1;
            dtpDateTimePicker.SelectedDate = null;
            dtpDateTimePicker.DisplayDate = DateTime.Today;
            lblCurrentTime.Content = DateTime.Now.ToString("HH:mm:ss");
        }
        private void UpdateGUI()
        {
            InitializeGUI();
            if (taskManager.Count > 0)
            {
                for (int i = 0; i < taskManager.Count; i++)
                {
                    lstToDo.Items.Add(taskManager.GetTaskAtPosition(i).ToString());

                }
            }
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            int index = lstToDo.SelectedIndex;
            task = new Task();
            if (index >= 0)
            {
                task.Description = txtTodo.Text;
                task.Priority = (PriorityLevel)cBoxPriority.SelectedValue;
                if (!ReadDate())
                    MessageBox.Show("You didn't select a date for the task");
                else if (!ReadToDo())
                    MessageBox.Show("You didn't write what ToDo");
                else if (!ReadPriority())
                    MessageBox.Show("You didn't set a priority level");
                else
                {
                    taskManager.ReplaceTask(task, index);
                    UpdateGUI();
                }
            }
            else
                MessageBox.Show("Nothing to change, no task selected");
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            task = new Task();
            if (!ReadDate())
                MessageBox.Show("You didn't select a date for the task");
            else if (!ReadToDo())
                MessageBox.Show("You didn't write what ToDo");
            else if (!ReadPriority())
                MessageBox.Show("You didn't set a priority level");
            else
            {
                taskManager.AddTask(task);
                UpdateGUI();
                task = new Task();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lstToDo.SelectedIndex;
            if (index >= 0)
            {
                taskManager.RemoveTask(index);
                UpdateGUI();
            }
            else
                MessageBox.Show("Nothing to delete, no task selected");
        }

        private void lstToDo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lstToDo.SelectedIndex;
            if (index >= 0)
            {
                Task temporaryTask = taskManager.GetTaskAtPosition(index);
                txtTodo.Text = temporaryTask.Description;
                cBoxPriority.SelectedItem = (PriorityLevel)temporaryTask.Priority;
                dtpDateTimePicker.SelectedDate = temporaryTask.Date;
            }
        }

        private bool ReadDate()
        {
            bool returnValue = false;
            if (dtpDateTimePicker.SelectedDate != null)
            {
                task.Date = (DateTime)dtpDateTimePicker.SelectedDate;
                returnValue = true;
            }
            return returnValue;
        }

        private bool ReadToDo()
        {
            if (String.IsNullOrEmpty(txtTodo.Text))
                return false;
            task.Description = txtTodo.Text;
            return true;
        }
        private bool ReadPriority()
        {
            if (cBoxPriority.SelectedIndex > -1)
            {
                task.Priority = (PriorityLevel)cBoxPriority.SelectedValue;
                return true;
            }
            else
                return false;
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            ExitProgramOrNot();
            if (exitProram)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void ExitProgramOrNot()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to exit program?",
                "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    exitProram = true;
                    break;

                case MessageBoxResult.No:
                    exitProram = false;
                    break;
            }
        }


        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_New_Click(object sender, RoutedEventArgs e)
        {
            InitializeGUI();
        }

        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                LoadFile(filename);
            }
        }

        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                SaveFile(filename);
            }
        }


        public void SaveFile(string filePath)
        {

            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, taskManager);
            }
        }


        public void LoadFile(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                taskManager = (TaskManager) binaryFormatter.Deserialize(stream);
                UpdateGUI();
            }

        }

    }
}