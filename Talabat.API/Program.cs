
using Microsoft.EntityFrameworkCore;
using Talabat.API.Helper;
using Talabat.Core.IReposities;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.API
{
    public class Program
    {
        public static void Main(string[] args)
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


            var app = builder.Build();

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
