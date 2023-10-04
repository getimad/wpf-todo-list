using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TodoList.Models;

namespace TodoList.Utilities
{
    public class DataAccess
    {
        public void AddTask(string content)
        {
            using var connection = new SqlConnection(Helper.GetConnectionString("TodoListDB"));

            connection.Open();

            var query = "INSERT INTO Tasks (Content) VALUES (@Content)";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Content", content);

            command.ExecuteNonQuery();
        }

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
                                Date = (DateTime)reader["Date"]
                            };

                            tasks.Add(task);
                        }
                    }
                }
            }

            return tasks;
        }
    }
}
