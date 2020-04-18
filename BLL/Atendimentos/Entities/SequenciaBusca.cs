using System;

namespace BLL.Atendimentos.Entities {

    public class SequenciaBusca {
        
        public int id { get; set; }
        public int idPraca { get; set; }
        public long nroInicio { get; set; }
        public long nroFim { get; set; }
        
        public DateTime dtPesquisa { get; set; }
        
        public string idTipo { get; set; }
    }

}