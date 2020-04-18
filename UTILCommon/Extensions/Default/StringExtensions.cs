using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UTILCommon.Extensions {

    public static class StringExtensions {

        // Cpaturar a string e joga-la dentro de uma lista
        public static string TrimNullable(this string strValue) {
            if (strValue == null) {
                return "";
            }

            return strValue.Trim();
        }

        public static string onlyNumber(this string str) {
            return (str == null) ? "" : Regex.Replace(str, @"[^\d]", String.Empty);
        }

        //
        public static string onlyDecimalNumber(this string str) {
            return (str == null) ? "" : Regex.Replace(str, @"[^\d.,]", String.Empty);
        }

        //
        public static string onlyAlphaNumber(this string str) {
            return (str == null) ? "" : Regex.Replace(str, @"[^\w\s]", String.Empty);
        }

        //
        public static string onlyEmailChars(string str) {
            return (str == null) ? "" : Regex.Replace(str, @"[^a-zA-Z0-9@._]", String.Empty);
        }

        //Extensao para facilitar a conversao de int nullable
        public static string removerAcentuacao(this string str) {

            if (string.IsNullOrEmpty(str)) {
                return String.Empty;
            }

            str = str.TrimStart()
                  .TrimEnd();

            byte[] bytes = Encoding.GetEncoding("iso-8859-8")
                                .GetBytes(str);

            return Encoding.UTF8.GetString(bytes);

        }

        //Extensao para facilitar a conversao de int nullable
        public static string removerCaracterEspecial(this string value) {

            value = value.Replace("/", "")
                      .Replace(".", "")
                      .Replace(",", "");

            return value;
        }

        //Devolve um vazio caso a string seja nula
        public static string stringOrEmpty(this object str) {
            str = str ?? "";

            return str.ToString()
                   .Trim();
        }

        //Devolve um vazio caso a string seja nula em uppercase
        public static string stringOrEmptyUpper(this object str) {
            str = str ?? "";

            return str.ToString()
                   .Trim()
                   .ToUpper();
        }

        //Devolve um vazio caso a string seja nula em lowercase
        public static string stringOrEmptyLower(this object str) {
            str = str ?? "";

            return str.ToString()
                   .Trim()
                   .ToLower();
        }

        //Devolve um vazio caso a string seja nula
        public static string removeTags(this string str) {

            return (str == null) ? "" : Regex.Replace(str, "<.*?>", String.Empty);

        }

        // 1 - Abreviar string
        public static string abreviar(this string str, int qtdeCaracteres, string strSufixo = "") {
            if (str.isEmpty()) {
                return String.Empty;
            }

            strSufixo.stringOrEmpty();

            if (str.Length > qtdeCaracteres) {
                str = str.Substring(0, (qtdeCaracteres - strSufixo.Length));

                if (!strSufixo.isEmpty()) {
                    str = String.Concat(str, strSufixo);
                }
            }

            return str;
        }

        // Cpaturar a string e joga-la dentro de uma lista
        public static bool isEmpty(this object strValue) {
            return string.IsNullOrEmpty(strValue?.ToString()
                                              .Trim());
        }


        /// <summary>
        /// Transformar a primeira letra em padrao CamelCse
        /// </summary>
        public static string toUppercaseFirst(this string s) {

            if (string.IsNullOrEmpty(s)) {
                return string.Empty;
            }

            char[] a = s.ToCharArray();

            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }

        /// <summary>
        /// Transformar o texto em Uppercase
        /// </summary>
        public static string toUppercaseWords(this string value) {

            if (value.isEmpty()) {
                return "";
            }

            string[] array = value.Split(' ');

            string retorno = string.Empty;

            string[] wordsByPass = new[] {"de", "da", "dos", "do"};

            for (int i = 0; i < array.Length; i++) {

                string word = array[i]
                .stringOrEmptyLower();

                if (word.isEmpty()) {
                    continue;
                }

                if (wordsByPass.Contains(word)) {

                    retorno = string.Concat(retorno, " ", word);

                    continue;
                }

                retorno = string.Concat(retorno, " ", word.toUppercaseFirst());
            }

            return retorno.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        public static string defaultIfEmpty(this string str, string strDefault = "...") {

            str = str.stringOrEmpty();

            if (str.isEmpty()) {
                return strDefault;
            }

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string compress(this string s) {

            if (s.isEmpty()) {
                return string.Empty;
            }

            var bytes = Encoding.Unicode.GetBytes(s);

            using (var msi = new MemoryStream(bytes)) {

                using (var mso = new MemoryStream()) {

                    using (var gs = new GZipStream(mso, CompressionMode.Compress)) {

                        msi.CopyTo(gs);
                    }

                    return Convert.ToBase64String(mso.ToArray());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string decompress(this string s) {

            var bytes = Convert.FromBase64String(s);

            using (var msi = new MemoryStream(bytes)) {
                using (var mso = new MemoryStream()) {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress)) {
                        gs.CopyTo(mso);
                    }

                    return Encoding.Unicode.GetString(mso.ToArray());
                }
            }
        }

    }

}
