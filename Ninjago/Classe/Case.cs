using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ninjago
{
    class Case
    {
        Joueur joueur;
        CartePersonnage carte;
        Button btnCase;

        public Case(Button unBtn)
        {
            this.Joueur = null;
            this.Carte = null;
            this.btnCase = unBtn;
        }
        public Case (Joueur unJoueur, CartePersonnage uneCarte)
        {
            this.Joueur = unJoueur;
            this.Carte = uneCarte;
        }

        public Button BtnCase { get => btnCase; set => btnCase = value; }
        internal Joueur Joueur { get => joueur; set => joueur = value; }
        internal CartePersonnage Carte { get => carte; set => carte = value; }

        public Joueur Duel(Joueur joueur1, Joueur joueur2, CartePersonnage carteJ1, CartePersonnage carteJ2, string param)
        {
            Joueur vainqueur = null;
            if (param.Equals("Attaque"))
            {
                if (carteJ1.Attaque > carteJ2.Attaque)
                {
                    vainqueur = joueur1;
                }
                else
                {
                    vainqueur = joueur2;
                }
            }
            else if (param.Equals("Defense"))
            {
                if (carteJ1.Defense > carteJ2.Defense)
                {
                    vainqueur = joueur1;
                }
                else
                {
                    vainqueur = joueur2;
                }
            }
            else if (param.Equals("Vitesse"))
            {
                if (carteJ1.Vitesse > carteJ2.Vitesse)
                {
                    vainqueur = joueur1;
                }
                else
                {
                    vainqueur = joueur2;
                }
            }
            else if (param.Equals("Force"))
            {
                if (carteJ1.Force > carteJ2.Force)
                {
                    vainqueur = joueur1;
                }
                else
                {
                    vainqueur = joueur2;
                }
            }
            return vainqueur;
        }
    }
}
