using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using BLL.Atendimentos.Services;
using BLL.Pracas.ConstEnums;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BLL.Atendimentos.Jobs {

    [DisallowConcurrentExecution]
    public class JobBuscadorSenhaRJ : IJob {

        //Dependencias
        private ILogger<JobBuscadorSenhaRJ> Logger;
        private readonly IServiceProvider ServiceProvider;

        /// <summary>
        /// Construtor
        /// </summary>
        public JobBuscadorSenhaRJ(ILogger<JobBuscadorSenhaRJ> _logger,
                                  IServiceProvider _provider) {

            Logger = _logger;

            ServiceProvider = _provider;

        }

        /// <summary>
        /// 
        /// </summary>
        public async Task Execute(IJobExecutionContext context) {
            
            // Create a new scope
            using(var scope = ServiceProvider.CreateScope()){

                try {
                    this.Logger.LogInformation($"Run RJ {DateTime.Now:dd/MM/yyyy HH:mm}");

                    var buscador = scope.ServiceProvider.GetService<IBuscadorSenha>();

                    await buscador.migrarRJ(new PracaConst());

                }catch(Exception ex) {

                    this.Logger.LogError(ex, "Problemas ao migrar senhas");
                }


            }

        }
        
    }
}
