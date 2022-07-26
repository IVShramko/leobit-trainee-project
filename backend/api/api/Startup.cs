using Dating.Logic.DB;
using Dating.Logic.Facades.AccountFacade;
using Dating.Logic.Facades.AlbumFacade;
using Dating.Logic.Facades.PhotoFacade;
using Dating.Logic.Facades.SearchFacade;
using Dating.Logic.Facades.UserProfileFacade;
using Dating.Logic.Repositories;
using Dating.Logic.Repositories.ImageRepository;
using Dating.Logic.Repositories.UserAlbumRepository;
using Dating.Logic.Repositories.UserPhotoRepository;
using Dating.Logic.Managers.AlbumManager;
using Dating.Logic.Managers.PhotoManager;
using Dating.Logic.Managers.TokenManager;
using Dating.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Dating.Logic.Infrastructure;

namespace Dating.WebAPI
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
            services.AddDbContext<AppDbContext>(config => 
            {
                config.UseSqlServer(
                    Configuration.GetConnectionString("DatingConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
               config.Password.RequireDigit = false;
               config.Password.RequireLowercase = false;
               config.Password.RequireUppercase = false;
               config.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(config => 
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                var secretBytes = Encoding.UTF8.GetBytes(
                    Configuration.GetSection("Constants")["Secret"]);

                var key = new SymmetricSecurityKey(secretBytes);

                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration.GetSection("Constants")["Issuer"],
                    ValidAudience = Configuration.GetSection("Constants")["Audiance"],
                    IssuerSigningKey = key
                };
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();                      
                });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //repositories
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserAlbumRepository, UserAlbumRepository>();
            services.AddScoped<IUserPhotoRepository, UserPhotoRepository>();
            services.AddScoped<IImageRepository,ImageRepository>();

            //facades
            services.AddScoped<IAccountFacade, AccountFacade>();
            services.AddScoped<IUserProfileFacade, UserProfileFacade>();
            services.AddScoped<ISearchFacade, SearchFacade>();
            services.AddScoped<IAlbumFacade, AlbumFacade>();
            services.AddScoped<IPhotoFacade, PhotoFacade>();

            //managers
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPhotoManager, PhotoManager>();
            services.AddScoped<IAlbumManager, AlbumManager>();

            //middlewares
            services.AddTransient<TokenManagerMiddleware>();

            //utilities
            services.AddSingleton<IDirectoryUtility, DirectoryUtility>();

            services.AddDistributedRedisCache(r =>
            {
                r.Configuration = Configuration["redis:ConnectionString"];
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseMiddleware<TokenManagerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
