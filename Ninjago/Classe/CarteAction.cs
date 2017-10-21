using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class CarteAction : Carte
    {
        //attributs hérité
        String description;


        public CarteAction(String unNom, String unNumero, int nbExemplaire, String uneDescription) : base(unNom, unNumero, nbExemplaire)
        {
            this.Description = uneDescription;
        }

        public string Description { get => description; set => description = value; }
    }

}
