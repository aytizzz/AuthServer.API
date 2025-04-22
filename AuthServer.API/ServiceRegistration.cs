using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Configurations;

namespace AuthServer.API
{
    public static class ServiceRegistration
    {
        public static void AddCustomTokenOptionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CustomTokenOption>(configuration.GetSection("TokenOptions"));
        }
    }
}
//Bu kod deyir ki:

//"Ey .NET, TokenOptions adlanan hissəni götür, CustomTokenOption obyektinə çevir və lazım olduqda onu mənə ver."