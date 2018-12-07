using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metin2RFT.Models.Entities
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Empire { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }
        public long Gold { get; set; }

        public virtual Account Account { get; set; }
    }
}