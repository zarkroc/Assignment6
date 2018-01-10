using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Reflection;
using System.Windows.Threading;

/// <summary>
/// Author: Tomas Perers
/// Date: 2017-12-08
/// </summary>
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
        private FileHandler fileHandler = new FileHandler();

        /// <summary>
        /// Initialize the window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            taskManager = new TaskManager();
            DispatcherTimer dispatcher = new DispatcherTimer();
            dispatcher.Tick += DispatcherSeconds_Tick;
            dispatcher.Interval = TimeSpan.FromSeconds(1);
            dispatcher.Start();
            UpdateGUI();
        }

        /// <summary>
        /// Resets the GUI to default settings.
        /// </summary>
        private void InitializeGUI()
        {
            cBoxPriority.Items.Clear();
            foreach (PriorityLevel priority in Enum.GetValues(typeof(PriorityLevel)))
            {
                cBoxPriority.Items.Add(priority);
            }
            txtTodo.Text = "";
            lstToDo.Items.Clear();
            cBoxPriority.SelectedIndex = -1;
            dtpDateTimePicker.SelectedDate = null;
            dtpDateTimePicker.DisplayDate = DateTime.Today;
            lblCurrentTime.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Update the GUI with new information from TaskManager.
        /// </summary>
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
            // Make sure that the buttons are only active if something is selected.
            if (lstToDo.SelectedIndex > -1)
            {
                btnChange.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnChange.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }

        /// <summary>
        /// Action to perform when the change button is clicked.
        /// Change the task at selected index from the listBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            int index = lstToDo.SelectedIndex;
            task = new Task();
            if (index >= 0)
            {
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

        /// <summary>
        /// Adds a new task, validates that the input is correct.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Deletes the selected task in the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm", MessageBoxButton.YesNo);
            if (result.ToString().ToLower() == "yes")
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
        }

        ///// <summary>
        ///// Updates the GUI with new information when selecting a new item in the listBox.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void lstToDo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lstToDo.SelectedIndex;
            if (index >= 0)
            {
                Task temporaryTask = taskManager.GetTaskAtPosition(index);
                txtTodo.Text = temporaryTask.Description;
                cBoxPriority.SelectedItem = (PriorityLevel)temporaryTask.Priority;
                dtpDateTimePicker.SelectedDate = temporaryTask.Date;
                btnChange.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
        }

        /// <summary>
        /// Reads and validates the Date.
        /// </summary>
        /// <returns></returns>
        private bool ReadDate()
        {
            if (dtpDateTimePicker.SelectedDate != null)
            {
                task.Date = (DateTime)dtpDateTimePicker.SelectedDate;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Reads and validates that the ToDo is not empty.
        /// </summary>
        /// <returns></returns>
        private bool ReadToDo()
        {
            if (String.IsNullOrEmpty(txtTodo.Text))
                return false;
            task.Description = txtTodo.Text;
            return true;
        }

        /// <summary>
        /// Reads and validates that a priority has been selected.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Make sure to ask the user to confirm when closing the program.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            ExitProgramOrNot();
            if (exitProram)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
        /// <summary>
        /// ´Handle asking the user wheter or not to exit the program.
        /// </summary>
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

        /// <summary>
        /// Close the application when picking the exit option in the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Reset all the GUI. And taskManager.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_New_Click(object sender, RoutedEventArgs e)
        {
            taskManager = new TaskManager();
            InitializeGUI();
            UpdateGUI();
        }

        /// <summary>
        /// Read saved TaskManager from a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                taskManager = fileHandler.LoadFile(filename, taskManager);
                UpdateGUI();
             }
        }

        /// <summary>
        /// Show the about box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_About_Click(object sender, RoutedEventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        /// <summary>
        /// Stores the current taskManager to a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                fileHandler.SaveFile(filename, taskManager);
            }
        }
        /// <summary>
        /// Update the time live.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherSeconds_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }
    }
}