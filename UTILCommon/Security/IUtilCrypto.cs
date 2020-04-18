using System;
using System.Collections.Generic;
using System.Text;

namespace UTILCommon.Security {

    public interface IUtilCrypto {

        string toSHA512(string str);
    }
}
