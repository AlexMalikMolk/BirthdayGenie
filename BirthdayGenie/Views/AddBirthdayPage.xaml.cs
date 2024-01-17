using BirthdayGenie.Models;
using BirthdayGenie.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System;
using UraniumUI.Pages;

namespace BirthdayGenie.Views
{


    public partial class AddBirthdayPage : UraniumContentPage
    {

        private List<string> categoryOptions = new List<string> { "Electronics", "Sports", "Books", "Home", "Clothing", "Beauty" };
        private List<string> brandOptions = new List<string> { "Apple", "Sony", "Microsoft", "Samsung", "Adidas", "Nike" };
        private List<string> storeOptions = new List<string> { "Mediamarkt", "Amazon", "Elgiganten", "NetOnNet", "Stadium", "IKEA", "Webhallen" };

        public AddBirthdayPage()
        {
            InitializeComponent();
            CategoryPicker.ItemsSource = categoryOptions;
            BrandPicker.ItemsSource = brandOptions;
            StorePicker.ItemsSource = storeOptions;
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


            // Create a new Birthday object from user input
            var newBirthday = new Birthday
            {
                Name = NameEntry.Text,
                DateOfBirth = BirthdayPicker.Date,
                Category = CategoryPicker.SelectedItem?.ToString(),
                Budget = budget,
                FavoriteBrand = BrandPicker.SelectedItem?.ToString(),
                FavoriteStore = StorePicker.SelectedItem?.ToString() 
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

    }
}
