using System.Drawing;

namespace WindowsFormsApplication3
{
    /**
    *   Classe Piece
    *   Classe absraite qui représente le modéle basique d'une pièce
    *   Dernière modification : 19/10/2015 par Christophe GERARD
    **/
    public abstract class Piece
    {
        // Attributs
        public int sens; // Compteur pour le sens
        public int hauteurPiece; // Hauteur de la pièce
        public int largeurPiece; // Largeur de la pièce
        // Ces deux variables servent à placer la pièce à l'initialisation au milieu du plateau de jeu
        public int offsetPieceHorizontal; // Offset sur la ligne horizontale
        public int offsetPieceVertical; // Offset en hauteur
        public Color couleur; // Couleur de la pièce
        public Case[,] representation; // Représentation de la pièce


        // Méthode qui décolore la pièce
        public void decolorerPiece()
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if (representation[j, i].estColore) // Si la case dans la représentation de la pièce est colorée
                    {
                        representation[j, i].estColore = false; // On la décolore
                    }
                }
            }
        }
        // Méthodes abstraites donc non implémentées
        abstract public void Tourner();
        abstract public bool peuxTourner(int direction);

        public void draw(Graphics g) // Méthode draw de la pièce
        {
            for (int i = 0; i < hauteurPiece; i++) // Parcours de la représentation de la pièce
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if (representation[j, i].estColore)
                    {
                        //Dessiner tous les rectangles
                        SolidBrush brush = new SolidBrush(this.couleur);
                        g.FillRectangle(brush, new Rectangle(new Point(representation[j, i].x * Jeu.TailleCase, representation[j, i].y * Jeu.TailleCase), new Size(Jeu.TailleCase, Jeu.TailleCase)));
                        g.DrawRectangle(new Pen(Color.FromArgb(0,0,0)), new Rectangle(new Point(representation[j, i].x * Jeu.TailleCase, representation[j, i].y * Jeu.TailleCase), new Size(Jeu.TailleCase, Jeu.TailleCase)));
                    }
                }
            }
        }

        public bool PeuxDescendre() // Méthode qui definit si la pièce peut descendre
        {
            // Parcours de la piece
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if (representation[j, i].estColore) // Si la case de la représentation de la pièc est colorée
                    {
                        // Si le y de la case dépasse du plateau ou que la case du dessous est colorée
                        if (representation[j, i].y >= Jeu.NB_CASE_HAUTEUR - 1 || Jeu.plateau[representation[j, i].x, representation[j, i].y + 1].estColore)
                        {
                            return false; // On peux pas descendre
                        }
                    }
                }
            }
            return true;
        }

        public void descendre() // Méthode qui descend la pièce
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if(!Jeu.enPause) // Si le jeu est pas en pause 
                    {
                        representation[j, i].y++; // Alors on descends la pièce
                    }
                }
            }
        }

        // Méthode qui permet de déplacer la pièce
        public void deplacer(int direction)
        {
            if (peuxDeplacer(direction))
            {
                for (int i = 0; i < hauteurPiece; i++) // Parcours de la représentation de la pièce
                {
                    for (int j = 0; j < largeurPiece; j++)
                    {
                        representation[j, i].x = representation[j, i].x + direction; // On ajoute au x de la case la direction (1 ou -1)
                    }
                }
            }
        }

        public bool peuxDeplacer(int direction) // Méthode qui définit si on peux déplacer la pièce dans la direction ou non
        {
            for (int i = 0; i < hauteurPiece; i++) // Parcours de la répresentation de la pièce
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    Case c = representation[j, i];
                    if (c.estColore) // si la case est colorée
                    {
                        // Si on depasse du plateau ou si le jeu est en pause
                        if ((c.x + direction < 0 || c.x + direction > Jeu.NB_CASE_LARGEUR - 1) || (Jeu.plateau[c.x + direction, c.y].estColore) || Jeu.enPause)
                        {
                            return false; // Alors on peux pas déplacer la pièce
                        }

                    }
                }
            }

            return true;
        }
    }
}