using System.ComponentModel.DataAnnotations;

namespace Practise.DTO
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
