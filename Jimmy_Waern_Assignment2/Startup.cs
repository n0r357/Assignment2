using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Jimmy_Waern_Assignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Jimmy_Waern_Assignment2.Data;

namespace Jimmy_Waern_Assignment2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("HiddenPolicy", policy => policy.RequireRole("Publisher", "Subscriber", "Administrator"));
                options.AddPolicy("AdultPolicy", policy => policy.Requirements.Add(new MinimumAgeRequirement(20)));
                options.AddPolicy("SportsPolicy", policy => policy.RequireRole("Publisher", "Administrator"));
                options.AddPolicy("CulturePolicy", policy => policy.RequireRole("Publisher", "Administrator"));
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
