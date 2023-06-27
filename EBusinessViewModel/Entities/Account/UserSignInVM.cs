using System.ComponentModel.DataAnnotations;

namespace EBusinessViewModel.Entities.Account
{
    public class UserSignInVM
    {
        [Required]
        public string UserName {get; set;}
        [Required,DataType(DataType.Password)]
        public string Password { get; set;}
        public bool IsParsistance { get; set; }
    }
}
