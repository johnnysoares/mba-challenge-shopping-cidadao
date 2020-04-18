using System.Threading.Tasks;

namespace DAL._Core {

    public class UnitOfWorkNoSql : IUnitOfWorkNoSql {

        //Dependencias
        private readonly INoSqlContext context;

        /// <summary>
        /// Construtor
        /// </summary>
        public UnitOfWorkNoSql(INoSqlContext _context) {
            this.context = _context;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<bool> commit(){
            
            var changeAmount = await context.saveChanges();

            return changeAmount > 0;
        }
    }

}
