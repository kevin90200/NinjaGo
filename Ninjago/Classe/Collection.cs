using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninjago
{
    class Collection
    {
        String nom;
        DateTime dateCreation;
        List<Carte> lesCartes;

        public Collection(String unNom, DateTime uneDate)
        {
            this.Nom = unNom;
            this.DateCreation = uneDate;
        }

        public string Nom { get => nom; set => nom = value; }
        public DateTime DateCreation { get => dateCreation; set => dateCreation = value; }
        internal List<Carte> LesCartes { get => lesCartes; set => lesCartes = value; }

        public void Ajouter(Carte uneCarte)
        {
            lesCartes.Add(uneCarte);
        }
        public void Supprimer(Carte uneCarte)
        {
            lesCartes.Remove(uneCarte);
        }
    }
}
