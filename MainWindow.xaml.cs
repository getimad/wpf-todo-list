using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TodoList.Utilities;
using TodoList.Models;


namespace TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataAccess _dataAccess = new DataAccess();

        public MainWindow()
        {
            InitializeComponent();

            RenderListView();
        }

        /// <summary>
        /// Refresh the list view
        /// </summary>
        private void RenderListView()
        {
            var tasks = _dataAccess.GetTasks();

            PrimaryList.ItemsSource = tasks;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            // Input
            var content = Input.Text;

            // Priority
            var priority = "Priority 4";
            if (PriorityComboBox.SelectedItem is ComboBoxItem selectedComboBoxItem)
            {
                priority = selectedComboBoxItem.Content.ToString();
            }

            // Clear the forms
            Input.Clear();
            PriorityComboBox.SelectedIndex = 3;

            // Add Task to tasks table in database
            _dataAccess.AddTask(content, priority);

            RenderListView();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Converting sender from object to Task
            if (sender is ListViewItem item)
            {
                if (item.Content is Task task)
                {
                    var id = task.Id;

                    _dataAccess.DeleteTask(id);
                }
            }

            RenderListView();
        }
    }
}
