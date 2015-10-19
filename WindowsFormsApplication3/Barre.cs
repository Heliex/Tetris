using System.Drawing;

namespace WindowsFormsApplication3
{
    /**
    *   Classe Barre
    *   Représente la Barre dans le jeu Tetris
    *   Dernière modification : 19/10/2015 par Christophe GERARD
    **/
    public class Barre : Piece
    {
        
        public Barre()
        {
            // Initialisation de la pièce
            sens = 0;
            offsetPieceHorizontal = 6;
            offsetPieceVertical = 0;
            hauteurPiece = 4;
            largeurPiece = 4;
            couleur = Color.FromArgb(121, 248, 248);
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

        public override void Tourner() // Méthode qui définit la rotation pour une pièce
        {
                switch (sens)
                {
                    case 0: // Vers le haut
                    if(peuxTourner(0)) // Si la pièce peux tourner
                    {
                        decolorerPiece();
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if (j == 2)
                                {
                                    representation[j, i].estColore = true;
                                }
                            }
                        }
                        sens++;
                    }  
                    break;
                    case 1: // Vers le bas
                    if(peuxTourner(1))
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

        public override bool peuxTourner(int direction) // Définition de la méthode peuxTourner pour une barre
        {
            switch(direction)
            {
                case 0:
                    for (int i = 0; i < hauteurPiece; i++)
                    {
                        for (int j = 0; j < largeurPiece; j++)
                        {
                            if (j == 2)
                            {
                                Case c = representation[j, i];
                                if(c.x < 0 || c.x >= Jeu.NB_CASE_LARGEUR || c.y >= Jeu.NB_CASE_HAUTEUR || Jeu.plateau[c.x, c.y].estColore || Jeu.enPause)
                                {
                                    return false;
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
                            if (i == 1)
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
            return true;
        }

        public void initialiserPiece() // Dessine la piece au départ
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if (i == 1)
                    {
                      representation[j, i].estColore = true;
                    }
                }
            }
        }
    }
}
