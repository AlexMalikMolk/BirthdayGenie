using Microsoft.EntityFrameworkCore;
using BirthdayGenie.Models;
using System.IO;
using Microsoft.Maui.Storage;
using System.Text.Json;

namespace BirthdayGenie.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Birthday> Birthdays { get; set; }

        // Database for the app users 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Filename={Path.Combine(FileSystem.AppDataDirectory, "BirthdayGenie.db")}");

            }
        }

        public List<Birthday> GetAllBirthdays()
        {
            return Birthdays.ToList();
        }



    }
}
