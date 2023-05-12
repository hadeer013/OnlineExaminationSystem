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
        public static async Task Main(string[] args)
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
            builder.Services.AddScoped(typeof(ISubjectRepository), typeof(SubjectRepository));
            builder.Services.AddScoped(typeof(IExamDistributionRepository), typeof(ExamDistrinutionRepository));
            builder.Services.AddScoped(typeof(IMCQwithMultipleCorrectAnsRepository), typeof(MCQwithMultipleCorrectAnsRepository));
            builder.Services.AddScoped(typeof(IMCQwithOneCorrectAnswerRepository), typeof(MCQwithOneCorrectAnswerRepository));
            builder.Services.AddScoped(typeof(IExamRepository), typeof(ExamRepository));
            builder.Services.AddScoped(typeof(IStudentExamCopyWithQuestionRelationhipRepository), typeof(StudentExamCopyWithQuestionRelationhipRepository));
            builder.Services.AddScoped(typeof(IExamSubmissionRepository), typeof(ExamSubmissionRepository));

            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenServices));
            builder.Services.AddIdentityServices(builder.Configuration);






            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = services.GetRequiredService<ExaminationContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message, "An Error occured during applying migration");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}