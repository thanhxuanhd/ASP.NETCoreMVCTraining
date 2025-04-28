using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Service.Dtos
{
    public class RefreshTokenDto
    {
        [Required]
        public required string AccessToken { get; set; }

        [Required]
        public required string RefreshToken { get; set; }
    }
}