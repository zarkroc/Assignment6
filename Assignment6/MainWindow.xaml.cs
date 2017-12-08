using System;
using System.Collections.Generic;
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

namespace Assignment6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskManager taskManager;
        Task task;
        public MainWindow()
        {
            InitializeComponent();
            taskManager = new TaskManager();
            UpdateGUI();
        }

        private void UpdateGUI()
        {

            cBoxPriority.Items.Add(PriorityLevel.High);
            cBoxPriority.Items.Add(PriorityLevel.Medium);
            cBoxPriority.Items.Add(PriorityLevel.Low);
            if (taskManager.Count > 0)
            {
                for (int i=0; i < taskManager.Count; i++)
                {
                    lstToDo.Items.Add(taskManager.GetTaskAtPosition(i).ToString());
                    
                }
            }
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            task = new Task(txtTodo.Text, (PriorityLevel) cBoxPriority.SelectedValue, (DateTime)dtpDateTimePicker.SelectedDate);
            taskManager.AddTask(task);
            UpdateGUI();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstToDo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
