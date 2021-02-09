using MKTFY.App;
using MKTFY.App.Repositories;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using MKTFY.App.Middleware;

namespace MKTFY.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            
            // Set up the database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                     Configuration.GetConnectionString("DefaultConnection"),   
                    // "Host=localhost;Port=26000;Database=devdb;User Id=devdbuser;Password=devdbpassword",
                b =>
                {
                    b.MigrationsAssembly("MKTFY.App");
                })
            );

            //Add Add Identity using our custom User Model
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetSection("Identity").GetValue<string>("Authority");

                    // name of the API resource
                    options.ApiName = "mktfyapi";
                    options.RequireHttpsMetadata = false;
                });

            services.AddControllers();

            // Add Repositories to dependency injection
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IListingRepository, ListingRepository>();
            services.AddScoped<IItemConditionRepository, ItemConditionRepository>();
            services.AddScoped<IListingStatusRepository, ListingStatusRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // using our Exception Handler
            app.UseMiddleware<GlobalExceptionMiddleware>();

            // Initialize the database
            // UpdateDatabase(app);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // Update the database to the latest migrations
        //private static void UpdateDatabase(IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices
        //         .GetRequiredService<IServiceScopeFactory>()
        //         .CreateScope())
        //    {
        //        using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
        //        {
        //            context.Database.Migrate();
        //        }
        //    }
        //}
    }
}
