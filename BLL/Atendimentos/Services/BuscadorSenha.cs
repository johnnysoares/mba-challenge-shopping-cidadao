using System;
using System.Collections.Generic;
using System.Data;
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

    public interface IBuscadorSenha {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task migrarRJ(PracaConst infoPraca);
        
        Task migrarCE(PracaConst infoPraca);
        
        Task migrarMG(PracaConst infoPraca);
    }

    public class BuscadorSenha : IBuscadorSenha {

        //Dependencias
        //
        private readonly IConfiguration config;
        private ILogger<BuscadorSenha> logger;
        private int qtdeItens = 3000;
        
        /// <summary>
        /// Construtor
        /// </summary>
        public BuscadorSenha(ILogger<BuscadorSenha> _logger, IConfiguration _config) {
            this.config = _config;
            this.logger = _logger;
        }

        
        public async Task migrarRJ(PracaConst infoPraca) {
            
            var praca = infoPraca.pracaRJ();

            var infoBuscaRJ = await this.buscarParametros(praca);

            await this.migrar(praca, infoBuscaRJ);
        }

        public async Task migrarCE(PracaConst infoPraca) {
            
            var praca = infoPraca.pracaCE();

            var infoBuscaCE = await this.buscarParametros(praca);

            await this.migrar(praca, infoBuscaCE);
        }
        
        public async Task migrarMG(PracaConst infoPraca) {
            
            var praca = infoPraca.pracaMG();

            var infoBuscaMG = await this.buscarParametros(praca);

            await this.migrar(praca, infoBuscaMG);
        }
        private async Task<SequenciaBusca> buscarParametros(Praca itemPraca) {
            
            var lista = new List<SequenciaBusca>();
            
            using (var db = new DataContext(config[DataContext.appKeyBI])) {
                db.Connection.Open();
                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText = $"SELECT id, idPraca, nroInicio, nroFim FROM sequencia_busca where idPraca = {itemPraca.id} and idTipo = 'senha' order by id desc limit 2;";
                    var result = await cmd.ExecuteReaderAsync();
                    lista = await readParametros(result);
                }
                db.Connection.Dispose();
            }
            
            var infoBusca = lista.FirstOrDefault() ?? new SequenciaBusca();
            infoBusca.idPraca = itemPraca.id;
            infoBusca.nroInicio = infoBusca.nroFim + 1;
            infoBusca.nroFim = (infoBusca.nroInicio + this.qtdeItens) - 1;
            infoBusca.idTipo = "senha";
            infoBusca.dtPesquisa = DateTime.Now;
            
            //Inserir os registros
            using (var db = new DataContext(config[DataContext.appKeyBI])) {
                db.Connection.Open();
                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText = $"INSERT INTO sequencia_busca (idPraca, idTipo, nroInicio, nroFim, dtPesquisa) VALUES ({infoBusca.idPraca}, '{infoBusca.idTipo}', {infoBusca.nroInicio}, {infoBusca.nroFim}, '{infoBusca.dtPesquisa:yyyy-MM-dd HH:mm:ss}');";
                    await cmd.ExecuteNonQueryAsync();
                }
                db.Connection.Dispose();
            }

            return infoBusca;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private async Task migrar(Praca itemPraca, SequenciaBusca infoBusca) {
            
            var listaDistinct = await carregarListaAtendimentos(itemPraca, infoBusca);

            if (listaDistinct.Count == 0) {
                return;
            }
            
            var cmdInsert = await tratarDuplicados(listaDistinct);

            if (cmdInsert.isEmpty()) {
                return;
            }

            //Inserir os registros
            using (var db = new DataContext(config[DataContext.appKeyBI])) {
                db.Connection.Open();
                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText = cmdInsert.ToString();
                    await cmd.ExecuteNonQueryAsync();
                    this.logger.LogInformation($"INSERT {itemPraca.nome} {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                }
                db.Connection.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task<StringBuilder> tratarDuplicados(List<Atendimento> listaDistinct) {
            StringBuilder cmdInsert = new StringBuilder();

            using (var db = new DataContext(config[DataContext.appKeyBI])) {
                db.Connection.Open();

                using (var cmd = db.Connection.CreateCommand()) {
                    foreach (Atendimento itemAtendimento in listaDistinct) {
                        cmd.CommandText = $"SELECT id from atendimento where dataBusca = {itemAtendimento.dataBusca} AND idPraca = {itemAtendimento.idPraca} AND idUnidade = {itemAtendimento.idUnidade} AND senha = '{itemAtendimento.senha}';";

                        var result = await cmd.ExecuteReaderAsync();

                        if (!result.HasRows) {
                            string sql = $"INSERT into atendimento (idPraca, idUnidade, senha, dataBusca, mesAnoBusca) VALUES ({itemAtendimento.idPraca}, {itemAtendimento.idUnidade}, '{itemAtendimento.senha}', {itemAtendimento.dataBusca}, {itemAtendimento.mesAnoBusca});";
                            cmdInsert.AppendLine(sql);
                        }

                        result.Close();
                    }
                }

                db.Connection.Dispose();
            }

            return cmdInsert;
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task<List<Atendimento>> carregarListaAtendimentos(Praca itemPraca, SequenciaBusca infoBusca) {
            
            var listaItens = new List<Atendimento>();

            using (var db = new DataContext(config[itemPraca.appConex])) {
                db.Connection.Open();

                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText =
                        $"SELECT {itemPraca.id} as idPraca, DATE(horario) AS data_senha, senha, id_unidade from evento where id > {infoBusca.nroInicio} AND id <= {infoBusca.nroFim};";

                    var result = await cmd.ExecuteReaderAsync();
                    listaItens = await this.readAtendimentos(result);
                }

                db.Connection.Dispose();
            }

            this.logger.LogInformation($"SENHAS ENCONTRADAS {itemPraca.nome}: {listaItens.Count}");

            var listaDistinct = listaItens.DistinctBy(x => new {
                                              x.dataBusca,
                                              x.idPraca,
                                              x.idUnidade,
                                              x.senha
                                          })
                                          .ToList();

            this.logger.LogInformation($"SENHAS ENCONTRADAS ÚNICAS {itemPraca.nome}: {listaDistinct.Count}");

            return listaDistinct;
        }

        private async Task<List<SequenciaBusca>> readParametros(DbDataReader reader){
            var itens = new List<SequenciaBusca>();
            using (reader){
                while (await reader.ReadAsync()) {
                    var item = new SequenciaBusca {
                        id = reader.GetInt32(0),
                        idPraca = reader.GetInt32(1),
                        nroInicio = reader.GetInt32(2),
                        nroFim = reader.GetInt32(3)
                    };
                    itens.Add(item);
                }
            }
            return itens;
        }
        
        private async Task<List<Atendimento>> readAtendimentos(DbDataReader reader){
            var itens = new List<Atendimento>();
            using (reader){
                while (await reader.ReadAsync()) {

                    DateTime data = reader.GetDateTime(1);

                    var item = new Atendimento();

                    item.idPraca = reader["idPraca"].stringOrEmpty().toInt();
                    item.idUnidade = reader["id_unidade"].stringOrEmpty().toInt();
                    item.dataBusca = data.ToString("yyyyMMdd").toInt();
                    item.mesAnoBusca = data.ToString("yyyyMM").toInt();
                    item.senha = reader["senha"].stringOrEmpty();
                    itens.Add(item);
                }
            }
            return itens;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private async Task<DataTable> loadDataTable(DbDataReader reader, DataTable dataTable){
            
            using (reader){
            
                while (await reader.ReadAsync()) {
                    
                    string dataSenha = reader["data_senha"].stringOrEmpty();
                    
                    var itemRow = dataTable.NewRow();
                    itemRow["idPraca"] = reader["idPraca"];
                    itemRow["idUnidade"] = reader["id_unidade"];
                    itemRow["dataBusca"] = dataSenha.onlyNumber();
                    itemRow["mesAnoBusca"] = dataSenha.Substring(0, 6);
                    itemRow["senha"] = reader["senha"];
                    
                    dataTable.Rows.Add(itemRow);
                }
            }
            return dataTable;
        }
    }

}
