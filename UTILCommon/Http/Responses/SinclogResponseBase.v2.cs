using System.Collections.Generic;

namespace UTILCommon.Http.Responses {

    public class SinclogResponseBaseV2<T> where T : class {

        public int code { get; set; }

        public bool erro { get; set; }

        public string mensagem { get; set; }

        public List<T> resultados { get; set; }

        public List<ErroDetalhe> errosDetalhes { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ErroDetalhe {
        
        public int code { get; set; }

        public string descricao { get; set; }

        public object info { get; set; }
    }
}

