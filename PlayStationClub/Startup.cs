using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using PlayStationClub.Areas.Identity.Data;
using PlayStationClub.Areas.Services;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using PlayStationClub.Data.Configuration;
using PlayStationClub.Infrastructure;
using System;

namespace PlayStationClub
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
            //PostgreSQL connected
            services.AddDbContext<PlayStationClubDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("GstickDb")));
            services.AddDbContext<PlayStationClubContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("GstickDb")));

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<PlayStationClubUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<PlayStationClubContext>();
            services.AddRazorPages();

            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromDays(1);
                o.SlidingExpiration = true;
            });
            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(3));
            services.AddTransient<IEmailSender, SendGridEmailSender>();
            services.Configure<SendGridEmailSenderOptions>(opt =>
            {
                opt.ApiKey = Configuration["EmailSender:SendGrid:Key"];
                opt.SenderEmail = Configuration["EmailSender:SendGrid:Email"];
                opt.SenderName = Configuration["EmailSender:SendGrid:User"];
            });

            services.AddAuthentication().AddGoogle(opt =>
            {
                IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                opt.ClientId = googleAuthNSection["ClientId"];
                opt.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ISessionService, SessionService>();

            services.AddAutoMapper(typeof(MapperConfigurator));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
