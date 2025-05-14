using System.ComponentModel.DataAnnotations;

namespace Challenge2.DTOs.Requests
{
    public class UserRequest
    {

        [Required(ErrorMessage ="Username is required")]
        [MaxLength(255, ErrorMessage = "Username must be at most 255 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [MaxLength(255, ErrorMessage = "Password must be at most 255 characters")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must contain at least 1 uppercase letter and 1 special character")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(0|\+84)[0-9]{9}$", ErrorMessage = "Invalid number phone")]
        public string Phone { get; set; }
    }
}
