using System.Collections.Generic;
using System.Linq;
using BLL.Pracas.Entities;
using DAL._Core.AcessData;

namespace BLL.Pracas.ConstEnums {

    public class PracaConst {

        public static int ID_CE = 1;
        public static int ID_MG = 2;
        public static int ID_RJ = 3;
        
        public List<Praca> listaPracas { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public PracaConst() {
            this.listaPracas = new List<Praca>();

            this.listaPracas.Add(new Praca {
                id = ID_CE,
                nome = "Ceará",
                appConex = DataContext.appKeyCE
            });
            
            this.listaPracas.Add(new Praca {
                id = ID_MG,
                nome = "Minas Gerais",
                appConex = DataContext.appKeyMG
            });

            this.listaPracas.Add(new Praca {
                id = ID_RJ,
                nome = "Rio de Janeiro",
                appConex = DataContext.appKeyRJ
            });

        }

        public Praca pracaCE() {
            return listaPracas.FirstOrDefault(x => x.id == ID_CE);
        }
        
        public Praca pracaMG() {
            return listaPracas.FirstOrDefault(x => x.id == ID_MG);
        }
        
        public Praca pracaRJ() {
            return listaPracas.FirstOrDefault(x => x.id == ID_RJ);
        }        
    }

    
}