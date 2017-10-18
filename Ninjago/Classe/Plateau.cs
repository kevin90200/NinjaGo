using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Plateau
    {
        int[,] plateau = new int[3,3];
        List<Case> lesCases =new List<Case>();

        public Plateau()
        {
            
        }

        public void Poser(Carte uneCarte, Joueur unJoueur, Case uneCase)
        {
            uneCase.Carte = uneCarte;
            uneCase.Joueur = unJoueur;
        }
    }
}
