using System.Threading.Tasks;

namespace UTILCommon.Cache.Distributed {

    public interface ICacheService {

        Task set(string key, object dataCache, int minutesCache = 5);

        Task<T> get<T>(string key);
    }
}
