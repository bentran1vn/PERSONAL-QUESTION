using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace bentran1vn.question.repository
{
    public class StartUp
    {
        private IConfiguration _configuration;

        public StartUp(WebApplicationBuilder builder, IWebHostEnvironment env)
        {
            _configuration = builder.Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "QUEST?ON", Version = "v1" });
            });

            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = false;
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
            });

            //var connectionString = _configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(connectionString);
            //});

            services.AddIdentity<Users, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            AddDI(services);
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var isUserSwagger = _configuration.GetValue<bool>("UseSwagger", false);
            if (isUserSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DefaultModelsExpandDepth(-1);
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "QUEST?ON v1");
                });
            }

            //Adding MiddleWare Here !


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseRouting();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddDI(IServiceCollection services)
        {
            //Adding Services Here !

        }
    }
}
