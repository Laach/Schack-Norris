using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Norris.Data.Data;
using Norris.Data.Data.Entities;

namespace Norris.Data
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration conf)
        {
            Configuration = conf;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (System.Environment.UserName == "casper"){
                services.AddDbContext<NContext>(options => options.UseSqlite("Data Source=norris.db"));
            }
            else{
                services.AddDbContext<NContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<NContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
