using Serilog;
using Template.Api.Middlewares;
using Template.Persistance;

namespace Template.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddPersistence(builder.Configuration, builder.Environment.IsDevelopment());

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger().UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();
            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
