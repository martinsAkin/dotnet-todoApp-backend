using System.ComponentModel.DataAnnotations;

namespace todoApp.models.requests
{
    public class SignupRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
