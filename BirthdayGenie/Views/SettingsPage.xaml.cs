using Microsoft.Maui.Controls;
using System;
using UraniumUI.Pages;

namespace BirthdayGenie.Views
{
    public partial class SettingsPage : UraniumContentPage
    {
        // Event handler for dark mode switch
        public event EventHandler<bool>? DarkModeChanged;

        public SettingsPage()
        {

            InitializeComponent();


        }

        private async void OnSaveSettingsClicked(object sender, EventArgs e)
        {

            bool darkModeSwitchToggled = DarkModeSwitch.IsToggled;

            // Signal that the dark mode switch was toggled
            DarkModeChanged?.Invoke(this, darkModeSwitchToggled);

            await DisplayAlert("Inställningar sparade", "Dina inställningar har sparats framgångsrikt", "OK");
            await Navigation.PopAsync();
        }


        //Update the switch to the current theme
        private void DarkModeSwitch_Toggled(object sender, ToggledEventArgs e)
        {

            if (App.Current != null)
            {
                App.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
            }
            else
            {
                Console.WriteLine("Warning: App.Current is null in OnDarkModeChanged.");
            }
        }
    }
}
