using System;
using System.IO;

namespace UTILCommon.Extensions {

    public static class ObjectExtensions {


        public static bool Is<T>(this object ToEvaluate) {

            return ToEvaluate is T;

        }

    }

}
