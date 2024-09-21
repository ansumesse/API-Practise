using System.ComponentModel.DataAnnotations;

namespace Practise.DTO
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
