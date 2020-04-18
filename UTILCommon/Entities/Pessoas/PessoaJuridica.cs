
namespace UTILCommon.Entities.Pessoas {

    public class PessoaJuridica : Pessoa {

        public string razaoSocial { get; set; }

        public string cnpj { get; set; }

        public string inscricaoEstadual { get; set; }

        public string inscricaoMunicipal { get; set; }
    }
}
