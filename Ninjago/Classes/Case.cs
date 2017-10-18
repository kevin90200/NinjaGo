using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Case
    {
        Joueur joueur;
        Carte carte;

        public Case()
        {
            this.Joueur = null;
            this.Carte = null;
        }
        public Case (Joueur unJoueur, Carte uneCarte)
        {
            this.Joueur = unJoueur;
            this.Carte = uneCarte;
        }

        internal Joueur Joueur { get => joueur; set => joueur = value; }
        internal Carte Carte { get => carte; set => carte = value; }

        public Joueur Duel(Joueur joueur1, Joueur joueur2)
        {
            Joueur vainqueur = null;
            
            return vainqueur;
        }
    }
}
