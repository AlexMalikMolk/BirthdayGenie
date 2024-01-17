using BirthdayGenie.Data;
using BirthdayGenie.Views;
using System;
using Microsoft.Maui.Controls;
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


        protected override void OnStart()
        {
            base.OnStart();


            if (MainPage is AppShell appShell && appShell.CurrentPage is SettingsPage settingsPage)
            {
                settingsPage.DarkModeChanged += OnDarkModeChanged;
            }

        }

        private void OnDarkModeChanged(object sender, bool isDarkMode)
        {


            if (App.Current != null)
            {

                App.Current.UserAppTheme = isDarkMode ? AppTheme.Dark : AppTheme.Light;
            }
            else
            {
                Console.WriteLine("Warning: App.Current is null in OnDarkModeChanged.");
            }
        }
    }
}
