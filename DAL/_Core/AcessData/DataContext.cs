using System;
using MySql.Data.MySqlClient;

namespace DAL._Core.AcessData {

    public class DataContext : IDisposable {
        
        public const string appKeyCE = "ConnectionStrings:databaseCE";
        public const string appKeyRJ = "ConnectionStrings:databaseRJ";
        public const string appKeyMG = "ConnectionStrings:databaseMG";
        public const string appKeyBI = "ConnectionStrings:databaseBI";
        public const string appKeyBIRemoto = "ConnectionStrings:databaseBIRemoto";
            
        public MySqlConnection Connection { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        public DataContext(string connectionString){
            
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();        
    }

}