using Microsoft.Extensions.DependencyInjection;
using Portal.DAL.Interfaces;
using Portal.DAL.Repositories;

namespace Portal.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
