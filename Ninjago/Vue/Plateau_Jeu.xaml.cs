using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Ninjago.Vue
{
    /// <summary>
    /// Logique d'interaction pour Plateau_Jeu.xaml
    /// </summary>
    public partial class Plateau_Jeu
    {
        int action;
        List<Joueur> lesJ = new List<Joueur>();
        List<CartePersonnage> lesCartes = new List<CartePersonnage>();        //liste de toutes les cartes
        Joueur j1, j2, Vainqueur, gagnant;
        Joueur actif;
        CartePersonnage selected = null;
        Plateau plt;
        Button btnTop;
        Button btnBottom;
        Button btnLeft;
        Button btnRight;
        Button btnPoser;
        Boolean duel = false;
        Boolean defausse = false;

        public Plateau_Jeu()
        {

            InitializeComponent();
            Main.Visibility = Visibility.Hidden;
            btn_jouer.Content = "Jouer";
            var json = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("joueur.json"));
            //Parcours de la collection pour créer les cartes en fonction du type renvoyer par le JSON

            foreach (var j in json)
            {
                List<CartePersonnage> deck = new List<CartePersonnage>();
                foreach (var cp in j.Deck)
                {
                    CartePersonnage c = new CartePersonnage(cp.Nom.ToString(), cp.Numero.ToString(), Convert.ToInt32(cp.Exemplaire), cp.Description.ToString(), cp.Type.ToString(), Convert.ToBoolean(cp.Deck1), Convert.ToBoolean(cp.Deck2), Convert.ToBoolean(cp.Deck3), Convert.ToInt32(cp.Attaque), Convert.ToInt32(cp.Defense), Convert.ToInt32(cp.Vitesse), Convert.ToInt32(cp.Force));
                    deck.Add(c);
                }
                Joueur player = new Joueur(Convert.ToString(j.Nom), Convert.ToString(j.Prenom), Convert.ToDateTime(j.DateNaissance), deck);

                lesJ.Add(player);
            }

            var jsonCartes = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("ninjago.json"));
            //Parcours de la collection pour créer la liste de cartes en fonction du type renvoyer par le JSON
            try
            {
                foreach (var carte in jsonCartes)
                {
                    if (carte.Type == "P")
                    {
                        CartePersonnage cp = new CartePersonnage(carte.Nom.ToString(), carte.Numero.ToString(), Convert.ToInt32(carte.Exemplaire), carte.Description.ToString(), carte.Type.ToString(), Convert.ToBoolean(carte.Deck1), Convert.ToBoolean(carte.Deck2), Convert.ToBoolean(carte.Deck3), Convert.ToInt32(carte.Attaque), Convert.ToInt32(carte.Defense), Convert.ToInt32(carte.Vitesse), Convert.ToInt32(carte.Force));
                        lesCartes.Add(cp);
                    }
                }
            }
            catch
            {

            }
            //List <Case> lesCases = new List<Case>();
            plt = new Plateau(5);

            for (int r = 0; r <= 4; r++)
            {
                for (int c = 0; c <= 4; c++)
                {
                    Button btn = new Button();
                    btn = (Button)FindName("btn" + r + "_" + c);
                    Case laCase = new Case(btn);
                    plt.UnPlateau[r, c] = laCase;
                }
            }


            if (lesJ[0].DateNaissance > lesJ[1].DateNaissance)
            {
                j1 = lesJ[0];
                j2 = lesJ[1];

            }
            else
            {
                j1 = lesJ[1];
                j2 = lesJ[0];
            }
            j1.TirerCarte();
            j2.TirerCarte();

        }

        #region plateau
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Boolean retirer = false;
            if (action.Equals(1))
            {
                Button btn = sender as Button;
                btnPoser = btn;
                if (btn.Content.Equals(""))
                {
                    btn.Content = selected.Numero;
                    try
                    {
                        btn.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btn.Content) + ".png")));
                    }
                    catch
                    {
                        btn.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btn.Content + ".png")));
                    }
                    //parcour de la grille pour afficher ou non les boutons de duel

                    for (int r = 0; r <= 4; r++)
                    {
                        for (int c = 0; c <= 4; c++)
                        {
                            int r1 = r + 1;
                            int c1 = c + 1;
                            int r01 = r - 1;
                            int c01 = c - 1;
                            if (btn.Name == "btn" + r + "_" + c)
                            {
                                if (actif == j1)
                                {
                                    plt.UnPlateau[r, c].Joueur = j1;
                                }
                                else
                                {
                                    plt.UnPlateau[r, c].Joueur = j2;
                                }
                                Button Top;
                                Button Bottom;
                                Button Left;
                                Button Right;
                                Left = (Button)FindName("btn" + r + "_" + c01);
                                Right = (Button)FindName("btn" + r + "_" + c1);
                                Top = (Button)FindName("btn" + r01 + "_" + c);
                                Bottom = (Button)FindName("btn" + r1 + "_" + c);
                                btnLeft = (Button)FindName("duel" + r + "_" + c01);
                                btnRight = (Button)FindName("duel" + r + "_" + c1);
                                btnTop = (Button)FindName("duel" + r01 + "_" + c);
                                btnBottom = (Button)FindName("duel" + r1 + "_" + c);
                                if (Left.Content != "" && actif.Nom != plt.UnPlateau[r, c01].Joueur.Nom)
                                {
                                    duel = true;
                                    btnLeft.Visibility = Visibility.Visible;
                                }
                                if (Right.Content != "" && actif.Nom != plt.UnPlateau[r, c1].Joueur.Nom)
                                {
                                    duel = true;
                                    btnRight.Visibility = Visibility.Visible;
                                }
                                if (Top.Content != "" && actif.Nom != plt.UnPlateau[r01, c].Joueur.Nom)
                                {
                                    duel = true;
                                    btnTop.Visibility = Visibility.Visible;
                                }
                                if (Bottom.Content != "" && actif.Nom != plt.UnPlateau[r1, c].Joueur.Nom)
                                {
                                    duel = true;
                                    btnBottom.Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }
                }
                //permet de retirer de la main la carte qui vient d'être posée
                if (actif == j1)
                {
                    int i = 0;
                    foreach (CartePersonnage c in j1.Main)
                    {
                        i = i + 1;
                        if (c == selected)
                        {
                            Button btnMain = (Button)FindName("btnCarte" + i);
                            btnMain.Content = "";
                            btnMain.Background = new ImageBrush(null);
                            retirer = true;
                        }
                    }
                    if (retirer == true)
                    {
                        j1.Main.Remove(selected);
                    }
                    refreshMain();
                }
                else
                {
                    int i = 0;
                    foreach (CartePersonnage c in j2.Main)
                    {
                        i = i + 1;
                        if (c == selected)
                        {
                            Button btnMain = (Button)FindName("btnCarte" + i);
                            btnMain.Content = "";
                            btnMain.Background = new ImageBrush(null);
                            retirer = true;

                        }
                    }
                    if (retirer == true)
                    {
                        j2.Main.Remove(selected);
                    }
                    refreshMain();
                }

                action = 0;
            }
        }
        private void btn_duel(object sender, RoutedEventArgs e)
        {
            duel = false;
            Button btnDuel = sender as Button;
            Button btnCarteDuel = new Button();
            Case ca;
            btnDuel.Visibility = Visibility.Hidden;
            for (int r = 1; r <= 3; r++)
            {
                for (int c = 1; c <= 3; c++)
                {
                    int r1 = r + 1;
                    int c1 = c + 1;
                    int r01 = r - 1;
                    int c01 = c - 1;
                    string param = "";
                    if (btnDuel.Name.Equals("duel" + r + "_" + c))
                    {


                        btnCarteDuel = (Button)FindName("btn" + r + "_" + c);

                        foreach (CartePersonnage cp in lesCartes)
                        {
                            if (cp.Numero == btnCarteDuel.Content.ToString())
                            {
                                if (actif == j1)
                                {
                                    if (btnPoser.Name == "btn" + r01 + "_" + c)
                                    {
                                        param = "Vitesse";

                                    }
                                    else if (btnPoser.Name == "btn" + r1 + "_" + c)
                                    {
                                        param = "Force";

                                    }
                                    else if (btnPoser.Name == "btn" + r + "_" + c01)
                                    {
                                        param = "Defense";
                                    }
                                    else if (btnPoser.Name == "btn" + r + "_" + c1)
                                    {
                                        param = "Attaque";
                                    }
                                    Vainqueur = plt.UnPlateau[r, c].Duel(j1, j2, selected, cp, param);
                                }
                                else
                                {
                                    if (btnPoser.Name == "btn" + r01 + "_" + c)
                                    {
                                        param = "Vitesse";
                                    }
                                    else if (btnPoser.Name == "btn" + r1 + "_" + c)
                                    {
                                        param = "Force";
                                    }
                                    else if (btnPoser.Name == "btn" + r + "_" + c01)
                                    {
                                        param = "Defense";
                                    }
                                    else if (btnPoser.Name == "btn" + r + "_" + c1)
                                    {
                                        param = "Attaque";
                                    }
                                    Vainqueur = plt.UnPlateau[r, c].Duel(j1, j2, cp, selected, param);
                                }
                            }

                        }
                    }
                }
            }
            if (Vainqueur == actif)
            {
                ca = new Case(btnCarteDuel);
                if (actif == j1)
                {
                    ca.Joueur = j2;
                }
                else
                {
                    ca.Joueur = j1;
                }
                foreach (Case c in plt.UnPlateau)
                {
                    if (ca.Joueur == c.Joueur && ca.BtnCase == c.BtnCase)
                    {
                        c.Joueur = actif;
                    }
                }

            }
            else
            {
                ca = new Case(btnPoser);
                ca.Joueur = actif;
                foreach (Case c in plt.UnPlateau)
                {
                    if (ca.Joueur == c.Joueur && ca.BtnCase == c.BtnCase)
                    {
                        if (actif == j1)
                        {
                            c.Joueur = j2;
                        }
                        else
                        {
                            c.Joueur = j1;
                        }
                    }
                }

            }

            foreach (Case c in plt.UnPlateau)
            {

                if (c.Joueur == j1)
                {
                    RotateTransform rotateTransformBtn = new RotateTransform(0);
                    rotateTransformBtn.CenterX = 0;
                    rotateTransformBtn.CenterY = 0;
                    c.BtnCase.RenderTransform = rotateTransformBtn;


                }
                else if (c.Joueur == j2)
                {
                    RotateTransform rotateTransformBtn = new RotateTransform(180);
                    rotateTransformBtn.CenterX = 90;
                    rotateTransformBtn.CenterY = 70;
                    c.BtnCase.RenderTransform = rotateTransformBtn;
                }
            }
        }


        #endregion
        #region bouton jouer
        Boolean retourner = true;
        private void btn_jouer_Click(object sender, RoutedEventArgs e)
        { //poser qu'une carte par tour : variable globale utiliser aussi dans btn_click

            btnCarte1.Opacity = 1;
            btnCarte2.Opacity = 1;
            btnCarte3.Opacity = 1;
            btnCarte4.Opacity = 1;
            // action pour retourner le plateau et indiquer le nom du personnage sur les radiobutton et transition entre j1 et j2
            int val_ret;
            if (btn_jouer.Content.ToString() == "Jouer")
            {
                action = 1;
                defausse = false;
                if (retourner == true)
                {
                    actif = j1;
                    j1.Piocher();
                    val_ret = 0;
                    retourner = false;
                    lblNomJoueur.Content = j1.Nom.ToString();
                    try
                    {
                        btnDepot.Content = j1.Depot[j1.Depot.Count - 1].Numero;
                        try
                        {
                            btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnDepot.Content).ToString() + ".png")));
                        }
                        catch
                        {
                            btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnDepot.Content + ".png")));
                        }
                    }
                    catch
                    {
                        btnDepot.Content = "carteDepot";
                        btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/" + btnDepot.Content + ".jpg")));
                    }
                    refreshMain();
                }
                else
                {
                    actif = j2;
                    j2.Piocher();
                    val_ret = 180;
                    retourner = true;
                    lblNomJoueur.Content = j2.Nom.ToString();
                    try
                    {
                        btnDepot.Content = j2.Depot[j2.Depot.Count - 1].Numero;
                        try
                        {
                            btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnDepot.Content).ToString() + ".png")));
                        }
                        catch
                        {
                            btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnDepot.Content + ".png")));
                        }
                    }
                    catch
                    {
                        btnDepot.Content = "carteDepot";
                        btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/" + btnDepot.Content + ".jpg")));
                    }
                    refreshMain();
                }
                RotateTransform rotateTransform = new RotateTransform(val_ret);
                rotateTransform.CenterX = -1;
                rotateTransform.CenterY = -3;
                Plateau.RenderTransform = rotateTransform;
                //changer le label jouer en passer
                btn_jouer.Content = "Passer";
                //rendre visible la main du joueur
                Main.Visibility = Visibility.Visible;


                //action pour retourner les boutons vides ( comme ça le joueur aura toujours ses cartes a lui dans son sens)
                for (int i = 1; i <= 3; i++)
                {
                    RotateTransform rotateTransformBtn = new RotateTransform(val_ret);
                    rotateTransformBtn.CenterX = 90;
                    rotateTransformBtn.CenterY = 70;


                    Button btn1 = (Button)FindName("btn1_" + i);
                    if (btn1.Content.Equals(""))
                    {
                        btn1.RenderTransform = rotateTransformBtn;
                    }
                    Button btn2 = (Button)FindName("btn2_" + i);
                    if (btn2.Content.Equals(""))
                    {
                        btn2.RenderTransform = rotateTransformBtn;
                    }
                    Button btn3 = (Button)FindName("btn3_" + i);
                    if (btn3.Content.Equals(""))
                    {
                        btn3.RenderTransform = rotateTransformBtn;
                    }
                    for (int j = 1; j <= 3; j++)
                    {
                        RotateTransform rotateTransformBtnDuel = new RotateTransform(val_ret);
                        Button duel = (Button)FindName("duel" + i + "_" + j);
                        rotateTransformBtnDuel.CenterX = -20;
                        rotateTransformBtnDuel.CenterY = 40;
                        duel.RenderTransform = rotateTransformBtnDuel;


                    }

                }

            }
            // cacher ses cartes
            else if (action == 0 && duel == false)
            {

                //permet de masque les boutton duel au changement de tour
                btnLeft.Visibility = Visibility.Hidden;
                btnRight.Visibility = Visibility.Hidden;
                btnTop.Visibility = Visibility.Hidden;
                btnBottom.Visibility = Visibility.Hidden;
                //on passe la carte selectionner à null
                selected = null;
                btnPoser = null;
                btn_jouer.Content = "Jouer";
                lblNomJoueur.Content = "";
                Main.Visibility = Visibility.Hidden;
                bool plein = true;
                int ptj1 = 0;
                int ptj2 = 0;
                for (int r = 1; r <= 3; r++)
                {
                    for (int c = 1; c <= 3; c++)
                    {
                        if (plt.UnPlateau[r, c].BtnCase.Content.ToString() == "")
                        {
                            plein = false;
                        }
                        else
                        {
                            if (plt.UnPlateau[r, c].Joueur == j1)
                            {
                                ptj1 = ptj1 + 1;
                            }
                            else if (plt.UnPlateau[r, c].Joueur == j2)
                            {
                                ptj2 = ptj2 + 1;
                            }
                        }
                    }
                }
                if (ptj1 > ptj2)
                {
                    gagnant = j1;
                }
                else
                {
                    gagnant = j2;
                }
                if (plein == true)
                {
                    MessageBox.Show(gagnant.ToString() + " | carte " + j1.Nom + ": " + ptj1 + " | carte " + j2.Nom + ": " + ptj2);
                }

            }

        }
        #endregion
        #region main
        private void btnCarte1_Click(object sender, RoutedEventArgs e)
        {
            if (actif == j1)
            {
                btnCarte1.Opacity = 1;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;

                foreach (CartePersonnage c in j1.Main)
                {
                    if (c.Numero == Convert.ToString(btnCarte1.Content))
                    {
                        selected = c;
                    }
                }

            }
            else
            {
                btnCarte1.Opacity = 1;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;

                foreach (CartePersonnage c in j2.Main)
                {
                    if (c.Numero == Convert.ToString(btnCarte1.Content))
                    {
                        selected = c;
                    }
                }

            }
        }

        private void btnCarte2_Click(object sender, RoutedEventArgs e)
        {
            if (actif == j1)
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 1;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;

                foreach (CartePersonnage c in j1.Main)
                {
                    if (c.Numero == Convert.ToString(btnCarte2.Content))
                    {
                        selected = c;
                    }
                }

            }
            else
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 1;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 0.5;

                foreach (CartePersonnage c in j2.Main)
                {
                    if (c.Numero == Convert.ToString(btnCarte2.Content))
                    {
                        selected = c;
                    }
                }

            }
        }




        private void btnCarte3_Click(object sender, RoutedEventArgs e)
        {
            if (actif == j1)
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 1;
                btnCarte4.Opacity = 0.5;

                foreach (CartePersonnage c in j1.Main)
                {
                    if (c.Numero == Convert.ToString(btnCarte3.Content))
                    {
                        selected = c;
                    }
                }

            }
            else
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 1;
                btnCarte4.Opacity = 0.5;

                foreach (CartePersonnage c in j2.Main)
                {
                    if (c.Numero == Convert.ToString(btnCarte3.Content))
                    {
                        selected = c;
                    }
                }

            }
        }



        private void btnCarte4_Click(object sender, RoutedEventArgs e)
        {
            if (actif == j1)
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 1;

                foreach (CartePersonnage c in j1.Main)
                {
                    if (c.Numero == Convert.ToString(btnCarte4.Content))
                    {
                        selected = c;
                    }
                }

            }
            else
            {
                btnCarte1.Opacity = 0.5;
                btnCarte2.Opacity = 0.5;
                btnCarte3.Opacity = 0.5;
                btnCarte4.Opacity = 1;

                foreach (CartePersonnage c in j2.Main)
                {
                    if (c.Numero == Convert.ToString(btnCarte4.Content))
                    {
                        selected = c;
                    }
                }

            }
        }

        #endregion

        private void btnDepot_Click(object sender, RoutedEventArgs e)
        {
            Boolean retirer = false;

            if (action == 0)
            {
                if (defausse == false)
                {
                    //permet de retirer de la main la carte qui vient d'être defausser
                    if (actif == j1)
                    {
                        int i = 0;
                        foreach (CartePersonnage c in j1.Main)
                        {
                            i = i + 1;
                            if (c == selected)
                            {
                                Button btnMain = (Button)FindName("btnCarte" + i);
                                btnMain.Content = "";
                                btnMain.Background = new ImageBrush(null);
                                retirer = true;
                            }
                        }
                        if (retirer == true)
                        {
                            j1.Main.Remove(selected);
                            j1.Depot.Add(selected);
                            defausse = true;
                            j1.Piocher();
                        }
                        refreshMain();
                        try
                        {
                            btnDepot.Content = j1.Depot[j1.Depot.Count - 1].Numero;
                            try
                            {
                                btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnDepot.Content).ToString() + ".png")));
                            }
                            catch
                            {
                                btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnDepot.Content + ".png")));
                            }
                        }
                        catch
                        {
                            btnDepot.Content = "carteDepot";
                            btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/" + btnDepot.Content + ".jpg")));
                        }
                    }
                    else
                    {
                        int i = 0;
                        foreach (CartePersonnage c in j2.Main)
                        {
                            i = i + 1;
                            if (c == selected)
                            {
                                Button btnMain = (Button)FindName("btnCarte" + i);
                                btnMain.Content = "";
                                btnMain.Background = new ImageBrush(null);
                                retirer = true;
                            }
                        }
                        if (retirer == true)
                        {
                            j2.Main.Remove(selected);
                            j2.Depot.Add(selected);
                            defausse = true;
                            j2.Piocher();
                        }
                        refreshMain();
                        try
                        {
                            btnDepot.Content = j2.Depot[j2.Depot.Count - 1].Numero;
                            try
                            {
                                btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnDepot.Content).ToString() + ".png")));
                            }
                            catch
                            {
                                btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnDepot.Content + ".png")));
                            }
                        }
                        catch
                        {
                            btnDepot.Content = "carteDepot";
                            btnDepot.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/" + btnDepot.Content + ".jpg")));
                        }
                    }
                }
            }

        }


        private void refreshMain()
        {
            btnCarte1.Opacity = 1;
            btnCarte2.Opacity = 1;
            btnCarte3.Opacity = 1;
            btnCarte4.Opacity = 1;
            btnCarte1.Content = "";
            btnCarte2.Content = "";
            btnCarte3.Content = "";
            btnCarte4.Content = "";
            btnCarte1.Background = new ImageBrush(null);
            btnCarte2.Background = new ImageBrush(null);
            btnCarte3.Background = new ImageBrush(null);
            btnCarte4.Background = new ImageBrush(null);
            try   //si la main contient 4 cartes
            {
                btnCarte1.Content = actif.Main[0].Numero;
                btnCarte2.Content = actif.Main[1].Numero;
                btnCarte3.Content = actif.Main[2].Numero;
                btnCarte4.Content = actif.Main[3].Numero;
                try
                {
                    btnCarte1.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte1.Content).ToString() + ".png")));
                }
                catch
                {
                    btnCarte1.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte1.Content.ToString() + ".png")));
                }
                try
                {
                    btnCarte2.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte2.Content).ToString() + ".png")));
                }
                catch
                {
                    btnCarte2.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte2.Content.ToString() + ".png")));
                }
                try
                {
                    btnCarte3.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte3.Content).ToString() + ".png")));
                }
                catch
                {
                    btnCarte3.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte3.Content.ToString() + ".png")));
                }
                try
                {
                    btnCarte4.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte4.Content).ToString() + ".png")));
                }
                catch
                {
                    btnCarte4.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte4.Content.ToString() + ".png")));
                }



            }
            catch   //si la main contient trois cartes
            {
                btnCarte1.Content = actif.Main[0].Numero;
                btnCarte2.Content = actif.Main[1].Numero;
                btnCarte3.Content = actif.Main[2].Numero;
                try
                {
                    btnCarte1.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte1.Content).ToString() + ".png")));
                }
                catch
                {
                    btnCarte1.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte1.Content.ToString() + ".png")));
                }
                try
                {
                    btnCarte2.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte2.Content).ToString() + ".png")));
                }
                catch
                {
                    btnCarte2.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte2.Content.ToString() + ".png")));
                }
                try
                {
                    btnCarte3.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + Convert.ToInt32(btnCarte3.Content).ToString() + ".png")));
                }
                catch
                {
                    btnCarte3.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Ressource/cartes/" + btnCarte3.Content.ToString() + ".png")));
                }

            }
        }

        private void btn_retour_plateau_click(object sender, RoutedEventArgs e)
        {
            Launcher fenetre = new Launcher();
            fenetre.Show();
            this.Close();
        }
    }
}

