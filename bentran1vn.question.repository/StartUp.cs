using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Middlewares;
using bentran1vn.question.src.Repositories.PublicQuestion;
using bentran1vn.question.src.Repositories.QuestionAnswer;
using bentran1vn.question.src.Repositories.RefreshToken;
using bentran1vn.question.src.Repositories.User;
using bentran1vn.question.src.Repositories.UserQuestion;
using bentran1vn.question.src.Services.PublicQuestion;
using bentran1vn.question.src.Services.QuestionAnswer;
using bentran1vn.question.src.Services.RefreshToken;
using bentran1vn.question.src.Services.User;
using bentran1vn.question.src.Services.UserQuestion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace bentran1vn.question.repository
{
    public class StartUp
    {
        private readonly IConfiguration _configuration;

        public StartUp(WebApplicationBuilder builder, IWebHostEnvironment env)
        {
            _configuration = builder.Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            //services.AddControllers().AddNewtonsoftJson();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            AddDI(services);
            services.AddSwaggerGen(o =>
            {
                //swagger profile
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "QUEST?ON", Version = "v1" });

                //Swagger Security Information
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bear Scheme." + Environment.NewLine +
                        "Enter 'Bearer' [space] and then your token in the text input below."
                        + Environment.NewLine + "Example: 'Bearer 12345abcef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            },
                            Scheme = "oauth2",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = false;
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
            });

            services.AddIdentity<Users, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                //lưu token ở trong HttpCotext
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = _configuration.GetValue<string>("JWT:ValidAudience"),
                    ValidIssuer = _configuration.GetValue<string>("JWT:ValidIssuer"),
                    //Nơi chung cấp cái token này, localhost !
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Secret"))),
                };
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/error");
            //    //endpoint if needed
            //    app.UseHsts();
            //}
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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //Adding MiddleWare Here !

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.MapControllers();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void AddDI(IServiceCollection services)
        {
            //Adding Services Here !
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserQuestionRepository, UserQuestionRepository>();
            services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
            services.AddScoped<IPublicQuestionRepository, PublicQuestionRepository>();

            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IRefreshTokenServices, RefreshTokenServices>();
            services.AddScoped<IUserQuestionServices, UserQuestionServices>();
            services.AddScoped<IQuestionAnswerServices, QuestionAnswerServices>();
            services.AddScoped<IPublicQuestionServices, PublicQuestionServices>();

            services.AddTransient<IStartupFilter, RequestPipelineStartupFilter>();
        }
    }
}
