using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Atendimentos.Entities;
using BLL.Pracas.ConstEnums;
using BLL.Pracas.Entities;
using DAL._Core.AcessData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoreLinq;
using UTILCommon.Extensions;

namespace BLL.Atendimentos.Services {

    public interface IBuscadorDados {
        Task migrarCE(PracaConst infoPraca);
    }

    public class BuscadorDados : BuscadorBase, IBuscadorDados {

        //Dependencias
        //
        private ILogger<BuscadorDados> logger;
        
        /// <summary>
        /// Construtor
        /// </summary>
        public BuscadorDados(ILogger<BuscadorDados> _logger,
                             IConfiguration _config):base(_config) {
            
            this.logger = _logger;
            this.qtdeItens = 20000;
        }
        

        public async Task migrarCE(PracaConst infoPraca) {
            
            var praca = infoPraca.pracaCE();

            //var infoBuscaCE = await this.buscarParametros(praca, "dados_adicionais");
            var infoBuscaCE = await this.buscarParametros(praca, "pausas");

            await this.migrar(praca, infoBuscaCE);
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task migrar(Praca itemPraca, SequenciaBusca infoBusca) {
            
            var listaDistinct = await carregarListaAtendimentos(itemPraca, infoBusca);

            if (listaDistinct.Count == 0) {
                return;
            }
            
            this.logger.LogInformation($"SENHAS ENCONTRADAS {itemPraca.nome}: {listaDistinct.Count}");
            
            var cmdUpdate = gerarQueryTempoPausa(listaDistinct);

            if (cmdUpdate.isEmpty()) {
                return;
            }

            //Inserir os registros
            using (var db = new DataContext(config[DataContext.appKeyBI])) {
                db.Connection.Open();
                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText = cmdUpdate.ToString();
                    await cmd.ExecuteNonQueryAsync();
                    this.logger.LogInformation($"UPDATE {itemPraca.nome} {DateTime.Now:dd/MM/yyyy HH:mm:ss} \n\n");
                }
                db.Connection.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private StringBuilder gerarQueryTempoPausa(List<Atendimento> listaDistinct) {
            
            StringBuilder cmdUpdate = new StringBuilder();
            StringBuilder itemUpdate = new StringBuilder();

            foreach (Atendimento itemAtendimento in listaDistinct) {
                
                itemUpdate.Clear();
                itemUpdate.Append("update atendimento_tempo_etapa2 set idUnidade = idUnidade ,");
                
                if (itemAtendimento.tempoTotalPausa.HasValue) {
                    itemUpdate.Append($"tempoTotalPausa = '{itemAtendimento.tempoTotalPausa.Value:hh\\:mm\\:ss}', ");
                }
                
                itemUpdate.Append("idPraca = idPraca ");
                itemUpdate.Append($"where senha = '{itemAtendimento.senha}' AND ");
                itemUpdate.Append($"idUnidade = {itemAtendimento.idUnidade} AND ");
                itemUpdate.Append($"idPraca = {itemAtendimento.idPraca} AND ");
                itemUpdate.Append($"dataBusca = {itemAtendimento.dataBusca}; ");

                cmdUpdate.AppendLine(itemUpdate.ToString());
            }

            return cmdUpdate;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private StringBuilder gerarQuery(List<Atendimento> listaDistinct) {
            
            StringBuilder cmdUpdate = new StringBuilder();
            StringBuilder itemUpdate = new StringBuilder();

            foreach (Atendimento itemAtendimento in listaDistinct) {
                
                itemUpdate.Clear();
                itemUpdate.Append("update atendimento set flagAlteracaoAtividade = flagAlteracaoAtividade ,");
                
                /*
                if (itemAtendimento.idAtividade > 0) {
                    itemUpdate.Append($"idAtividade = {itemAtendimento.idAtividade}, ");
                }
                
                if (itemAtendimento.idSecao > 0) {
                    itemUpdate.Append($"idSecao = {itemAtendimento.idSecao}, ");
                }
                
                if (itemAtendimento.idGuiche > 0) {
                    itemUpdate.Append($"idGuiche = {itemAtendimento.idGuiche}, ");
                }
                
                if (itemAtendimento.idPrioridade > 0) {
                    itemUpdate.Append($"idPrioridade = {itemAtendimento.idPrioridade}, ");
                }
                
                if (itemAtendimento.idAtendente > 0) {
                    itemUpdate.Append($"idAtendente = {itemAtendimento.idAtendente}, ");
                }
                
                if (itemAtendimento.idAvaliacao > 0) {
                    itemUpdate.Append($"idAvaliacao = {itemAtendimento.idAvaliacao}, ");
                }
                
                if (itemAtendimento.idResposta > 0) {
                    itemUpdate.Append($"idResposta = {itemAtendimento.idResposta}, ");
                }
                
                if (itemAtendimento.hrEmissaoSenha.HasValue) {
                    itemUpdate.Append($"hrEmissaoSenha = '{itemAtendimento.hrEmissaoSenha.Value:HH:mm:ss}', ");
                }
                
                if (itemAtendimento.hrAlocacaoGuiche.HasValue) {
                    itemUpdate.Append($"hrAlocacaoGuiche = '{itemAtendimento.hrAlocacaoGuiche.Value:HH:mm:ss}', ");
                }
                
                if (itemAtendimento.hrAlocacaoSecao.HasValue) {
                    itemUpdate.Append($"hrAlocacaoSecao = '{itemAtendimento.hrAlocacaoSecao.Value:HH:mm:ss}', ");
                }
                
                if (itemAtendimento.hrInicioAtendimento.HasValue) {
                    itemUpdate.Append($"hrInicioAtendimento = '{itemAtendimento.hrInicioAtendimento.Value:HH:mm:ss}', ");
                }
                
                if (itemAtendimento.hrFimAtendimento.HasValue) {
                    itemUpdate.Append($"hrFimAtendimento = '{itemAtendimento.hrFimAtendimento.Value:HH:mm:ss}', ");
                }
                
                if (itemAtendimento.hrFinalizacaoSenha.HasValue) {
                    itemUpdate.Append($"hrFinalizacaoSenha = '{itemAtendimento.hrFinalizacaoSenha.Value:HH:mm:ss}', ");
                }                
                */
                itemUpdate.Append("idAtividadeAnterior = idAtividadeAnterior ");
                itemUpdate.Append($"where senha = '{itemAtendimento.senha}' AND ");
                itemUpdate.Append($"idUnidade = {itemAtendimento.idUnidade} AND ");
                itemUpdate.Append($"idPraca = {itemAtendimento.idPraca} AND ");
                itemUpdate.Append($"dataBusca = {itemAtendimento.dataBusca}; ");

                cmdUpdate.AppendLine(itemUpdate.ToString());
            }

            return cmdUpdate;
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task<List<Atendimento>> carregarListaAtendimentos(Praca itemPraca, SequenciaBusca infoBusca) {
            
            var listaItens = new List<Atendimento>();

            using (var db = new DataContext(config[itemPraca.appConex])) {
                db.Connection.Open();

                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText = $"SELECT {itemPraca.id} as idPraca, horario, " +
                                      $"senha, id_unidade, evento, id_atividade, id_secao, id_guiche, id_prioridade, id_atendente, id_avaliacao, id_resposta "+
                                      $"from evento " +
                                      $"where id >= {infoBusca.nroInicio} AND id <= {infoBusca.nroFim} and id_motivo_pausa > 0 order by id_unidade, senha, id;";

                    var result = await cmd.ExecuteReaderAsync();
                    listaItens = await this.readAtendimentos(result);
                }

                db.Connection.Dispose();
            }

            var listaDistinct = this.filtrar(listaItens);

            return listaDistinct;
        }
        
        
        private async Task<List<Atendimento>> readAtendimentos(DbDataReader reader){
            
            var itens = new List<Atendimento>();
            
            using (reader){
                while (await reader.ReadAsync()) {

                    DateTime data = reader.GetDateTime(1);

                    var item = new Atendimento();

                    item.idPraca = reader["idPraca"].stringOrEmpty().toInt();
                    item.idUnidade = reader["id_unidade"].stringOrEmpty().toInt();
                    //item.idAtividade = reader["id_atividade"].stringOrEmpty().toInt();
                    //item.idGuiche = reader["id_guiche"].stringOrEmpty().toInt();
                    //item.idSecao = reader["id_secao"].stringOrEmpty().toInt();
                    //item.idPrioridade = reader["id_prioridade"].stringOrEmpty().toInt();
                    //item.idAtendente = reader["id_atendente"].stringOrEmpty().toInt();
                    //item.idAvaliacao = reader["id_avaliacao"].stringOrEmpty().toInt();
                    //item.idResposta = reader["id_resposta"].stringOrEmpty().toInt();
                    item.evento = reader["evento"].stringOrEmptyLower();
                    item.dtEvento = data;
                    
                    item.dataBusca = data.ToString("yyyyMMdd").toInt();
                    item.dataBusca = data.ToString("yyyyMMdd").toInt();
                    item.mesAnoBusca = data.ToString("yyyyMM").toInt();
                    item.senha = reader["senha"].stringOrEmpty();
                    itens.Add(item);
                }
            }
            return itens;
        }
        
        private List<Atendimento> filtrar(List<Atendimento> listaCompleta) {

            listaCompleta = listaCompleta
                            .Where(x => x.evento == "iniciou pausa atendimento" || 
                                                    x.evento == "finalizou pausa atendimento"
                                                )
                                                .ToList();

            if (!listaCompleta.Any()) {
                this.logger.LogInformation($"NENHUM EVENTO ENCONTRADO \n\n");

                return listaCompleta;
            }
            
            var listaDistinct = listaCompleta.DistinctBy(x => new {
                                                 x.dataBusca,
                                                 x.idPraca,
                                                 x.idUnidade,
                                                 x.senha
                                             })
                                             .ToList();

            foreach (var item in listaDistinct) {
                
                var subListaItem = listaCompleta.Where(x => 
                                                                    x.dataBusca == item.dataBusca &&
                                                                    x.idPraca == item.idPraca && 
                                                                    x.idUnidade == item.idUnidade && 
                                                                    x.senha == item.senha
                                                                ).OrderBy(x => x.dtEvento).ToList();

                var iniciosPausas = subListaItem.Where(x => x.evento == "iniciou pausa atendimento").ToList();
                var fimPausas = subListaItem.Where(x => x.evento == "finalizou pausa atendimento").ToList();
                double totalSeconds = 0;
                
                for (int i = 0; i < iniciosPausas.Count; i++) {
                    
                    var inicio = iniciosPausas[i];

                    if (fimPausas.Count < (i + 1)) {
                        continue;
                    }

                    var fim = fimPausas[i];

                    double seconds = fim.dtEvento.GetValueOrDefault().Subtract(inicio.dtEvento.GetValueOrDefault()).TotalSeconds;

                    totalSeconds = totalSeconds + seconds;
                }

                item.tempoTotalPausa = TimeSpan.FromSeconds(totalSeconds);

                /*
                item.idAtividade = subListaItem.Where(x => x.idAtividade > 0)
                                               .Select(x => x.idAtividade)
                                               .FirstOrDefault();

                item.idSecao = subListaItem.Where(x => x.idSecao > 0)
                                               .Select(x => x.idSecao)
                                               .FirstOrDefault();

                item.idGuiche = subListaItem.Where(x => x.idGuiche > 0)
                                           .Select(x => x.idGuiche)
                                           .FirstOrDefault();

                item.idPrioridade = subListaItem.Where(x => x.idPrioridade > 0)
                                            .Select(x => x.idPrioridade)
                                            .FirstOrDefault();

                
                item.idAvaliacao = subListaItem.Where(x => x.idAvaliacao > 0)
                                               .Select(x => x.idAvaliacao)
                                               .FirstOrDefault();
                
                item.idResposta = subListaItem.Where(x => x.idResposta > 0)
                                               .Select(x => x.idResposta)
                                               .FirstOrDefault();
                
                item.idAtendente = subListaItem.Where(x => x.idAtendente > 0 && (x.evento == "iniciou o atendimento"))
                                               .Select(x => x.idAtendente)
                                               .FirstOrDefault();

                item.hrEmissaoSenha = subListaItem.Where(x => x.evento == "emitiu a senha")
                                               .Select(x => x.dtEvento)
                                               .FirstOrDefault();
                
                item.hrAlocacaoGuiche = subListaItem.Where(x => x.evento == "alocou a senha no guichê")
                                                  .Select(x => x.dtEvento)
                                                  .FirstOrDefault();                

                item.hrAlocacaoSecao = subListaItem.Where(x => x.evento == "alocou a senha na seção")
                                                   .Select(x => x.dtEvento)
                                                   .FirstOrDefault();

                item.hrInicioAtendimento = subListaItem.Where(x => x.evento == "iniciou o atendimento")
                                                       .Select(x => x.dtEvento)
                                                       .FirstOrDefault();
                
                item.hrFimAtendimento = subListaItem.Where(x => x.evento == "finalizou o atendimento")
                                                       .Select(x => x.dtEvento)
                                                       .FirstOrDefault();
                                
                item.hrFinalizacaoSenha = subListaItem.Where(x => x.evento == "finalizou a senha")
                                                    .Select(x => x.dtEvento)
                                                    .FirstOrDefault();
                                                    */
            }

            return listaDistinct;
        }        

    }

}
