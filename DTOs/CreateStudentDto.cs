using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CreateStudentDto
{
    [MaxLength(25), Required]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(25), Required]
    public string LastName { get; set; } = string.Empty;
}