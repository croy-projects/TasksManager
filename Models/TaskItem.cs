using System.ComponentModel.DataAnnotations;

namespace TasksManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public string UserId { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}