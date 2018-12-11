using Metin2RFT.Models.Entities;
using System.Collections.Generic;

namespace Metin2RFT.Models
{
    public class AccountManageModel
    {
        public Account Account { get; set; }
        public List<Item> Items { get; set; }
        public List<Player> Players { get; set; }
    }
}