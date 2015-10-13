using System.Drawing;

namespace WindowsFormsApplication3
{
    public abstract class Piece
    {
        public int sens;
        public int hauteurPiece;
        public int largeurPiece;

        public Color couleur;
        public Case[,] representation;



        public void decolorerPiece()
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if (representation[j, i].estColore)
                    {
                        representation[j, i].estColore = false;
                    }
                }
            }
        }

        abstract public void Tourner();
        abstract public bool peuxTourner(int direction);

        public void draw(Graphics g)
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if (representation[j, i].estColore)
                    {
                        //Dessiner tous les rectangles
                        SolidBrush brush = new SolidBrush(this.couleur);
                        g.FillRectangle(brush, new Rectangle(new Point(representation[j, i].x * Jeu.TailleCase, representation[j, i].y * Jeu.TailleCase), new Size(32, 32)));
                        g.DrawRectangle(new Pen(Color.FromArgb(0,0,0)), new Rectangle(new Point(representation[j, i].x * Jeu.TailleCase, representation[j, i].y * Jeu.TailleCase), new Size(32, 32)));
                    }
                }
            }
        }

        public bool PeuxDescendre()
        {
            // Parcours de la piece
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if (representation[j, i].estColore)
                    {
                        if (representation[j, i].y >= Jeu.NB_CASE_HAUTEUR - 1 || Jeu.plateau[representation[j, i].x, representation[j, i].y + 1].estColore)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public void descendre()
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    representation[j, i].y++;
                }
            }
        }

        public void deplacer(int direction)
        {
            if (peuxDeplacer(direction))
            {
                for (int i = 0; i < hauteurPiece; i++)
                {
                    for (int j = 0; j < largeurPiece; j++)
                    {
                        representation[j, i].x = representation[j, i].x + direction;
                    }
                }
            }
        }

        public bool peuxDeplacer(int direction)
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    Case c = representation[j, i];
                    if (c.estColore)
                    {
                        if ((c.x + direction < 0 || c.x + direction > Jeu.NB_CASE_LARGEUR - 1) || (Jeu.plateau[c.x + direction, c.y].estColore))
                        {
                            return false;
                        }

                    }
                }
            }

            return true;
        }
    }
}