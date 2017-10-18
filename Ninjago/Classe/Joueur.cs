﻿using System;
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
        List<CartePersonnage> deck;
        List<CartePersonnage> main;
        List<CartePersonnage> depot;

        Joueur(String unNom, String unPrenom)
        {
            this.Nom = unNom;
            this.Prenom = unPrenom;
            this.deck = new List<CartePersonnage>();
            this.main= new List<CartePersonnage>();
            this.depot = new List<CartePersonnage>();
        }


        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        internal List<CartePersonnage> Deck { get => deck; set => deck = value; }
        internal List<CartePersonnage> Main { get => main; set => main = value; }
        internal List<CartePersonnage> Depot { get => depot; set => depot = value; }

        public List<CartePersonnage> TirerCarte()
        {
            for (int i = 1; i <= 3; i++){
                Random aleatoire = new Random();
                int numeroAlea = aleatoire.Next(187); 
                main.Add(deck[numeroAlea]);
            }

            return main;
        }

        public Carte Piocher()
        {
            main.Add(deck[0]);
            return main[main.Count -1];
        }

        public void Defausser(CartePersonnage unCartePersonnage)
        {
            main.Remove(unCartePersonnage);
        }
    }
}
