
using Microsoft.EntityFrameworkCore;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region Injections
            // to allow dependancy injiction for the Db Context 
            builder.Services.AddDbContext<StoreContext>((Options) =>
            {
                // i will get the coniction string from the app setting
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulrConnection"));
            });

            // to allow the dependancy injection for the genaricrepo 
            // tell the clr to create object from genaricRepo Class That implement IGenaricRepo
            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));

            #endregion


            var app = builder.Build();

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
