using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OneFinanaceMVC.Models
{
    public class RegisterDto
    {
       
            [Required]
            public string Name { get; set; }
            [DataType(DataType.EmailAddress)]
            [Required]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        
    }
}
