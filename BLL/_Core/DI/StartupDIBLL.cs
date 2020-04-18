using BLL.Atendimentos.Services;
using BLL.Atividades.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL._Core.DI {

    public class StartupDIBLL {

        public static void register(IServiceCollection services) {

            services.AddTransient<IAtividadeMigracao, AtividadeMigracao>();
            services.AddTransient<IBuscadorSenha, BuscadorSenha>();
            services.AddTransient<IBuscadorDados, BuscadorDados>();
        }
    }
}
