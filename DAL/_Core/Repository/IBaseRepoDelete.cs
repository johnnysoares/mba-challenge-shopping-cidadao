using System;
using System.Threading.Tasks;

namespace DAL._Core {

    public interface IBaseRepoDelete<in TEntity>: IDisposable where TEntity : class {
        
        void remove(Guid id);
    }

}
