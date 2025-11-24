using Character.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Character.Infrastructure
{
    public class CharacterContext : IdentityDbContext<UserModel>
    {
        public DbSet<CharacterClassModel> Characters { get; set; }
        public DbSet<Warrior> Warriors { get; set; }
        public DbSet<Mage> Mages { get; set; }
        public DbSet<WeaponModel> Weapons { get; set; }
        public DbSet<Quests> Quests { get; set; }

        public CharacterContext()
        {
        }

        public CharacterContext(DbContextOptions<CharacterContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = characters.db");
            }
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
