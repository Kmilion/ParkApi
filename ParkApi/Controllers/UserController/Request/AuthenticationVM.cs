using System.ComponentModel.DataAnnotations;

namespace ParkApi.Controllers.UserController.Request
{
    public class AuthenticationVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
