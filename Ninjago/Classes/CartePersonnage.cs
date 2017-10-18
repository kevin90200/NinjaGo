using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class CartePersonnage : Carte
    {
        //attributs hérité
        int attaque;
        int defense;
        int vitesse;
        int force;
        bool alignement;

        public CartePersonnage(String unNom, String unNumero, int uneAttaque, int uneDefense, int uneVitesse, int uneForce, bool unAlignement) : base(unNom, unNumero)
        {
            this.Attaque = uneAttaque;
            this.Defense = uneDefense;
            this.Vitesse = uneVitesse;
            this.Force = uneForce;
            this.Alignement = unAlignement;
        }

        public int Attaque { get => attaque; set => attaque = value; }
        public int Defense { get => defense; set => defense = value; }
        public int Vitesse { get => vitesse; set => vitesse = value; }
        public int Force { get => force; set => force = value; }
        public bool Alignement { get => alignement; set => alignement = value; }
    }
}
