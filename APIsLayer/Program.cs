
using CoreLayer.RepoContract;
using Microsoft.EntityFrameworkCore;
using RepoLayer.Contract;
using RepoLayer.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using APIsLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using APIsLayer.Errors;
using APIsLayer.MiddleWares;
using APIsLayer.Extensions;
using StackExchange.Redis;
using CoreLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RepoLayer.Data.DataSeed;

namespace APIsLayer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddOpenApi();
           
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(connectionString));
            builder.Services.AddDbContext<IdentityContext>(options => 
            { options.UseSqlServer(builder.Configuration.GetConnectionString("identityConnection")); });
            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection") ?? throw new InvalidOperationException("Connection string 'Redis' not found.");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddServices();
            builder.Services.AddIdentityServices(builder.Configuration);

              var app = builder.Build();

            #region update database automatic when runing app
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<AppDbContext>();
         
            var loggerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbcontext.Database.MigrateAsync();
               
                await StoreContextSeed.SeedAsync(_dbcontext);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "Error when updating database");
            }
            try
            {
                var _identitycontext = services.GetRequiredService<IdentityContext>();
                var usermanager = services.GetRequiredService<UserManager<UserApplication>>();

                await _identitycontext.Database.MigrateAsync();
                await identitycontextseed.IdentitySeeding(usermanager);

            }
            catch(Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "Error when updating database identity");
            }
            #endregion

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddlewarecs>();
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }
            app.UseStatusCodePagesWithRedirects("errors/{0}");
            
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
