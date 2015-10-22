using System;
using System.Drawing;


namespace WindowsFormsApplication3
{
    /**
    *   Classe Case
    *   Cette classe représente la définition d'une case
    *   Dernière modification : 22/10/2015 par Christophe GERARD
    **/
    public class Case
    {
        public int x { get; set; } // Attribut qui représente l'abscisse de la case
        public int y { get; set; } // Attribut qui représente l'ordonnée de la case
        private Piece piece; // Une case peut être lié à une piece
        public bool estColore { get; set; } // Une case peut être colorée

        public Case(int x, int y, Piece piece = null) // COnstructeur de case
        {
            this.x = x;
            this.y = y;
            this.piece = piece;
            estColore = false;
        }

        internal void draw(Graphics g) // Classe qui permet de dessiner la case par elle même
        {
            if (this.estColore) // Si la case est colorée
            {
                if (piece != null) // ET que la piece n'est pas null ( elle est lié à la case)
                {
                    // Alors on la dessine.
                    g.FillRectangle(new SolidBrush(piece.couleur), new Rectangle(new Point((x * Jeu.TailleCase), (y * Jeu.TailleCase)), new Size(Jeu.TailleCase, Jeu.TailleCase)));
                    g.DrawRectangle(new Pen(Color.Black), new Rectangle(new Point(x * Jeu.TailleCase, y * Jeu.TailleCase), new Size(Jeu.TailleCase, Jeu.TailleCase)));
                }
            }
        }

        public void ClonerCase(Case aCloner) // Permet de copier une case (Utile pour le décalage de ligne dans le plateau)
        {
            this.estColore = aCloner.estColore;
            this.piece = aCloner.piece;
        }

        public override bool Equals(Object c) // Redéfinition du equals entre 2 case.
        {
            Case caseCourante = c as Case;
            if (this.estColore == caseCourante.estColore && this.x == caseCourante.x && this.y == caseCourante.y)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode() // Methode qui renvoie le hashcode de la case.
        {
            return base.GetHashCode();
        }
    }
}
