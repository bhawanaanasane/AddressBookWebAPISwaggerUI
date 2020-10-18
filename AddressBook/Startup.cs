using AddressBook.AutoMapper;
using AddressBook.DataBase;
using AddressBook.DataBase.Repository;
using AddressBook.Services.CommonServices;
using AddressBook.Services.LoginServices;
using AddressBook.Services.UserServices;
using AddressBook.ViewModel;
using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AddressBook
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
            AddAutoMapper(services);
            services.AddControllers();
            services.AddDbContext<dbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //Add Swagger service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = @"JWT Authorization header using the bearer scheme.\r\n\r\n
                                Enter 'Bearer' [space] and then token in the text input below.
                                    \r\n\r\n Example: 'Bearer 12345qwerty'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
            });
            //Add Repository service
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            //Add User service
            services.AddScoped<IUserService, UserService>();
            //Add Address book service
            services.AddScoped<IAddressBookService, AddressBookService>();
            //Add Common Services scope
            services.AddScoped<ICommonService, CommonService>();
            //Add login Services
            services.AddScoped<ILoginService, LoginService>();



            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(a => a.SwaggerEndpoint("/swagger/v1/swagger.json", "AddressBook Api"));

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        protected virtual void AddAutoMapper(IServiceCollection services)
        {


            //create AutoMapper configuration
            var mappingProfile = new AddressBookMappingConfiguration();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(mappingProfile);
            });

            //register AutoMapper
            services.AddAutoMapper();


            //register
            AutoMapperConfiguration.Init(config);
        }
    }
}
