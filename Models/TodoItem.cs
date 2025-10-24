using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
    }
}