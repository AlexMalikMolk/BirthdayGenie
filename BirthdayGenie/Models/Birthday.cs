using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGenie.Models
{
    public class Birthday
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Interests { get; set; }
        public decimal Budget { get; set; }

        public string? FavoriteBrand { get; set; }
        public string? FavoriteColor { get; set; }
        public string? Size { get; set; } // For predefined sizes
        public string? CustomSize { get; set; } // For custom input
        public string? FavoriteStore { get; set; }
    }
}
