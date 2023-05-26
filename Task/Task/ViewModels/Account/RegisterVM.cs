using System.ComponentModel.DataAnnotations;

namespace Task.ViewModels.Account
{
    public class RegisterVM
    {
        public string UserName { get; set; }
        [MaxLength(100),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(100),DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(100), DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
