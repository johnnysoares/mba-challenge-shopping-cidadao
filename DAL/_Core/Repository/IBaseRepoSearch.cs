using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DAL._Core {

    public interface IBaseRepoSearch<TEntity>: IDisposable where TEntity : class {
        
        Task<TEntity>              carregar(Guid id);
        Task<IEnumerable<TEntity>> query(FilterDefinition<TEntity> filter);
        
    }

}
