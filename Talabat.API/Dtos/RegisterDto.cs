using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]
        //At least one uppercase letter.
        //At least one lowercase letter.
        //At least one digit.
        //At least one special character.
        //Minimum length of 8 characters.
        public string Password { get; set; }






    }
}
