using System;

namespace BLL.Atendimentos.Entities {

    public class Atendimento {
        
        public long id { get; set; }
        public int idPraca { get; set; }
        public int idUnidade { get; set; }
        public int dataBusca { get; set; }
        public int mesAnoBusca { get; set; }
        public string senha { get; set; }
        
        public string evento { get; set; }
        public DateTime? dtEvento { get; set; }
        
        public int idAtividade { get; set; }
        public int idSecao { get; set; }
        public int idGuiche { get; set; }
        public int idPrioridade { get; set; }
        public int idAtendente { get; set; }
        public int idAvaliacao { get; set; }
        public int idResposta { get; set; }
        
        public DateTime? hrEmissaoSenha { get; set; }
        public DateTime? hrAlocacaoSecao { get; set; }
        public DateTime? hrAlocacaoGuiche { get; set; }
        public DateTime? hrInicioAtendimento { get; set; }
        public DateTime? hrFimAtendimento { get; set; }
        public DateTime? hrFinalizacaoSenha { get; set; }
        
        public DateTime? hrAlteracaoAtividade { get; set; }

        public TimeSpan? tempoTotalPausa { get; set; }

    }

}