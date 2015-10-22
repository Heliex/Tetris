using System.Drawing;

namespace WindowsFormsApplication3
{
    /**
    *   Classe Carre
    *   Cette classe permet de définir le carré dans tetris
    *   Dernière modification : 19/10/2015 par Christophe GERARD
    **/
    class Carre : Piece
    {
        public Carre()
        {
            // Initialisation du carré
            offsetPieceHorizontal = 6;
            offsetPieceVertical = 0;
            hauteurPiece = 4;
            largeurPiece = 4;
            couleur = Color.FromArgb(239, 216, 9);
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

        // Le carré n'a pas besoin d'être tourné
        public override void Tourner()
        {
            
        }

        public override bool peuxTourner(int direction)
        {
            return true; // Le carré n'a pas besoin d'être tourné
        }
        public void initialiserPiece() // Dessine la piece au départ
        {
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    if (i == 1 || i == 2)
                    {
                        if (j > 0 && j < 3)
                        {
                          representation[j, i].estColore = true;
                        }
                    }
                }
            }
        }
    }
}
