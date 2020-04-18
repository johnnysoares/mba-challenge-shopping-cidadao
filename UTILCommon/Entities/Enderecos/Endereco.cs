
namespace UTILCommon.Entities.Enderecos {

    public class Endereco {

        public string cep { get; set; }

        public string logradouro { get; set; }

        public string numero { get; set; }

        public string complemento { get; set; }

        public string bairro { get; set; }

        public string nomeCidade { get; set; }

        public int idCidadeIBGE { get; set; }

        public string siglaUF { get; set; }

        public bool flagPrincipal { get; set; }
    }
}
