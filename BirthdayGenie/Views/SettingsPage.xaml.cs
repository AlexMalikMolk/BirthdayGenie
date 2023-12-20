using Microsoft.Maui.Controls;

namespace BirthdayGenie.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            // Example: Save the settings
            bool notificationsEnabled = NotificationSwitch.IsToggled;

            // TODO: Implement logic MAYBEEEEE

            bool DarkModeSwitch_Toggled = DarkModeSwitch.IsToggled;
           
            if (DarkModeSwitch_Toggled)
            {
                return;
            }

            await DisplayAlert("Settings Saved", "Your settings have been saved successfully", "OK");
            await Navigation.PopAsync(); // Navigate back to the previous page
        }

        private void DarkModeSwitch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}
