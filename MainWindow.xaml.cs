using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using TodoList.Utilities;
using TodoList.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Get tasks from the database.
        /// </summary>
        public List<Task>? Tasks { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            RenderListView();
        }

        /// <summary>
        /// Refresh the list view.
        /// </summary>
        private void RenderListView()
        {
            PrimaryList.ItemsSource = Tasks = DataAccess.GetTasks();
        }

        private void RenderListView(List<Task>? tasks = null)
        {
            if (tasks is null)
                RenderListView();

            PrimaryList.ItemsSource = tasks;
        }

        /// <summary>
        /// Clear all input form of MainWindow
        /// </summary>
        private void ClearForm()
        {
            SearchInput.Clear();
            AddInput.Clear();
            SearchByComboBox.SelectedIndex = 0;
            SortedByComboBox.SelectedIndex = 0;
            DesCheckBox.IsChecked = false;
            PriorityComboBox.SelectedIndex = 3;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            // Task content
            var content = AddInput.Text.Trim();
            // Handle empty input
            if (string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Input field cannot be empty.");

                return;
            }

            // Task priority
            var priority = "Priority 4";
            if (PriorityComboBox.SelectedItem is ComboBoxItem selectedComboBoxItem)
            {
                priority = selectedComboBoxItem.Content.ToString();
            }

            ClearForm();

            // Add Task to tasks table in database
            DataAccess.AddTask(content, priority);

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

                    DataAccess.DeleteTask(id);
                }
            }

            RenderListView();
        }

        private void Search_Event(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)  // Check if MainWindow.xaml is ready.
                return;

            var searchContent = SearchInput.Text.Trim();

            var searchBy = "All";
            if (SearchByComboBox.SelectedItem is ComboBoxItem selectedComboBoxItem)
            {
                searchBy = selectedComboBoxItem.Content.ToString();
            }

            var tasks = new List<Task>();

            foreach (var task in Tasks)
            {
                var idMatch = task.Id.ToString().Contains(searchContent, StringComparison.OrdinalIgnoreCase);
                var contentMatch = task.Content.Contains(searchContent, StringComparison.OrdinalIgnoreCase);

                if ((searchBy == "Id" && idMatch) ||
                    (searchBy == "Content" && contentMatch) ||
                    (searchBy == "All" && (idMatch || contentMatch)))
                {
                    tasks.Add(task);
                }
            }

            RenderListView(tasks);
            SortedBy_Event(sender, e);
        }

        private void SortedBy_Event(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)  // Check if MainWindow.xaml is ready.
                return;

            var sortedBy = SortedByComboBox.SelectedItem is ComboBoxItem selectedComboBoxItem ? selectedComboBoxItem.Content.ToString() : null;

            if (sortedBy is null)
                return;

            var tasks = PrimaryList.ItemsSource is IEnumerable<Task> list ? list.ToList() : new List<Task>();

            tasks.Sort(new DynamicComparer<Task>(sortedBy));
            if (DesCheckBox.IsChecked ?? false)
                tasks.Reverse();

            RenderListView(tasks);
        }
    }
}
