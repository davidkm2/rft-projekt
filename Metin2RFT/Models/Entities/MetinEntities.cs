using Metin2RFT.Models.Entities;
using System.Data.Entity;

namespace Metin2RFT.Models
{
    public class MetinEntities : DbContext
    {
        public MetinEntities() : base("name=Database")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}