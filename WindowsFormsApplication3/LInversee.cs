using System.Drawing;

namespace WindowsFormsApplication3
{
    /**
    *   Classe LInversee
    *   Représente le pièce LInversee dans le jeu Tetris
    *   Dernière modification : 22/10/2015 par Christophe GERARD
    **/
    public class LInversee : Piece
    {
        public LInversee()
        {
            offsetPieceHorizontal = 6; // Offset pour placer la pièce au milieu de plateau à l'apparition
            offsetPieceVertical = 1;
            sens = 0;
            hauteurPiece = 4;
            largeurPiece = 4;
            couleur = Color.FromArgb(15, 157, 232);
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
        public override void Tourner() // Méthode tourner pour la pièce LInversee (J)
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
                                if ((j == 1 && i == 0) || (i == 1 && j > 0))
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
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if (((j == 2 || j == 3) && i == 0) || (i == 1 && j == 2) || (i == 2 && j == 2))
                                {
                                    representation[j, i].estColore = true;
                                }
                            }
                        }
                        sens++;
                    }

                    break;
                case 2: // Vers la gauche
                    if (peuxTourner(2))
                    {
                        decolorerPiece();
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if (i == 1 && j > 0)
                                {
                                    representation[j, i].estColore = true;
                                }
                                if (i == 2 && j == 3)
                                {
                                    representation[j, i].estColore = true;
                                }
                            }
                        }
                        sens++;
                    }

                    break;
                case 3: // Vers la droite
                    if (peuxTourner(3))
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

        public override bool peuxTourner(int direction) // Méthode qui détermine si la pièce Linversee peux tourner
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
                                if ((j == 1 && i == 0) || (i == 1 && j > 0))
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
                                if (((j == 2 || j == 3) && i == 0) || (i == 1 && j == 2) || (i == 2 && j == 2))
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
                    case 2: // Vers le haut
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if (i == 1 && j > 0 || (i == 2 && j == 3))
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
                    case 3: // Vers le haut
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if ((j == 2 && (i == 0 || i == 1)) || (i == 2 && (j == 1 || j == 2)))
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

        public void initialiserPiece() // Dessine la piece au départ
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if ((j == 2 && (i == 0 || i == 1)) || (i == 2 && (j == 1 || j == 2)))
                    {
                        representation[j, i].estColore = true;
                    }
                }
            }
        }
    }
}
