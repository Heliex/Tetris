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
    public partial class Debug : Form
    {
        Jeu game;
        public Debug(Jeu jeu)
        {
            InitializeComponent();
            game = jeu; 
            Location = new Point(1000, 0);
            jeu.RaiseCustomEvent += Jeu_RaiseCustomEvent;
            
        }

        private void Jeu_RaiseCustomEvent(object sender, RafraichirGUIEvent e)
        {
            this.Invoke(() => this.RefreshDebug());
        }

        private void RefreshDebug()
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
