using System;

namespace UTILCommon.Extensions {

    public static class NumberExtensions {

        //Extensao para facilitar a conversao de byt nullable
        public static byte toByte(this object value) {

            if (value == null) {
                return 0;
            }

            string valor = value.ToString().onlyNumber();

            byte result = 0;

            byte.TryParse(valor, out result);

            return result;
        }

        /// <summary>
        /// Extensao para facilitar a conversao de byte nullable
        /// </summary>
        public static byte toByte(this byte? value) {

            if (value == null)
                return 0;

            string valor = value.ToString().onlyNumber();

            byte result = 0;

            byte.TryParse(valor, out result);

            return result;
        }

        /// <summary>
        /// Extensao para facilitar a conversao para short
        /// </summary>
        public static short toShort(this object value) {

            if (value == null)
                return 0;

            string valor = value.ToString().onlyNumber();

            short result = 0;

            Int16.TryParse(valor, out result);

            return result;
        }

        //Extensao para facilitar a conversao de int nullable
        public static int toInt(this int? value) {

            if (value == null) {
                return 0;
            }

            return Convert.ToInt32(value.ToString());
        }


        //Extensao para facilitar a conversao de int nullable
        public static int toInt(this string value) {
            int result = 0;

            int.TryParse(value, out result);

            return result;
        }

        //Extensao para facilitar a conversao de int nullable
        public static int toInt(this object value) {

            if (value == null) {
                return 0;
            }

            string valor = value.ToString().onlyNumber();

            int result = 0;

            int.TryParse(valor, out result);

            return result;
        }

        public static long toLong(this string value) {

            long result = 0;

            long.TryParse(value, out result);

            return result;
        }

        //Extensao para facilitar a conversao de int nullable
        public static long toLong(this object value) {

            if (value == null) {
                return 0;
            }

            string valor = value.ToString().onlyNumber();

            long result = 0;

            long.TryParse(valor, out result);

            return result;
        }


        //Extensao para facilitar a conversao de double nullable
        public static double toDouble(this double? value) {

            if (value == null) {
                return 0;
            }

            return Convert.ToDouble(value);
        }


        /// <summary>
        /// Converter int em formato decimal
        /// </summary>
        public static decimal toDecimal(this int? value) {

            if (value.toInt() == 0) {

                return new decimal(0);
            }

            return new decimal(value.toInt());
        }
        
        /// <summary>
        /// Converter int em formato decimal e dividir por 100
        /// </summary>
        public static decimal toDecimal100(this int? value) {

            if (value.toInt() == 0) {

                return new decimal(0);
            }

            var result = decimal.Divide(value.toInt(), new decimal(100));

            return result;
        }

        /// <summary>
        /// Converter int em formato decimal
        /// </summary>
        public static decimal toDecimal(this int value) {

            if (value == 0) {

                return new decimal(0);
            }

            return new decimal(value);
        }

        /// <summary>
        /// Converter string em formato decimal
        /// </summary>
        public static decimal toDecimal(this string str) {

            decimal result = 0;

            decimal.TryParse(str, out result);

            return result;
        }

        /// <summary>
        /// Converter decimal nullable em decimal
        /// </summary>
        public static decimal toDecimal(this decimal? valor) {

            decimal result = 0;

            if (valor == null) {

                return result;

            }

            decimal.TryParse(valor.ToString(), out result);

            return result;
        }

        /// <summary>
        /// Converter decimal nullable em decimal
        /// </summary>
        public static decimal toDecimal(this object valor) {

            decimal result = 0;

            if (valor == null) {

                return result;

            }

            decimal.TryParse(valor.ToString(), out result);

            return result;
        }

        /// <summary>
        /// Converter decimal nullable em decimal
        /// </summary>
        public static decimal round(this decimal valor, int precision = 2) {

            return Decimal.Round(valor, precision, MidpointRounding.ToEven);
        }

        /// <summary>
        /// Converter decimal nullable em decimal
        /// </summary>
        public static decimal round(this decimal? valor, int precision = 2) {

            return Decimal.Round(valor.toDecimal(), precision, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Converter decimal nullable em decimal
        /// </summary>
        public static decimal precision(this decimal valor, int precision = 2) {

            return decimal.Parse(valor.ToString($"N{precision}"));
        }

        /// <summary>
        /// Converter decimal nullable em decimal
        /// </summary>
        public static decimal precision(this decimal? valor, int precision = 2) {

            return decimal.Parse(valor.toDecimal().ToString($"N{precision}"));
        }

        /// <summary>
        /// Converter decimal nullable em double
        /// </summary>
        public static double precision(this double valor, int precision = 2) {

            return double.Parse(valor.ToString($"N{precision}"));
        }

        /// <summary>
        /// Converter decimal nullable em decimal
        /// </summary>
        public static double precision(this double? valor, int precision = 2) {

            return double.Parse(valor.toDouble().ToString($"N{precision}"));
        }

        /// <summary>
        /// 
        /// </summary>
        public static decimal valorPercentual(this decimal valor, decimal percent) {

            if (percent == 0) {
                return new decimal(0);
            }

            decimal fatorPercent = Decimal.Divide(new decimal(100), percent);

            decimal valorCalculado = Decimal.Multiply(valor, fatorPercent);

            return valorCalculado;
        }


    }
}
