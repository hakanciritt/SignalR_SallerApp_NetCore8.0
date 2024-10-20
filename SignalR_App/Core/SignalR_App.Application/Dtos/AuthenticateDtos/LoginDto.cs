using System.ComponentModel.DataAnnotations;

namespace SignalR_App.Application.Dtos.AuthenticateDtos
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
