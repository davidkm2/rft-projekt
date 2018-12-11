using System.ComponentModel.DataAnnotations;

namespace Metin2RFT.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "E-mail cím")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "Az e-mail cím legfeljebb {1} karakter hosszú lehet.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Jelszó")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A jelszónak legalább {2} karakter hosszúnak kell lennie.", MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "Jelszó megerősítése")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A két jelszó nem egyezik.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Karakter törlő kód")]
        [DataType(DataType.Text)]
        [StringLength(7, ErrorMessage = "A karakter törlő kódnak {2} karakter hosszúnak kell lennie.", MinimumLength = 7)]
        public string DeleteCode { get; set; }
    }
}