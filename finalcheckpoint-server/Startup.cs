using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using finalcheckpoint_server.Services;
using finalcheckpoint_server.Repositories;
using Microsoft.OpenApi.Models;

namespace finalcheckpoint_server
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
            // REVIEW[epic=Authentication] creates functionality for authentication
            services.AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              }).AddJwtBearer(options =>
              {
                  options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                  options.Audience = Configuration["Auth0:Audience"];
              });
            services.AddCors(options =>
              {
                  options.AddPolicy("CorsDevPolicy", builder =>
                {
                    builder
                        .WithOrigins(new string[]{
                            "http://localhost:8080",
                            "http://localhost:8081"
                            })
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
              });

            // TODO Transient Repositories
            services.AddTransient<VaultsRepository>();
            services.AddTransient<VaultKeepsRepository>();
            services.AddTransient<ProfilesRepository>();
            services.AddTransient<KeepsRepository>();


            // TODO Transient Services
            services.AddTransient<VaultsService>();
            services.AddTransient<VaultKeepsService>();
            services.AddTransient<ProfilesService>();
            services.AddTransient<KeepsService>();


            services.AddControllers();

            services.AddScoped<IDbConnection>(x => CreateDbConnection());


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "finalcheckpoint_server", Version = "v1" });
            });
        }

        private IDbConnection CreateDbConnection()
        {
            var connectionString = Configuration["db:gearhost"];
            return new MySqlConnection(connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "finalcheckpoint_server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // REVIEW[epic=Authentication] runs authentication process
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
