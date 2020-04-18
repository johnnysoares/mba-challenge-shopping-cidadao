using System;
using System.Text;

namespace UTILCommon.Extensions.Default {

    public static class CryptExtensions {

        /// <summary>
        /// 
        /// </summary>
        public static string toBase64Encode(this string base64Decoded) {

            string base64Encoded;

            byte[] data = ASCIIEncoding.ASCII.GetBytes(base64Decoded);

            base64Encoded = Convert.ToBase64String(data);

            return base64Encoded;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string toBase64Decode(this string base64Encoded) {

            byte[] data = Convert.FromBase64String(base64Encoded);
            
            string base64Decoded = ASCIIEncoding.ASCII.GetString(data);

            return base64Decoded;
        }

    }
}
