using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Carte
    {
        protected String nom;
        protected String numero;
        protected int exemplaire = 0; //Uniquement utile pour collection        //à ajouter dans le diagramme de classe + dans le JSON
        protected string description;
        protected string type;
        protected string urlImage;  //permet d'attribuer à chaque carte une url pour l'image qui lui correspond      //à ajouter dans le diagramme de classe
        protected bool deck;  //uniquement utile pour deck        //permet de savoir si la catrte apartient ou non au deck du joueur      //à ajouter dans le diagramme de classe + dans le JSON


        public string Nom { get => nom; set => nom = value; }
        public string Numero { get => numero; set => numero = value; }
        public int Exemplaire { get => exemplaire; set => exemplaire = value; }
        public string Description { get => description; set => description = value; }
        public string Type { get => type; set => type = value; }
        public string UrlImage { get => urlImage; set => urlImage = value; }
        public bool Deck { get => deck; set => deck = value; }

        public Carte()
        {

        }
        public Carte(String unNom, String unNumero, int nbExemplaire, String uneDescription, String unType)
        {
            this.Nom = unNom;
            this.Numero = unNumero;
            this.exemplaire = nbExemplaire;
            this.description = uneDescription;
            this.Type = unType;

        }
        //Constructeur nécessaire à la partie deck
        public Carte(String unNom, String unNumero, String uneDescription, String unType, bool unDeck)
        {
            this.Nom = unNom;
            this.Numero = unNumero;
            this.description = uneDescription;
            this.Type = unType;
            this.deck = unDeck;

        }

        //Gestion de l'ajout et suppression des exemplaires
        public void ajoutExemplaire()
        {
            this.exemplaire = this.exemplaire +1;
        }
        public void supressionExemplaire()
        {
            if (this.exemplaire == 0){

            }
            else
            {
                this.exemplaire = this.exemplaire -1;
            }
        }

        //gestion de l'ajout et suppression d'une carte au deck
        public void ajoutDeck()
        {
            this.deck = true;
        }
        public void suppressionDeck()
        {
            this.deck = false;
        }
        public override string ToString()
        {
            return Nom;
        }
    }
}
