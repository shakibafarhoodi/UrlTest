using App.Services;
using Data.Repository;
using Domin.InterFaceRepository;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using web.Tools;

namespace IOC
{
	public class DependencyContainer
	{
		public static void RegisterServices(IServiceCollection services)

		{
			services.AddScoped<IUrlService, UrlService>();
			services.AddScoped<IUrlRepository, UrlRepository>();
			services.AddScoped<IEmailSenderServices, IEmailSenderServices>();
			services.AddScoped<IViewRenderService, ViewRenderService>();
			services.AddSingleton<IRazorViewEngine, RazorViewEngine>();
			services.AddSingleton<ITempDataProvider, SessionStateTempDataProvider>();
            services.AddScoped<IMangerRoleService, MangeRoleService>();
			services.AddScoped<IUserService, UserService>();


        }
    }
}
