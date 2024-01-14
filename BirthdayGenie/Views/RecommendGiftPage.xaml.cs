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

        // Anropa en asynkron metod för att undvika blocking
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Hämta produkter från API asynkront
        products = await GetProductsFromApiAsync();

        // Filtera och visa rekommendationer
        ShowRecommendations();
    }

    private async Task<List<Product>> GetProductsFromApiAsync()
    {
        using (var httpClient = new HttpClient())
        {
            // Ange den lokala adressen där API:et körs
            var apiEndpoint = "http://localhost:5000/api/products";

            // Gör ett GET-anrop för att hämta produkter från API
            var response = await httpClient.GetStringAsync(apiEndpoint);

            // Deserialisera JSON till en lista med Product-objekt
            var result = JsonSerializer.Deserialize<List<Product>>(response);

            // Om resultatet är null, returnera en tom lista istället
            return result ?? new List<Product>();
        }
    }

    private void ShowRecommendations()
    {
        // Filtera produkter baserat på användarinformation och visa resultatet
        var filteredProducts = FilterProducts();

        // Använd filteredProducts för att visa rekommendationer i användargränssnittet
        productsListView.ItemsSource = filteredProducts;
    }

    private List<Product> FilterProducts()
    {
        if (products == null)
        {
            Debug.WriteLine("No matching products");
        }


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
