using System;
using System.ComponentModel.DataAnnotations;

namespace TaskApi.Entities
{
    public class TaskEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public bool IsDone { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
