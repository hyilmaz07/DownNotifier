using DownNotifier.BackgroundJob;
using DownNotifier.Business.Abstract;
using DownNotifier.Business.Concrete;
using DownNotifier.DataAccess.Abstract;
using DownNotifier.DataAccess.Concrete;
using DownNotifier.Entities.Models;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DownNotifier.Web
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
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection"),
                 conf =>
                 {
                     conf.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
                 }), ServiceLifetime.Transient);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Login/";
                options.ExpireTimeSpan = DateTime.Now.AddDays(7).TimeOfDay;
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;
            });

            services.AddHttpContextAccessor();
            services.AddSession();
            var hangfireConnectionString = Configuration["ConnectionStrings:HangfireConnection"];
            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };

                config.UseSqlServerStorage(hangfireConnectionString, option)
                      .WithJobExpirationTimeout(TimeSpan.FromHours(6));

            });
            services.AddHangfireServer();

            services.Configure<MailSettings>(Configuration.GetSection(nameof(MailSettings)));

            services.AddScoped<IAccountService, AccountManager>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<INotifierService, NotifierManager>();
            services.AddScoped<INotifierRepository, NotifierRepository>();

            services.AddTransient<IMailService, MailService>();


            services.AddControllersWithViews();
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor httpAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Hangfire DashBoard",
                AppPath = "/",
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() }
            });
            app.UseHangfireServer();
             
            DownNotifierJobSchedule.PrepareJobs();
        }
    }
}
