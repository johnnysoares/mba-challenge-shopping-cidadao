using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using BLL.Atividades.Entities;
using BLL.Pracas.ConstEnums;
using BLL.Pracas.Entities;
using DAL._Core.AcessData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace BLL.Atividades.Services {

    public interface IAtividadeMigracao {
        
        Task migrarTudo();

        Task limparTabela();

        Task migrar(Praca itemPraca);
    }

    public class AtividadeMigracao : IAtividadeMigracao {

        //
        private readonly IConfiguration config;
        private ILogger<AtividadeMigracao> logger;
        
        /// <summary>
        /// Construtor
        /// </summary>
        public AtividadeMigracao(ILogger<AtividadeMigracao> _logger, IConfiguration _config) {
            this.config = _config;
            this.logger = _logger;
        }

        public async Task migrarTudo() {

            await this.limparTabela();
            
            var infoPraca = new PracaConst();
            
            await migrar(infoPraca.pracaCE());
            
            await migrar(infoPraca.pracaMG());
            
            await migrar(infoPraca.pracaRJ());

        }
        
        public async Task limparTabela() {

            using (var db = new DataContext(config[DataContext.appKeyBI])) {
                db.Connection.Open();
                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText = @"DELETE FROM atividade;";
                    await cmd.ExecuteNonQueryAsync();
                }
                db.Connection.Dispose();
            }
        }
        
        public async Task migrar(Praca itemPraca) {
            
            var dataTable = new DataTable();
            
            using (var db = new DataContext(config[itemPraca.appConex])) {
                db.Connection.Open();
                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText = $"SELECT 0 as idNovo, id, {itemPraca.id} as idPraca, nome FROM atividade;";
                    var result = await cmd.ExecuteReaderAsync();
                    dataTable.Load(result);
                }
                db.Connection.Dispose();
            }
            
            this.logger.LogInformation($"ATIVIDADES ENCONTRADAS {itemPraca.nome}: {dataTable.Rows.Count}");

            if (dataTable.Rows.Count == 0) {
                return;
            }
            
            using (var connection = new MySqlConnection($"{config[DataContext.appKeyBI]};AllowLoadLocalInfile=True")){
                await connection.OpenAsync();
                var bulkCopy = new MySqlBulkCopy(connection);
                bulkCopy.DestinationTableName = "atividade";
                await bulkCopy.WriteToServerAsync(dataTable);
            }
            
        }
        
        private async Task<List<Atividade>> readAllAsync(DbDataReader reader){
            var itens = new List<Atividade>();
            using (reader){
                while (await reader.ReadAsync()) {
                    var item = new Atividade {
                        id = reader.GetInt64(0),
                        nome = reader.GetString(1)
                    };
                    itens.Add(item);
                }
            }
            return itens;
        }
    }

}