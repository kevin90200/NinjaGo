using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class CarteVehicule:Carte
    {
        //attributs hérité
        String description;

        public string Description { get => description; set => description = value; }
        public CarteVehicule(String unNom, String unNumero, int nbExemplaire, String uneDescription) : base(unNom, unNumero, nbExemplaire)
        {
            this.description = uneDescription;
        }

        
    }
}