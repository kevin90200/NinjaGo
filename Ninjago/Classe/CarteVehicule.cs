﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class CarteVehicule:Carte
    {
        String description;

        public string Description { get => description; set => description = value; }
        public CarteVehicule(String unNom, String unNumero, String uneDescription) : base(unNom, unNumero)
        {
            this.description = uneDescription;
        }

        
    }
}