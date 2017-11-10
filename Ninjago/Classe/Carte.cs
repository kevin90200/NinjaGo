using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Carte
    {
        protected String nom;
        protected String numero;
        protected int exemplaire;
        protected string description;
        private string type;
        private string urlImage;


        public string Nom { get => nom; set => nom = value; }
        public string Numero { get => numero; set => numero = value; }
        public int Exemplaire { get => exemplaire; set => exemplaire = value; }
        public string Description { get => description; set => description = value; }
        public string Type { get => type; set => type = value; }
        public string UrlImage { get => type; set => type = value; }

        public Carte()
        {

        }
        public Carte(String unNom, String unNumero, int nbExemplaire, String uneDescription, String unType)
        {
            this.Nom = unNom;
            this.Numero = unNumero;
            this.exemplaire = nbExemplaire;
            this.description = uneDescription;
            this.Type = unType;

        }
        public void ajoutExemplaire()
        {
            this.exemplaire = this.exemplaire +1;
        }
        public void supressionExemplaire()
        {
            if (this.exemplaire == 0){
            }
            else
            {
                this.exemplaire = this.exemplaire -1;
            }
        }
        public override string ToString()
        {
            return Nom;
        }
    }
}
