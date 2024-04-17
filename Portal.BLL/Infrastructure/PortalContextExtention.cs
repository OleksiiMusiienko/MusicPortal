using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portal.DAL.EF;

namespace Portal.BLL.Infrastructure
{
    public static class PortalContextExtention
    {
        public static void AddPortalContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<PortalContext>(options => options.UseSqlServer(connection));
        }
    }
}
