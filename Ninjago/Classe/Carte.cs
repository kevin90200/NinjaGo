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


        protected string Nom { get => nom; set => nom = value; }
        protected string Numero { get => numero; set => numero = value; }
        protected int Exemplaire { get => exemplaire; set => exemplaire = value; }

        public Carte(String unNom, String unNumero, int nbExemplaire)
        {
            this.Nom = unNom;
            this.Numero = unNumero;
            this.exemplaire = nbExemplaire;
        }
    }
}
