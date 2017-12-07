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

        public Joueur Duel(Joueur actif, Joueur nonActif, CartePersonnage carteActif, CartePersonnage carteNonActif, string param)
        {
            Joueur vainqueur = null;
            if (param.Equals("Attaque"))
            {
                if (carteActif.Attaque >= carteNonActif.Attaque)
                {
                    vainqueur = actif;
                }
                else
                {
                    vainqueur = nonActif;
                }
            }
            else if (param.Equals("Defense"))
            {
                if (carteActif.Defense >= carteNonActif.Defense)
                {
                    vainqueur = actif;
                }
                else
                {
                    vainqueur = nonActif;
                }
            }
            else if (param.Equals("Vitesse"))
            {
                if (carteActif.Vitesse >= carteNonActif.Vitesse)
                {
                    vainqueur = actif;
                }
                else
                {
                    vainqueur = nonActif;
                }
            }
            else if (param.Equals("Force"))
            {
                if (carteActif.Force >= carteNonActif.Force)
                {
                    vainqueur = actif;
                }
                else
                {
                    vainqueur = nonActif;
                }
            }
            return vainqueur;
        }
    }
}
