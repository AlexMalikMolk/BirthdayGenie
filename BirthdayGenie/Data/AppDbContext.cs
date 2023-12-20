using Microsoft.EntityFrameworkCore;
using BirthdayGenie.Models;
using System.IO;
using Microsoft.Maui.Storage;

namespace BirthdayGenie.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Birthday> Birthdays { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Filename={Path.Combine(FileSystem.AppDataDirectory, "GiftGenie.db")}");
            }
        }

        public List<Birthday> GetAllBirthdays()
        {
            return Birthdays.ToList();
        }
    }
}
