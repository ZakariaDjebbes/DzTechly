using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddCors(opt =>
           {
               opt.AddPolicy("CorsPolicy", policy =>
               {
                   policy
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   .WithOrigins("https://localhost:4200").WithOrigins("http://127.0.0.1:5500");
               });
           });

            services.AddDbContext<StoreContext>(ctx =>
            {
                ctx.UseSqlite(_config.GetConnectionString("DefaultConnection"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });

	        services.AddSingleton<IConnectionMultiplexer>(c => {
				var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
				return ConnectionMultiplexer.Connect(configuration);
			});

            services.AddIdentityService(_config);
            services.AddSwaggerDocumentation();
            services.AddApplicationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSwaggerDocumentation();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
