using App.Services;
using Data.Repository;
using Domin.InterFaceRepository;
using Microsoft.Extensions.DependencyInjection;

namespace IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)

        {
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IUrlRepository, UrlRepository>();
        }
    }
}
