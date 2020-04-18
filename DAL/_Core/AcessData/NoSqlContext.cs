using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace DAL._Core {

    public class NoSqlContext : INoSqlContext {
        
        //Dependencies
        private IMongoDatabase Database { get; set; }
        private readonly List<Func<Task>> listCommands;

        /// <summary>
        /// Construtor
        /// </summary>
        public NoSqlContext(IConfiguration _configuration) {
            
            // Set Guid to CSharp style (with dash -)
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            // Every command will be stored and it'll be processed at SaveChanges
            listCommands = new List<Func<Task>>();

            registerConventions();

            // Configure mongo (You can inject the config, just to simplify)
            var conex = _configuration.GetSection("MongoSettings")
                                        .GetSection("Connection")
                                        .Value;

            var database = _configuration.GetSection("MongoSettings")
                                            .GetSection("DatabaseName")
                                            .Value;
            
            var noSqlClient = new MongoClient(conex);
            
            this.Database = noSqlClient.GetDatabase(database);
        }

        private void registerConventions(){
            
            var pack = new ConventionPack{
                           new IgnoreExtraElementsConvention(true),
                           new IgnoreIfDefaultConvention(true)
                       };
            ConventionRegistry.Register("Default_Conventions", pack, t => true);
            
        }        

        /// <summary>
        /// Adicionar comando na fila de execucao
        /// </summary>
        public void addCommand(Func<Task> func) {
            
            listCommands.Add(func);
            
        }

        /// <summary>
        /// Enviar comandos para NoSQL
        /// </summary>
        public async Task<int> saveChanges() {
            
            var commandTasks = listCommands.Select(c => c());

            await Task.WhenAll(commandTasks);

            return listCommands.Count;
        }

        /// <summary>
        /// Carregar uma colecao do banco NoSql
        /// </summary>
        public IMongoCollection<T> getCollection<T>(string name) {
            
            return Database.GetCollection<T>(name);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Dispose(){
            
            GC.SuppressFinalize(this);
        }        
    }

}
