using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Plateau
    {
        Case[,] unPlateau;

        public Plateau(int unNombre)
        {
           this.UnPlateau = new Case[unNombre, unNombre];
        }

        public Case[,] UnPlateau { get => unPlateau; set => unPlateau = value; }

        public void Poser(CartePersonnage uneCarte, Joueur unJoueur, Case uneCase)
        {
            uneCase.Carte = uneCarte;
            uneCase.Joueur = unJoueur;
        }
    }
}
