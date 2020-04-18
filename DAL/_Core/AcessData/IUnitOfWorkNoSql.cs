using System.Threading.Tasks;

namespace DAL._Core {

    public interface IUnitOfWorkNoSql {

        Task<bool> commit();
    }

}
