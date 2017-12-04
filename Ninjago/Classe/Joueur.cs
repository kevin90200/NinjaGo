using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Joueur
    {
        String nom;
        String prenom;
        DateTime dateNaissance;
        List<CartePersonnage> deck;
        List<CartePersonnage> main;
        List<CartePersonnage> depot;
        Random aleatoire;
        public Joueur()
        {
           
        }
        public Joueur(String unNom, String unPrenom , DateTime uneDate, List<CartePersonnage> desC)
        {
            this.Nom = unNom;
            this.Prenom = unPrenom;
            this.DateNaissance = uneDate;
            this.deck = desC;
            this.main= new List<CartePersonnage>();
            this.depot = new List<CartePersonnage>();
            aleatoire = new Random();
        }


        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }
        public List<CartePersonnage> Deck { get => deck; set => deck = value; }
        internal List<CartePersonnage> Main { get => main; set => main = value; }
        internal List<CartePersonnage> Depot { get => depot; set => depot = value; }

        public void TirerCarte()
        {
            for (int i = 1; i <= 3; i++){
                
                this.main.Add(deck[0]);
                this.deck.Remove(deck[0]);
            }
        }

        public void Piocher()
        {
            this.main.Add(deck[0]);
            this.deck.Remove(deck[0]);
        }

        public void Defausser(CartePersonnage uneCartePersonnage)
        {
            main.Remove(uneCartePersonnage);
        }
        public override string ToString()
        {
            return this.Nom;
        }
    }
}
