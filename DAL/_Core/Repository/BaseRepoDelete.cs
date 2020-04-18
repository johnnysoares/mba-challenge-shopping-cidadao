using System;
using MongoDB.Driver;

namespace DAL._Core {

    public abstract class BaseRepoDelete<TEntity>: IBaseRepoDelete<TEntity> where TEntity : class {
        
        //Dependencias
        protected readonly INoSqlContext Context;
        protected readonly IMongoCollection<TEntity> DbSet;
        
        /// <summary>
        /// Construtor
        /// </summary>
        protected BaseRepoDelete(INoSqlContext _context){
            
            Context = _context;
            
            DbSet = _context.getCollection<TEntity>(typeof(TEntity).Name);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void remove(Guid id) {
            
            this.Context.addCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            
            GC.SuppressFinalize(this);
        }
    }

}
