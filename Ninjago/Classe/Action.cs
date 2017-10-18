﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Action : Carte
    {
        String description;


        public Action(String unNom, String unNumero, String uneDescription) : base(unNom, unNumero)
        {
            this.Description = uneDescription;
        }

        public string Description { get => description; set => description = value; }
    }

}
