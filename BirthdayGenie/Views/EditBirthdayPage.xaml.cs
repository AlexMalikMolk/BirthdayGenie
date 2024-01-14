using BirthdayGenie.Data;
using BirthdayGenie.Models;
using Microsoft.Maui.Controls;
using System;

namespace BirthdayGenie.Views
{
    public partial class EditBirthdayPage : ContentPage
    {
        private Birthday birthdayToEdit;
        private List<string> categoryOptions = new List<string> { "Electronics", "Sports", "Books", "Home", "Clothing", "Beauty" };
        private List<string> brandOptions = new List<string> { "Apple", "Sony", "Microsoft", "Samsung", "Adidas", "Nike" };
        private List<string> storeOptions = new List<string> { "Mediamarkt", "Amazon", "Elgiganten", "NetOnNet", "Stadium", "IKEA", "Webhallen" };

        public EditBirthdayPage(Birthday birthday)
        {
            InitializeComponent();
            CategoryPicker.ItemsSource = categoryOptions;
            BrandPicker.ItemsSource = brandOptions;
            StorePicker.ItemsSource = storeOptions;
            birthdayToEdit = birthday;

            // Populate existing birthday data into fields
            NameEntry.Text = birthdayToEdit.Name;
            BirthdayPicker.Date = birthdayToEdit.DateOfBirth;
            CategoryPicker.SelectedItem = birthdayToEdit.Category;
            BudgetEntry.Text = birthdayToEdit.Budget.ToString();
            BrandPicker.SelectedItem = birthdayToEdit.FavoriteBrand;
            StorePicker.SelectedItem = birthdayToEdit.FavoriteStore;

        }

        // Event handler for the save button
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {

            // Update birthdayToEdit with new values from fields
            birthdayToEdit.Name = NameEntry.Text;
            birthdayToEdit.DateOfBirth = BirthdayPicker.Date;
            birthdayToEdit.Category = CategoryPicker.SelectedItem?.ToString();
            birthdayToEdit.Budget = decimal.Parse(BudgetEntry.Text);
            birthdayToEdit.FavoriteBrand = BrandPicker.SelectedItem?.ToString();
            birthdayToEdit.FavoriteStore = StorePicker.SelectedItem?.ToString();

            using (var db = new AppDbContext())
            {
                db.Birthdays.Update(birthdayToEdit);
                await db.SaveChangesAsync();
            }

            await DisplayAlert("Success", "Birthday updated successfully!", "OK");
            await Navigation.PopAsync();
        }

        // Event handler for the delete button
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


        // Event handler for the recommended gifts button
        private async void OnRecommendGiftClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecommendGiftPage(birthdayToEdit));
        }
    }
}
