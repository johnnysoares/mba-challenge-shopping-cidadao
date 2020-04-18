using System.Threading.Tasks;
using UTILCommon.Models;

namespace UTILCommon.Validators {

    public interface IValidadorDuplicidade<TEntity> where TEntity: class {

        Task<ResponseModel<TEntity>> verificarDuplicidade(TEntity Item);
        
        Task<ResponseModel<TEntity>> verificarDuplicidade(long idReferencia);
    }

}
