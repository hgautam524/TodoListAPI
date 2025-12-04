using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListAPI.Models
{
    public class TodoTask
    {
        [Key]
        public int TodoTaskId { get; set; }

        [Required]
        public string Title { get; set; }

        public bool Status { get; set; } = false;

        public int TodoListId { get; set; }

        [ForeignKey("TodoListId")]
        public TodoList TodoList { get; set; }
    }
}

