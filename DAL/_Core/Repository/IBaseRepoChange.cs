using System;
using System.Threading.Tasks;

namespace DAL._Core {

    public interface IBaseRepoChange<in TEntity>: IDisposable where TEntity : class {
        
        void insert(TEntity Registro);
        
        void update(TEntity Registro);
    }

}
