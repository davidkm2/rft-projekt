using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metin2RFT.Models.Entities
{
    public enum ItemCategory
    {
        Kard = 1,
        Vért,
        Hátas,
        Egyéb
    }

    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        [Required]
        public ItemCategory Category { get; set; }

        public int Price { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}