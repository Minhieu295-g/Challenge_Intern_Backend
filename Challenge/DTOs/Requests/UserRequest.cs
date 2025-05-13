using System.ComponentModel.DataAnnotations;

namespace Challenge2.DTOs.Requests
{
    public class UserRequest
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
