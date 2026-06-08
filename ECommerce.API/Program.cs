
using ECommerce.API.Extensions;
using ECommerce.Domain.Contracts;
using ECommerce.Presistence.Data.DataSeed;
using ECommerce.Presistence.Data.DbContexts;
using ECommerce.Presistence.Repositories;
using ECommerce.Services;
using ECommerce.Services.Abstraction;
using ECommerce.Services.MappingProfiles;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<StoreDbContext>(options=> 
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                
                
                });

            builder.Services.AddScoped<IDataIntializer, DataIntializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(x=>x.AddProfile<ProductProfile>());
            builder.Services.AddScoped<IProductService, ProductService>();


            var app = builder.Build();


            await app.MigrateDataBaseAsync();

            await app.SeedDataAsync();
           
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

           await app.RunAsync();
        }
    }
}
