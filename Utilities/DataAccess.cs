using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TodoList.Models;

namespace TodoList.Utilities
{
    public class DataAccess
    {
        public List<Task> GetTasks()
        {
            var tasks = new List<Task>();

            using (var connection = new SqlConnection(Helper.GetConnectionString("TodoListDB")))
            {
                connection.Open();

                var query = "SELECT * FROM Tasks";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var task = new Task
                            {
                                Id = (int)reader["Id"],
                                Content = (string)reader["Content"],
                                Priority = (string)reader["Priority"],
                                Date = (DateTime)reader["Date"]
                            };

                            tasks.Add(task);
                        }
                    }
                }
            }

            return tasks;
        }

        public void AddTask(string content, string priority)
        {
            using var connection = new SqlConnection(Helper.GetConnectionString("TodoListDB"));

            connection.Open();

            var query = "INSERT INTO Tasks (Content, Priority) VALUES (@Content, @Priority)";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Content", content);
            command.Parameters.AddWithValue("@Priority", priority);

            command.ExecuteNonQuery();
        }

        public void DeleteTask(int id)
        {
            using var connection = new SqlConnection(Helper.GetConnectionString("TodoListDB"));

            connection.Open();

            var query = "DELETE FROM TASKS WHERE Id = @Id";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}
