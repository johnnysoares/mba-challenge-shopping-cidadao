using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DAL._Core {

    public interface INoSqlContext : IDisposable {

        void addCommand(Func<Task> func);
        
        Task<int> saveChanges();
        
        IMongoCollection<T> getCollection<T>(string name);
    }

}
