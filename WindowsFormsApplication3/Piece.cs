using System;
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
                    if(representation[j,i].estColore)
                    {
                        representation[j, i].estColore = false;
                    }
                }
            }
        }

        abstract public bool Tourner();

        public void draw(Graphics g)
        {
            for(int i = 0; i < hauteurPiece; i++)
            {
                for(int j = 0; j < largeurPiece; j++)
                {
                    if(representation[j,i].estColore)
                    {
                        //Dessiner tous les rectangles
                        SolidBrush brush = new SolidBrush(this.couleur);
                        g.FillRectangle(brush, new Rectangle(new Point(representation[j, i].x * Jeu.TailleCase, representation[j, i].y * Jeu.TailleCase), new Size(32,32)));
                        g.DrawRectangle(new Pen(Color.Black), new Rectangle(new Point(representation[j, i].x * Jeu.TailleCase, representation[j, i].y * Jeu.TailleCase), new Size(32, 32)));
                    }
                }
            }
        }

        public bool PeuxDescendre()
        {
            // Parcours de la piece
            for(int i = 0; i < hauteurPiece; i++)
            {
                for(int j = 0; j < largeurPiece; j++)
                {
                    if(representation[j,i].estColore)
                    {
                        if(representation[j,i].y >= Jeu.NB_CASE_HAUTEUR - 1 || Jeu.plateau[representation[j,i].x,representation[j,i].y + 1].estColore)
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
            for(int i = 0; i < hauteurPiece; i++)
            {
                for(int j = 0; j < largeurPiece; j++)
                {
                  representation[j,i].y++;
                }
            }
        }

        public void deplacer(int direction)
        {
            if(direction == -1)
            {
                for(int i = 0; i < hauteurPiece; i++)
                {
                    for(int j = 0; j < largeurPiece; j++)
                    {
                        representation[j, i].x--;
                    }
                }
            }
            else
            {
                for (int i = 0; i < hauteurPiece; i++)
                {
                    for (int j = 0; j < largeurPiece; j++)
                    {
                        representation[j, i].x++;
                    }
                }
            }
        }
    }
}