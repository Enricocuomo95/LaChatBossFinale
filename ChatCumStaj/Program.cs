
using ChatCumStaj.Models;
using ChatCumStaj.Repository;
using ChatCumStaj.Service;
using Microsoft.EntityFrameworkCore;

namespace ChatCumStaj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ChatChattiamoContext>(options => options.UseSqlServer(
               builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<UtenteRepo>();
            builder.Services.AddScoped<IStanza, StanzaRepo>();
            builder.Services.AddScoped<IMessaggio, MessaggioRepo>();
            builder.Services.AddScoped<IService, UtenteService>();
            builder.Services.AddScoped<IStanzaService, StanzaService>();
            builder.Services.AddScoped<StanzaService>();
            builder.Services.AddScoped<IMessaggioService, MessaggioService>();

            var app = builder.Build();

            app.UseCors(builder =>
                builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());


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
