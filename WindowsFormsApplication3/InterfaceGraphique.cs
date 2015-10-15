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
            label1.ForeColor = Color.FromArgb(223,241,239);
            label1.BackColor = Color.FromArgb(37,43,90);
            panel2.Visible = false;
            panel2.BackgroundImage = Image.FromFile("Images/gameover.jpg");
            label1.Font = new Font("Arial",12);
            jeu = new Jeu();
            label1.Text = jeu.score.ToString();
            label1.AutoSize = false;
            label1.Width = 100;
            label1.Height = 20;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            Thread ThreadJeu = new Thread(jeu.lancerJeu);
            ThreadJeu.Start();
            jeu.RaiseCustomEvent += HandleCustomEvent;
            jeu.YouGameOverEvent += HandleGameOverEvent;
            KeyDown += new KeyEventHandler(MyKeyPressedEventHandler);
            new Debug(jeu).Show();
            panel1.BackgroundImage = Image.FromFile("Images/fondTetris.jpg");
        }

        public void HandleCustomEvent(Object sender, RafraichirGUIEvent e)
        {
            this.Invoke(() => this.Refresh());
        }

        public void HandleGameOverEvent(Object sender, GameOverEvent e)
        {
            this.Invoke(() => panel2.Visible = true);
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
            label1.Text = jeu.score.ToString();
        }

        public void MyKeyPressedEventHandler(Object sender ,KeyEventArgs keyData)
        {
            if(keyData.KeyCode == Keys.D || keyData.KeyCode == Keys.Right)
            {
                jeu.pieceCourante.deplacer(1);
            }
            if(keyData.KeyCode == Keys.Q || keyData.KeyCode == Keys.Left)
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
            if(keyData.KeyCode == Keys.Z || keyData.KeyCode == Keys.Up)
            {
                jeu.pieceCourante.Tourner();
            }
            if(keyData.KeyCode == Keys.Escape)
            {
                jeu.estPerdu = true;
                this.Close();
            }
            if(keyData.KeyCode == Keys.S || keyData.KeyCode == Keys.Down)
            {
                if(jeu.pieceCourante.PeuxDescendre() && !Jeu.enPause)
                {
                    jeu.pieceCourante.descendre();
                    jeu.score += jeu.pointDescente;
                }
            }
            Refresh();
            
        }

        private void InterfaceGraphique_FormClosed(object sender, FormClosedEventArgs e)
        {
            jeu.estPerdu = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
          
        }
    }
}
