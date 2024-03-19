using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[Index(nameof(Name), IsUnique = true)]
public class User: BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Password { get; set; }
}