using System.ComponentModel.DataAnnotations;

namespace Task.ViewModels.Account
{
    public class LoginVM
    {
        public string UserName { get; set; }
        [MaxLength(100),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
