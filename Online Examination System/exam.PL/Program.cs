using exam.BLL.Interfaces;
using exam.BLL.Interfaces.token;
using exam.BLL.Repositories;
using exam.DAL.Data;
using exam.PL.Extentions;
using Microsoft.EntityFrameworkCore;
using VHW.BLL.Token;

namespace exam.PL
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
            builder.Services.AddDbContext<ExaminationContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IUserGenericRepository<>), typeof(UserGenericRepository<>));

            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenServices));
            builder.Services.AddIdentityServices(builder.Configuration);






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