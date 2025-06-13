
using BusinessLogic;
using BusinessLogic.Contracts;
using BusinessLogic.Data;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

namespace RecipeDbApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("Default");
            //builder.Services.AddSqlServer<ApplicationDbContext>(connectionString);
            // Alternative
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddAppServices();

            #region Identity

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            builder.Services.Configure<JwtOptions>(jwtSettings);
            builder.Services.AddTransient<ITokenService, JwtTokenService>();

            // Registrierungen fuer Benutzerverwaltung
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            var jwtOptions = jwtSettings.Get<JwtOptions>();

            builder.Services.AddAuthentication(options =>
            {

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                };
            });

            #endregion

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Enums als string konvertieren statt integers zu verwenden
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

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
