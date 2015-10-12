using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    public class Case
    {
        public int x { get; set; }
        public int y { get; set; }
        private Piece piece;
        public bool estColore { get; set; }
        
        public Case(int x, int y, Piece piece = null)
        {
            this.x = x;
            this.y = y;
            this.piece = piece;
            estColore = false;
        }

        internal void draw(Graphics g)
        {
            if(this.estColore)
            {
                if(piece != null)
                {
                    g.FillRectangle(new SolidBrush(piece.couleur), new Rectangle(new Point((x * Jeu.TailleCase),(y * Jeu.TailleCase)), new Size(Jeu.TailleCase, Jeu.TailleCase)));
                    g.DrawRectangle(new Pen(Color.Black), new Rectangle(new Point(x * Jeu.TailleCase ,y * Jeu.TailleCase), new Size(Jeu.TailleCase, Jeu.TailleCase)));
                } 
            }
        }

        public void ClonerCase(Case aCloner)
        {
            this.estColore = aCloner.estColore;
            this.piece = aCloner.piece;
        }

        public override bool Equals(Object c)
        {
            Case caseCourante = c as Case;
            if(this.estColore == caseCourante.estColore && this.x == caseCourante.x && this.y == caseCourante.y )
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
