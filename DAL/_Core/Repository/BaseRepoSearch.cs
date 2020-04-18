using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DAL._Core {

    public abstract class BaseRepoSearch<TEntity>: IBaseRepoSearch<TEntity> where TEntity : class {
        
        //Dependencias
        protected readonly INoSqlContext             Context;
        protected readonly IMongoCollection<TEntity> DbSet;
        
        /// <summary>
        /// Construtor
        /// </summary>
        protected BaseRepoSearch(INoSqlContext _context){
            
            Context = _context;
            
            DbSet = _context.getCollection<TEntity>(typeof(TEntity).Name);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual async Task<TEntity> carregar(Guid id) {
            
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            
            return data.SingleOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual async Task<IEnumerable<TEntity>> query(FilterDefinition<TEntity> filter) {
            
            var all = await DbSet.FindAsync(filter);
            
            return all.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            
            GC.SuppressFinalize(this);
        }
    }

}
