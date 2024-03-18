using Template.Persistance;

namespace Template.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app1 = builder.Services;

            builder.Services.AddPersistence(builder.Configuration, builder.Environment.IsDevelopment());

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
