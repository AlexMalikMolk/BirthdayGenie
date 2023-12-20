using UraniumUI.Material.Resources;
using BirthdayGenie.Data;

namespace BirthdayGenie
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }

            MainPage = new AppShell();
        }
    }
}
