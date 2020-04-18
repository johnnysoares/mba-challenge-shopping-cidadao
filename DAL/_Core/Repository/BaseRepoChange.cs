using System;
using MongoDB.Driver;

namespace DAL._Core {

    public abstract class BaseRepoChange<TEntity>: IBaseRepoChange<TEntity> where TEntity : class {
        
        //Dependencias
        protected readonly INoSqlContext             Context;
        protected readonly IMongoCollection<TEntity> DbSet;
        
        /// <summary>
        /// Construtor
        /// </summary>
        protected BaseRepoChange(INoSqlContext _context){
            
            Context = _context;
            
            DbSet = _context.getCollection<TEntity>(typeof(TEntity).Name);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void insert(TEntity Registro) {
            
            Context.addCommand(() => DbSet.InsertOneAsync(Registro));
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void update(TEntity Registro) {
            
            //Context.addCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", Registro.GetId()), Registro));
        }

        public void Dispose() {
            
            GC.SuppressFinalize(this);
        }
        
    }

}
