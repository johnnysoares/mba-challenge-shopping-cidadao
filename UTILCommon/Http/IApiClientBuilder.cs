using System.Collections;

namespace UTILCommon.Http {

    public interface IApiClientBuilder {

        ApiClient build(string baseEndpoint, Hashtable headers);
    }
}
