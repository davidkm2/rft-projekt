using System.ComponentModel.DataAnnotations;

namespace Metin2RFT.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Jelszó")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Emlékezz rám")]
        public bool RememberMe { get; set; }
    }
}