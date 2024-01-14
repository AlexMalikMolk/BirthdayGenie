
using System.Text.Json;

namespace MockStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Configure Kestrel
            builder.WebHost.UseKestrel(options =>
            {
                options.ListenAnyIP(5000); // Set the port
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // Endpoint to get product information
            app.MapGet("/api/products", () =>
            {
                // Load data from the JSON file
                var jsonData = System.IO.File.ReadAllText("Data/MOCK_DATA.json");

                // Deserialize JSON to a list of Product objects
                var products = JsonSerializer.Deserialize<List<Product>>(jsonData);

                // Return the products
                return products;





            })
            .WithName("GetProducts")
            .WithOpenApi();

            app.Run();

        }
    }
}
