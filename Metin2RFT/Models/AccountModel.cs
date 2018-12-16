using Metin2RFT.Models.Entities;
using System.Collections.Generic;

namespace Metin2RFT.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Balance { get; set; }
        public bool IsBanned { get; set; }
        public List<Player> Players { get; set; }
    }
}