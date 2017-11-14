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

        public CartePersonnage()
        {

        }
        public CartePersonnage(String unNom, String unNumero, int nbExemplaire, String uneDescription, String unType, int uneAttaque, int uneDefense, int uneVitesse, int uneForce) : base(unNom, unNumero, nbExemplaire, uneDescription, unType)
        {
            this.Attaque = uneAttaque;
            this.Defense = uneDefense;
            this.Vitesse = uneVitesse;
            this.Force = uneForce;
        }
        //Constructeur nécessaire à la partie deck
        public CartePersonnage(String unNom, String unNumero,int nbExemplaire, String uneDescription, String unType, bool unDeck, int uneAttaque, int uneDefense, int uneVitesse, int uneForce) : base(unNom, unNumero, nbExemplaire, uneDescription, unType, unDeck)
        {
            this.Attaque = uneAttaque;
            this.Defense = uneDefense;
            this.Vitesse = uneVitesse;
            this.Force = uneForce;
        }

        public int Attaque { get => attaque; set => attaque = value; }
        public int Defense { get => defense; set => defense = value; }
        public int Vitesse { get => vitesse; set => vitesse = value; }
        public int Force { get => force; set => force = value; }
    }
}
