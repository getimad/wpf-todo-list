using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using TodoList.Utilities;
using TodoList.Models;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.Metadata;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataAccess _dataAccess = new DataAccess();

        /// <summary>
        /// Get tasks from the database.
        /// </summary>
        public List<Task> Tasks => _dataAccess.GetTasks();

        public MainWindow()
        {
            InitializeComponent();

            RenderListView();
        }

        /// <summary>
        /// Refresh the list view.
        /// </summary>
        private void RenderListView(List<Task>? tasks = null)
        {
            PrimaryList.ItemsSource = tasks ?? Tasks;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            // Task content
            var content = Input.Text.Trim();
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
        }
    }
}
