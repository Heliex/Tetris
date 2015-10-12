using System;
using System.Drawing;

using System.Windows.Forms;
using System.Threading;


namespace WindowsFormsApplication3
{
    public partial class InterfaceGraphique : Form
    {
        Jeu jeu;
        public InterfaceGraphique()
        {
            InitializeComponent();
            jeu = new Jeu();
            Thread ThreadJeu = new Thread(jeu.lancerJeu);
            ThreadJeu.Start();
            jeu.RaiseCustomEvent += HandleCustomEvent;
            KeyDown += new KeyEventHandler(MyKeyPressedEventHandler);
            new Debug(jeu).Show();
        }

        public void HandleCustomEvent(Object sender, RafraichirGUIEvent e)
        {
            this.Invoke(() => this.Refresh());
        }

        private void InterfaceGraphique_Load(object sender, EventArgs e)
        {
        }

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

        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            this.ClientSize = new Size((int)Jeu.NB_CASE_LARGEUR * Jeu.TailleCase, (int)Jeu.NB_CASE_HAUTEUR * Jeu.TailleCase);
            jeu.draw(g);
        }

        public void MyKeyPressedEventHandler(Object sender ,KeyEventArgs keyData)
        {
            if(keyData.KeyCode == Keys.D)
            {
                jeu.pieceCourante.deplacer(1);
            }
            if(keyData.KeyCode == Keys.S)
            {
                jeu.pieceCourante.deplacer(-1);
            }
            if (keyData.KeyCode == Keys.R)
            {
                jeu.resume();
            }
            if (keyData.KeyCode == Keys.P)
            {
                jeu.pause();
            }
            if(keyData.KeyCode == Keys.Z)
            {
                jeu.pieceCourante.Tourner();
            }
            
        }

        private void InterfaceGraphique_FormClosed(object sender, FormClosedEventArgs e)
        {
            jeu.estPerdu = true;
        }
    }
}
