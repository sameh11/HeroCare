using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HeroCare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

///Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Initial Catalog=TestHeroCare;Persist Security Info=False;User ID=.;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
