using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookApi.Data.Models;

public class User : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }
    
    //[Required]
    //[MaxLength(100)]
    //public string? Password { get; set; }
}