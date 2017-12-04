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
        protected bool deck1;  //uniquement utile pour deck        //permet de savoir si la catrte apartient ou non au deck du joueur      //à ajouter dans le diagramme de classe + dans le JSON
        protected bool deck2;
        protected bool deck3;


        public string Nom { get => nom; set => nom = value; }
        public string Numero { get => numero; set => numero = value; }
        public int Exemplaire { get => exemplaire; set => exemplaire = value; }
        public string Description { get => description; set => description = value; }
        public string Type { get => type; set => type = value; }
        public string UrlImage { get => urlImage; set => urlImage = value; }
        public bool Deck1 { get => deck1; set => deck1 = value; }
        public bool Deck2 { get => deck2; set => deck2 = value; }
        public bool Deck3 { get => deck3; set => deck3 = value; }

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
        public Carte(String unNom, String unNumero, int nbExemplaire, String uneDescription, String unType, bool deck1, bool deck2, bool deck3)
        {
            this.Nom = unNom;
            this.Numero = unNumero;
            this.exemplaire = nbExemplaire;
            this.description = uneDescription;
            this.Type = unType;
            this.deck1 = deck1;
            this.deck2 = deck2;
            this.deck3 = deck3;

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
        public void ajoutDeck1()
        {
            this.deck1 = true;
        }
        public void suppressionDeck1()
        {
            this.deck1 = false;
        }
        public void ajoutDeck2()
        {
            this.deck2 = true;
        }
        public void suppressionDeck2()
        {
            this.deck2 = false;
        }
        public void ajoutDeck3()
        {
            this.deck3 = true;
        }
        public void suppressionDeck3()
        {
            this.deck3 = false;
        }
        public override string ToString()
        {
            try   
            {
                return Convert.ToInt32(Numero).ToString() + " | " + Nom;        //Conversion en int puis à nouveau en string pour supprier les 0 de l'affichage
            }
            catch           //Try catch nécessaire pour les cartes LEx car on ne peut aps convertir en int
            {
                return Numero + " | " + Nom;
            }
        }
        public string convertNum()
        {
            try
            {
                return Convert.ToInt32(Numero).ToString();
            }
            catch           //Try catch nécessaire pour les cartes LEx car on ne peut aps convertir en int
            {
                return Numero;
            }
        }
        public override bool Equals(object obj)
        {
            return ((Carte)obj).Nom == this.Nom;
        }
    }
}
