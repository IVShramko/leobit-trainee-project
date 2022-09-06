using Dating.Chat.Facades;
using Dating.Chat.Hubs;
using Dating.Chat.Repositories.StatusRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Chat
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
            services.AddControllers();

            services.AddCors(options => 
            {
                options.AddDefaultPolicy(policy => 
                {
                    policy.AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader();
                });
            });

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
                config.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (!String.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSignalR();

            services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddScoped<IStatusFacade, StatusFacade>();

            services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(options =>
            {
                string connectionString = Configuration.GetConnectionString("RedisConnection");
                var multiplexer = ConnectionMultiplexer.Connect(connectionString);

                return multiplexer;
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                string chatEndpoint = Configuration["Hubs:ChatHub"];
                endpoints.MapHub<ChatHub>(chatEndpoint);

                string statusEndpoint = Configuration["Hubs:StatusHub"];
                endpoints.MapHub<StatusHub>(statusEndpoint);

                string notificationEndpoint = Configuration["Hubs:NotificationHub"];
                endpoints.MapHub<NotificationHub>(notificationEndpoint);

            });
        }
    }
}
