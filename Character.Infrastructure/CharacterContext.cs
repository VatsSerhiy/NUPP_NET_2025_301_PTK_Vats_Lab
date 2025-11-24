using Character.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character.Infrastructure
{
    public class CharacterContext : DbContext
    {
        public DbSet<CharacterClassModel> Characters { get; set; }
        public DbSet<Warrior> Warriors { get; set; }
        public DbSet<Mage> Mages { get; set; }
        public DbSet<WeaponModel> Weapons { get; set; }
        public DbSet<Quests> Quests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = characters.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterClassModel>().ToTable("Characters");
            modelBuilder.Entity<Warrior>().ToTable("Warriors");
            modelBuilder.Entity<Mage>().ToTable("Mages");
            modelBuilder.Entity<Warrior>().HasMany(x => x.Weapons).WithOne(k => k.Owner).HasForeignKey(p=>p.OwnerId);
            modelBuilder.Entity<CharacterClassModel>().HasOne<CharacterProfileModel>(x => x.characterProfile).WithOne(p => p.Character).HasForeignKey<CharacterProfileModel>(p => p.IdCharacter);
            base.OnModelCreating(modelBuilder);
        }

    }
}
