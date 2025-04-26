using AuthServer.Core.Configurations;
using AuthServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using AuthServer.Core.Services;
using AuthServer.Core.UnitofWork;
using AuthServer.Data.Context;
using AuthServer.Data.Repositories;
using AuthServer.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Configurations;
using AuthServer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AuthServer.API
{
    public static class ServiceRegistration
    {
        public static void AddCustomTokenOptionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, IUnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            services.Configure<CustomTokenOption>(configuration.GetSection("TokenOptions"));
            services.Configure<List<Client>>(configuration.GetSection("Clients"));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("AuthServer.Data");
                });
            });


            services.AddIdentity<UserApp, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            // bir token geldikde onu dogrulamaq-bir endpointe istek yapilacak ve onda dogrulama islemi gerceklesdirelecek



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();
                opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = tokenOptions.Issuer,
                    ValidateAudience = tokenOptions.Audience[0],
                    IssuerSigningKey = SignService.getsymmericsecuritykey
                };







            });









        }
    }
}
//Bu kod deyir ki:

//"Ey .NET, TokenOptions adlanan hissəni götür, CustomTokenOption obyektinə çevir və lazım olduqda onu mənə ver."