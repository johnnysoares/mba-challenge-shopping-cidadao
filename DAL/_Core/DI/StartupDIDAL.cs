using Microsoft.Extensions.DependencyInjection;

namespace DAL._Core.DI {

    public class StartupDIDAL {

        public static void register(IServiceCollection services) {

            services.AddScoped<INoSqlContext, NoSqlContext>();
            services.AddScoped<IUnitOfWorkNoSql, UnitOfWorkNoSql>();
            
            //AuthorizationDI.register(services);
        }
    }
}
