using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid(); // Generates GUID on creation

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = null!;

    [MaxLength(255)]
    public string? ProfilePictureUrl { get; set; }

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [MaxLength(255)]
    public string? DateOfBirth { get; set; } // Keep as nvarchar(255)

    [MaxLength(100)]
    public string? DisplayName { get; set; }
}
