namespace bentran1vn.question.repository
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var startup = new StartUp(builder, builder.Environment);
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();
            startup.Configure(app, builder.Environment);
            app.Run();
        }
    }
}
