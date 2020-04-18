using System;
using System.Security.Cryptography;
using System.Text;

namespace UTILCommon.Security {

    public class UtilCrypto : IUtilCrypto {

        //Propriedades
        public static readonly string saltSistema = "SopocX532lo889!##XYZ";

        /// <summary>
        /// Construtor
        /// </summary>
        public UtilCrypto() {

        }

        //Devolve uma string sem acentos
        public string toSHA512(string str) {

            string cryprStr = String.Concat(saltSistema, str);
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(cryprStr);

            SHA512Managed SHhash = new SHA512Managed();
            HashValue = SHhash.ComputeHash(MessageBytes);

            string strHex = "";
            foreach (byte b in HashValue) {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }

    }
}
