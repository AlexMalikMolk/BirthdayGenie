using InputKit.Shared.Controls;
using UraniumUI.Pages;
using BirthdayGenie.Data;
using BirthdayGenie.Models;
using BirthdayGenie.Views;

namespace BirthdayGenie
{
    public partial class MainPage : UraniumContentPage
    {
        public MainPage()
        {
            SelectionView.GlobalSetting.CornerRadius = 0;
            InitializeComponent();
            LoadBirthdays();
            BirthdaysListView.ItemSelected += BirthdaysListView_ItemSelected;
        }

        //Refreshes the list of birthdays when the page appears
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadBirthdays();
        }

        // Loads the list of birthdays from the database
        private void LoadBirthdays()
        {
            using (var db = new AppDbContext())
            {
                BirthdaysListView.ItemsSource = db.Birthdays.ToList();
            }
        }


        // Event handler for the "Edit Birthday" button
        private async void BirthdaysListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Birthday selectedBirthday)
            {
                await Navigation.PushAsync(new EditBirthdayPage(selectedBirthday));
            }
            // Clear selection
            BirthdaysListView.SelectedItem = null;
        }

        // Event handler for the "Add Birthday" button
        private async void OnAddBirthdayClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddBirthdayPage());
        }

        // Event handler for the "Settings" button
        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

    }
}