using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        [MinLength(5,ErrorMessage ="Min length 5 character")]
        public string DisplayName { set; get; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,20}$",
        ErrorMessage = "Password must be 6-20 characters, include uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }
    }
}
