using SYNTAXMain.Core.Services.Titles;
using SYNTAXMain.Core.Services.Profiles;
using SYNTAXdb.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SYNTAXMain.Bots
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<TicketVendingProtocol>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TicketVendingProtcol;Trusted_Connection=True;MultipleActiveResultSets=true",
                    x => x.MigrationsAssembly("SYNTAXdb.DAL.Migrations"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<ITitleService, TitleService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ITicketService, TicketService>();

            var serviceProvider = services.BuildServiceProvider();

            var bot = new Bot(serviceProvider);
            services.AddSingleton(bot);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}