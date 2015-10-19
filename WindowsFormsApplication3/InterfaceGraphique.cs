using System;
using System.Drawing;

using System.Windows.Forms;
using System.Threading;


namespace WindowsFormsApplication3
{
    /**
    *   Classe InterfaceGraphique
    *   Représente toute la partie UI de tetris
    *   Dernière modification : 19/10/2015 par Christophe GERARD
    **/
    public partial class InterfaceGraphique : Form
    {
        Jeu jeu; // Attribut jeu 
        public InterfaceGraphique()
        {
            InitializeComponent(); // Méthode obligatoire forcément placée en première
            /**
            *   Couleurs pour le score (fond et texte)
            **/
            label1.ForeColor = Color.FromArgb(223,241,239);
            label1.BackColor = Color.FromArgb(37,43,90);
            panel2.Visible = false; // On rend invisible le deuxieme panel
            panel2.BackgroundImage = Image.FromFile("Images/gameover.jpg"); // Et on lui affecte une image de fond
            label1.Font = new Font("Arial",12);
            jeu = new Jeu(); // Création du nouveau jeu
            label1.Text = jeu.score.ToString(); // Affichage du score dans le label 1
            label1.AutoSize = false;
            label1.Width = 100;
            label1.Height = 20;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            Thread ThreadJeu = new Thread(jeu.lancerJeu); // Nouveau Thread pour lancer le jeu (Thread Safe)
            ThreadJeu.Start();
            jeu.RaiseCustomEvent += HandleCustomEvent; // Gestion d'evenement pour rafraichir la GUI
            jeu.YouGameOverEvent += HandleGameOverEvent; // Gestion d'evenement pour le gameOver
            KeyDown += new KeyEventHandler(MyKeyPressedEventHandler); // Gestion d'evenement pour une touche pressée
            new Debug(jeu).Show(); // Interface de debug (a commenté si non voulue)
            panel1.BackgroundImage = Image.FromFile("Images/fondTetris.jpg"); // On set le fond du jeu
        }

        // Cette méthode permet de rafraichir la GUI quand un evenement RafraichirGUIEvent se présente
        public void HandleCustomEvent(Object sender, RafraichirGUIEvent e) 
        {
            this.Invoke(() => this.Refresh());
        }

        // Cette méthode affiche le gameOver lorsque l'evenement GameOverEvent se présente
        public void HandleGameOverEvent(Object sender, GameOverEvent e)
        {
            this.Invoke(() => panel2.Visible = true);
        }

        // Méthode qui permet d'executer le delegate sur le thread qui détient le controle de la fenetre
        public void Invoke(Action action)
        {
            if (panel1.InvokeRequired)
            {
                panel1.Invoke(action);
            }
            else
            {
                action();
            }
        }

        
        private void panel1_Paint(object sender, PaintEventArgs e) // Méthode qui initialize le panel Graphiquement
        {
            Graphics g = e.Graphics;
            this.ClientSize = new Size((int)Jeu.NB_CASE_LARGEUR * Jeu.TailleCase, (int)Jeu.NB_CASE_HAUTEUR * Jeu.TailleCase);
            jeu.draw(g); // Appel a la méthode draw de la classe jeu
            label1.Text = jeu.score.ToString();
        }

        // Méthode qui gére les evenement quand une touche du clavier est pressée
        public void MyKeyPressedEventHandler(Object sender ,KeyEventArgs keyData)
        {
            if(keyData.KeyCode == Keys.D || keyData.KeyCode == Keys.Right)
            {
                jeu.pieceCourante.deplacer(1); // Déplacement vers la droite
            }
            if(keyData.KeyCode == Keys.Q || keyData.KeyCode == Keys.Left)
            {
                jeu.pieceCourante.deplacer(-1); // Déplacement vers la gauche
            }
            if (keyData.KeyCode == Keys.R)
            {
                jeu.resume(); // Reprise du jeu
            }
            if (keyData.KeyCode == Keys.P)
            {
                jeu.pause(); // Mise en pause
            }
            if(keyData.KeyCode == Keys.Z || keyData.KeyCode == Keys.Up)
            {
                jeu.pieceCourante.Tourner(); // Rotation de la pièce courante
            }
            if(keyData.KeyCode == Keys.Escape) // Echap pour quitter le jeu
            {
                jeu.estPerdu = true;
                this.Close();
            }
            if(keyData.KeyCode == Keys.S || keyData.KeyCode == Keys.Down) // Descendre la pièce plus rapidement
            {
                if(jeu.pieceCourante.PeuxDescendre() && !Jeu.enPause) // Si la piece peut descendre et que le jeu n'est pas en pause
                {
                    jeu.pieceCourante.descendre(); // Alors on descend la pièce
                    jeu.score += jeu.pointDescente;
                }
            }
            Refresh(); // Et dans tous les cas on rafraichi l'interface graphique à la fin
            
        }

        private void InterfaceGraphique_FormClosed(object sender, FormClosedEventArgs e) // Quand on clique sur la croix rouge
        {
            jeu.RaiseCustomEvent -= HandleCustomEvent; // On supprime les catch d'evements
            jeu.YouGameOverEvent -= HandleGameOverEvent;
            jeu.estPerdu = true; // Et on stoppe la boucle infinie
        }
    }
}
