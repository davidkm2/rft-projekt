using Metin2RFT.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace Metin2RFT
{
    public class MetinEntities : DbContext
    {
        public MetinEntities() : base("name=Database")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Player> Players { get; set; }
    }
}