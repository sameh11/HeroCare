using BusinessApp.Core.ApplicationSerivce;
using BusinessApp.Core.ApplicationSerivce.Service;
using BusinessApp.Core.DomainService.AccountRepository;
using BusinessApp.Core.Entitiy.Users;
using BusinessApp.Infrastructure;
using BusinessApp.Infrastructure.Data.DomainRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HeroCare
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }
        public Startup(IHostingEnvironment env)
        {
            Env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //
            services.AddDbContext<HeroCareCoreContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("HeroCareCoreContext")));
            //
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<HeroCareCoreContext>()
            .AddDefaultTokenProviders();

            //
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //
            //services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            //
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountService, AccountService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors();
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
