using BLL.Services.Implementation;
using BLL.Services.Interfaces;
using Common.AutoMapper;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Win32;
using Repository.Implementation;
using Repository.Interfaces;

namespace HRMSAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // Register Swagger with OpenAPI info
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "Example API with Swagger and OpenAPI",
                    Contact = new OpenApiContact
                    {
                        Name = "Your Name",
                        Email = "your@email.com"
                    }
                });
            });

            // Register DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // Register Repositories
            builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();

            // Register Services
            builder.Services.AddScoped<IRegistrationService, RegistrationService>();

            var app = builder.Build();

            // Enable Swagger middleware
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                    c.RoutePrefix = string.Empty; // Swagger at root URL
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Ensure database is created and migrated
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}
