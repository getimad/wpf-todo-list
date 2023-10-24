using System;

namespace TodoList.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Priority {  get; set; }
        public DateTime Date { get; set; }
    }
}
