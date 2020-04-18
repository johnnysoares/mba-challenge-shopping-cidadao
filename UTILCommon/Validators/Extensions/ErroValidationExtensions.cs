using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace UTILCommon.Validators.Extensions {

    public static class ErroValidationExtensions {

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Mensagem de erros em formato de lista de strings</returns>
        public static List<string> retornarErros(this ValidationResult Result) {

            var listaErroString = Result.Errors.Select(fail => fail.ErrorMessage).ToList();

            return listaErroString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> retornarErros(this Hashtable listaErros) {

            var listaErroString = listaErros.Values.Cast<string>();

            return listaErroString.ToList();
        }
        
    }

}
