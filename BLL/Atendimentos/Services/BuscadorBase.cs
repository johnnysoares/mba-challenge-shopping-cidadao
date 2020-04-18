using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using BLL.Atendimentos.Entities;
using BLL.Pracas.Entities;
using DAL._Core.AcessData;
using Microsoft.Extensions.Configuration;

namespace BLL.Atendimentos.Services {

    public abstract class BuscadorBase {

        //Dependencias
        //
        protected readonly IConfiguration config;
        protected int qtdeItens { get; set; }
        
        /// <summary>
        /// Construtor
        /// </summary>
        protected BuscadorBase(IConfiguration _config) {
            this.config = _config;
        }

        protected async Task<SequenciaBusca> buscarParametros(Praca itemPraca, string idTipoBusca) {
            
            var lista = new List<SequenciaBusca>();
            
            using (var db = new DataContext(config[DataContext.appKeyBI])) {
                db.Connection.Open();
                using (var cmd = db.Connection.CreateCommand()) {
                    cmd.CommandText = $"SELECT id, idPraca, nroInicio, nroFim FROM sequencia_busca where idPraca = {itemPraca.id} and idTipo = '{idTipoBusca}' order by id desc limit 2;";
                    var result = await cmd.ExecuteReaderAsync();
                    lista = await readParametros(result);
                }
                db.Connection.Dispose();
            }
            
            var infoBusca = lista.FirstOrDefault() ?? new SequenciaBusca();
            infoBusca.idPraca = itemPraca.id;
            infoBusca.nroInicio = infoBusca.nroFim + 1;
            infoBusca.nroFim = (infoBusca.nroInicio + this.qtdeItens) - 1;
            infoBusca.idTipo = idTipoBusca;
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

    }

}
