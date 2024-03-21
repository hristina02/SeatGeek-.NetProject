using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SeatGeek.WebApi
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Services.Data.Interfaces;
    using Web.Infrastructure.Extensions;
    using static Common.GeneralApplicationConstants;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = "Server=.;Database=SeatGeek;Encrypt=false;Trusted_Connection=true;";

            builder.Services.AddDbContext<SeatGeekDbContext>(opt =>
                opt.UseSqlServer(connectionString));

            builder.Services.AddApplicationServices(typeof(IEventService));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("SeatGeek", policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins("https://localhost:7130")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
          
            app.MapControllers();

            app.UseCors("SeatGeek");

            app.Run();
        }
    }
}