
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.API.Helper;
using Talabat.Core.Enities;
using Talabat.Core.IReposities;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeeding;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            //var DbContext = new StoreContext();
            //await DbContext.Database.MigrateAsync();


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region Injections

            // Register IConfiguration for DI
            builder.Services.AddSingleton(builder.Configuration);


            // to allow dependancy injiction for the Db Context 
            builder.Services.AddDbContext<StoreContext>((Options) =>
            {
                // i will get the coniction string from the app setting
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulrConnection"));
            });

            ///Anthor Way 
            /// Add DbContext and IConfiguration to DI
            ///builder.Services.AddDbContext<StoreContext>((options, context) =>
            ///{
            ///    context.UseSqlServer(options.GetRequiredService<IConfiguration>().GetConnectionString("DefaulrConnection"));
            ///});



            // to allow the dependancy injection for the genaricrepo 
            // tell the clr to create object from genaricRepo Class That implement IGenaricRepo
            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));


            // to add The Mapping Profile
            // to allow dependancy injection to the auto mapper
            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            // another way to nake injection for the automapper and this the best way
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            #endregion



            // Register Identity services
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<StoreContext>();


            var app = builder.Build();

            #region For Update Dtatabase With The DataSeeding
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await IdentityUserSeeding.UserSeedAsync(userManager , logger);
            }
            catch (Exception ex)
            {
                
                logger.LogError(ex, "An error occurred during migration or seeding the database.");
            }

            #endregion

            // to apply the files request Form WWWRoot 
            app.UseStaticFiles();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
