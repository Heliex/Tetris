using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    /**
    *   Classe Debug
    *   Permet de débugger le plateau
    *   Dernière modification : 19/10/2015 par Christophe GERARD
    **/
    public partial class Debug : Form
    {
        Jeu game; // Jeu
        public Debug(Jeu jeu)
        {
            InitializeComponent();
            game = jeu; 
            Location = new Point(1000, 0);
            jeu.RaiseCustomEvent += Jeu_RaiseCustomEvent; // On attrape l'evenement rafraichirGUI
            
        }

        private void Jeu_RaiseCustomEvent(object sender, RafraichirGUIEvent e)
        {
            this.Invoke(() => this.RefreshDebug());
        }

        private void RefreshDebug() // Cette méthode appelle le ToString de la classe Jeu et l'affiche dans son interface graphique
        {
            textBox1.Text = game.ToString();
        }

        public void Invoke(Action action)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
