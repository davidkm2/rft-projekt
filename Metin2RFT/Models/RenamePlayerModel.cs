using Metin2RFT.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Metin2RFT.Models
{
    public class RenamePlayerModel
    {
        public Player Player { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "A név legfeljebb {1} karakter hosszú lehet.")]
        [DataType(DataType.Text)]
        [Display(Name = "Új név")]
        public string NewName { get; set; }
    }
}