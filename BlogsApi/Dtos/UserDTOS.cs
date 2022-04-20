using System.ComponentModel.DataAnnotations;

namespace BlogsApi.Dtos
{
    public class UserDTOS { 
    
        [Required(ErrorMessage = "Email Required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
    }
}
