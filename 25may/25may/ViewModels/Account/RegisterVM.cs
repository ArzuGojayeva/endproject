using System.ComponentModel.DataAnnotations;

namespace _25may.ViewModels.Account
{
    public class RegisterVM
    {
        public string UserName { get; set; }
        [MaxLength(100),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(100), DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(100), DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPasswird { get; set; }
    }
}
