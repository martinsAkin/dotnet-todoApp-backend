using System.ComponentModel.DataAnnotations;

namespace todoApp.models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
