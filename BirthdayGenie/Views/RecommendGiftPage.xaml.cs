using BirthdayGenie.Data;
using BirthdayGenie.Models;
using System.Diagnostics;
using System.Text.Json;

namespace BirthdayGenie.Views;

public partial class RecommendGiftPage : ContentPage
{
    private Birthday selectedBirthday;
    private List<Product>? products;

    public RecommendGiftPage(Birthday birthday)
    {
        InitializeComponent();
        selectedBirthday = birthday;

        // call asyncronus to avoid blocking the UI thread
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Get products from API
        products = await GetProductsFromApiAsync();

        // Filter products and show recommendations
        ShowRecommendations();
    }

    // Get products from API
    private async Task<List<Product>> GetProductsFromApiAsync()
    {
        using (var httpClient = new HttpClient())
        {

            var apiEndpoint = "http://localhost:5000/api/products";


            var response = await httpClient.GetStringAsync(apiEndpoint);


            var result = JsonSerializer.Deserialize<List<Product>>(response);


            return result ?? new List<Product>();
        }
    }
    // Filter products and show recommendations
    private void ShowRecommendations()
    {

        var filteredProducts = FilterProducts();


        if (filteredProducts == null || !filteredProducts.Any())
        {
            Debug.WriteLine("No matching products");

            // Visa ett meddelande för användaren
            DisplayAlert("No Matching Products", "Sorry, no matching products found.", "OK");

            // Ta användaren tillbaka till startsidan
            Navigation.PopAsync();


        }
        else
        {

            productsListView.ItemsSource = filteredProducts;
        }
    }

    private List<Product> FilterProducts()
    {

        // Debug lines to check that the data is correct
        Debug.WriteLine($"Selected Birthday: {selectedBirthday}");
        Debug.WriteLine($"Selected Budget: {selectedBirthday.Budget}");
        Debug.WriteLine($"Selected Category: {selectedBirthday.Category}");
        Debug.WriteLine($"Selected Brand: {selectedBirthday.FavoriteBrand}");
        Debug.WriteLine($"Selected Store: {selectedBirthday.FavoriteStore}");

        var filteredProducts = products
            .Where(p => p.Price <= selectedBirthday.Budget)
            .Where(p => p.Category == selectedBirthday.Category || p.Brand == selectedBirthday.FavoriteBrand || p.Store == selectedBirthday.FavoriteStore)
            .ToList();

        Console.WriteLine($"Filtered Products Count: {filteredProducts.Count}");

        return filteredProducts;
    }
}
