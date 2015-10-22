using System.Drawing;

namespace WindowsFormsApplication3
{
    /**
     *   Classe Z
     *   Représentation de la pièce Z dans le jeu Tetris
     *   Dernière modification : 22/10/2015 par Christophe GERARD
     **/
    public class Z : Piece
    {
        public Z()
        {
            offsetPieceHorizontal = 6; // Offset pour placer la pièce au milieu de plateau
            offsetPieceVertical = 0;
            sens = 0;
            hauteurPiece = 4;
            largeurPiece = 4;
            couleur = Color.FromArgb(235, 0, 0);
            representation = new Case[hauteurPiece, largeurPiece];
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    representation[j, i] = new Case(j + offsetPieceHorizontal, i + offsetPieceVertical, this);
                }
            }
            initialiserPiece();
        }

        public void initialiserPiece() // Initialisation de la pièce Z
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if ((i == 1 && (j == 1 || j == 2)) || (i == 2 && j > 1))
                    {
                        representation[j, i].estColore = true;
                    }
                }
            }
        }
        public override bool peuxTourner(int direction) // Redefiniton de la méthode peux Tourner pour la pièce Z
        {
            if(Jeu.plateau != null)
            {
                switch (direction)
                {
                    case 0:
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if ((i == 0 && j == 3) || (i == 1 && j > 1) || (i == 2 && j == 2))
                                {
                                    Case c = representation[j, i];
                                    // Si la pièce dépasse du plateau ou que le jeu est en pause
                                    if (c.x < 0 || c.x >= Jeu.NB_CASE_LARGEUR || c.y >= Jeu.NB_CASE_HAUTEUR || Jeu.plateau[c.x, c.y].estColore || Jeu.enPause)
                                    {
                                        return false; // Alors on peux pas tourner
                                    }
                                }
                            }
                        }
                        break;
                    case 1:
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if ((i == 1 && (j == 1 || j == 2)) || (i == 2 && j > 1))
                                {
                                    Case c = representation[j, i];
                                    if (c.x < 0 || c.x >= Jeu.NB_CASE_LARGEUR || c.y >= Jeu.NB_CASE_HAUTEUR || Jeu.plateau[c.x, c.y].estColore || Jeu.enPause)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public override void Tourner() // Redéfinition de la méthode tourner
        {
            switch (sens)
            {
                case 0: // Vers le haut
                    if (peuxTourner(0))
                    {
                        decolorerPiece();
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if ((i == 0 && j == 3) || (i == 1 && j > 1 ) || (i == 2 && j == 2))
                                {
                                    representation[j, i].estColore = true;
                                }
                            }
                        }
                        sens++;
                    }
                    break;
                case 1: // Vers le bas
                    if (peuxTourner(1))
                    {
                        decolorerPiece();
                        initialiserPiece();
                        sens = 0;
                    }
                    break;
                default:

                    break;
            }
        }
    }
}
