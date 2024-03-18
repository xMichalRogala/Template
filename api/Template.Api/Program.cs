using Template.Persistance;

namespace Template.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddPersistence(builder.Configuration, builder.Environment.IsDevelopment());

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger().UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
