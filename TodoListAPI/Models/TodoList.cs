using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TodoListAPI.Models
{
    public class TodoList
    {
        [Key]
        public int TodoListId { get; set; }

        [Required]
        public string Title { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        public ICollection<TodoTask> Tasks { get; set; }
    }
}

