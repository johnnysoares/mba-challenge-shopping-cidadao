using System;
using System.Collections;
using System.Collections.Generic;

namespace UTILCommon.Models {

    public class ResponseModel<T> {

        //public string Token { get; set; }
        //public Boolean IsAuthenicated;
        public bool flagErro { get; set; }
        public List<string> listaMensagens { get; set; }
        public Hashtable Erros;
        public PagerInfo PagerInfo { get; set; }
        public T Entity;

        /// <summary>
        /// Construtor
        /// </summary>
        public ResponseModel() {

            listaMensagens = new List<String>();
            
            Erros = new Hashtable();

            this.PagerInfo = new PagerInfo(20);
            
        }

        /// <summary>
        /// 
        /// </summary>
        public ResponseModel(bool _flagErro, string _firstError) : this() {

            this.flagErro = _flagErro;

            this.Erros.Add(0, _firstError);

        }
        /// <summary>
        /// 
        /// </summary>
        public ResponseModel(bool _flagErro, string _firstError, T _Entity) : this(_flagErro, _firstError) {

            this.Entity = _Entity;

        }
    }
}
