using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Norris.Data.Data;

namespace Norris.UI
{  
    //Extension method for seeding the database
    public static class WebHostExtensions
    {
        public static IWebHost SeedData(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                //accessing the NContext service
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<NContext>();
                //applying all migrations on the database
                context.Database.Migrate();
                //add predefinded data to the the database
                new DatabaseSeeder(context).SeedData();
                return host;
            }
        }
    }
}
