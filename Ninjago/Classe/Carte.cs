using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Carte
    {
        private String nom;
        private String numero;

        protected string Nom { get => nom; set => nom = value; }
        protected string Numero { get => numero; set => numero = value; }

        public Carte(String unNom, String unNumero)
        {
            this.Nom = unNom;
            this.Numero = unNumero;
        }
    }
}
