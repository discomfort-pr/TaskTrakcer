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
    
    [DefaultValue("NEW")]
    public string Status{ get; set; }
    
    [MaxLength(50)]
    public string? Tag { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    public int? GroupId { get; set; }
    
    // навигационные свойства
    public virtual User User { get; set; }
}