
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Service.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public required string Password { get; set; }
        
        [StringLength(100, MinimumLength = 8)]
        public required string FirstName { get; set; }
        
        [StringLength(100, MinimumLength = 8)]
        public required string LastName { get; set; }
    }
}
