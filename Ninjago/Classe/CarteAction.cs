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


        public CarteAction(String unNom, String unNumero, int nbExemplaire, String uneDescription, String unType) : base(unNom, unNumero, nbExemplaire, uneDescription, unType)
        {
        }

    }

}
