
using BusinessLogic;
using System.Text.Json.Serialization;
using WebApiContrib.Core.Formatter.Csv;

namespace RecipeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInMemoryServices();
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Enums als string konvertieren statt integers zu verwenden
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                })
                // Weitere Formatter hinzufügen
                .AddXmlSerializerFormatters()
                .AddCsvSerializerFormatters();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
