using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models;

public class Task
{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    
    [MaxLength(50)]
    public string? Description { get; set; }
    
    [ForeignKey("TaskStatus")]
    public int StatusId { get; set; }
    
    [MaxLength(50)]
    public string? Tag { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    // навигационные свойства
    public virtual User User { get; set; }
    public virtual TaskStatus TaskStatus { get; set; }
}