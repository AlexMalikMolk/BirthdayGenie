using BirthdayGenie.Models;
using BirthdayGenie.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System;

namespace BirthdayGenie.Views
{


    public partial class AddBirthdayPage : ContentPage
    {
        public AddBirthdayPage()
        {
            InitializeComponent();
            SizePicker.SelectedIndexChanged += SizePicker_SelectedIndexChanged;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate Required Fields: Name, DateOfBirth, and Budget
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || BudgetEntry.Text == null)
            {
                await DisplayAlert("Error", "Name, Date of Birth, and Budget are required.", "OK");
                return;
            }

            if (!decimal.TryParse(BudgetEntry.Text, out decimal budget))
            {
                await DisplayAlert("Error", "Invalid budget format", "OK");
                return;
            }

            string selectedSize = SizePicker.SelectedItem?.ToString();
            string customSize = selectedSize == "Custom" ? CustomSizeEntry.Text : string.Empty;

            // Create a new Birthday object from user input
            var newBirthday = new Birthday
            {
                Name = NameEntry.Text,
                DateOfBirth = BirthdayPicker.Date,
                Interests = InterestsEntry.Text,
                Budget = budget,
                FavoriteBrand = FavoriteBrandEntry.Text,
                FavoriteColor = FavoriteColorPicker.SelectedItem?.ToString(),
                Size = selectedSize,
                CustomSize = customSize,
                FavoriteStore = FavoriteStoreEntry.Text
            };

            // Save the new Birthday object to the database
            using (var db = new AppDbContext())
            {
                db.Birthdays.Add(newBirthday);
                await db.SaveChangesAsync();
            }

            await DisplayAlert("Success", "Birthday added successfully!", "OK");
            await Navigation.PopAsync();
        }

        private void SizePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomSizeEntry.IsVisible = SizePicker.SelectedItem?.ToString() == "Custom";
        }
    }
}
