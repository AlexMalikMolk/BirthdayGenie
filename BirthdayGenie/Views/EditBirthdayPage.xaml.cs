using BirthdayGenie.Data;
using BirthdayGenie.Models;
using Microsoft.Maui.Controls;
using System;

namespace BirthdayGenie.Views
{
    public partial class EditBirthdayPage : ContentPage
    {
        private Birthday birthdayToEdit;

        public EditBirthdayPage(Birthday birthday)
        {
            InitializeComponent();
            birthdayToEdit = birthday;

            // Populate existing birthday data into fields
            NameEntry.Text = birthdayToEdit.Name;
            BirthdayPicker.Date = birthdayToEdit.DateOfBirth;
            InterestsEntry.Text = birthdayToEdit.Interests;
            BudgetEntry.Text = birthdayToEdit.Budget.ToString();
            FavoriteBrandEntry.Text = birthdayToEdit.FavoriteBrand;
            FavoriteStoreEntry.Text = birthdayToEdit.FavoriteStore;

        }


        private async void SaveButton_Clicked(object sender, EventArgs e)
        {

            // Update birthdayToEdit with new values from fields
            birthdayToEdit.Name = NameEntry.Text;
            birthdayToEdit.DateOfBirth = BirthdayPicker.Date;
            birthdayToEdit.Interests = InterestsEntry.Text;
            birthdayToEdit.Budget = decimal.Parse(BudgetEntry.Text); // Add error handling
            birthdayToEdit.FavoriteBrand = FavoriteBrandEntry.Text;
            birthdayToEdit.FavoriteStore = FavoriteStoreEntry.Text;

            using (var db = new AppDbContext())
            {
                db.Birthdays.Update(birthdayToEdit);
                await db.SaveChangesAsync();
            }

            await DisplayAlert("Success", "Birthday updated successfully!", "OK");
            await Navigation.PopAsync();
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this birthday?", "Yes", "No");
            if (confirm)
            {
                using (var db = new AppDbContext())
                {
                    db.Birthdays.Remove(birthdayToEdit);
                    await db.SaveChangesAsync();
                }

                await DisplayAlert("Deleted", "Birthday has been deleted.", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
