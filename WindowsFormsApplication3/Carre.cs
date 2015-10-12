using System.Drawing;

namespace WindowsFormsApplication3
{
    class Carre : Piece
    {
        public Carre()
        {
            hauteurPiece = 4;
            largeurPiece = 4;
            couleur = Color.Yellow;
            representation = new Case[hauteurPiece, largeurPiece];
            for (int i = 0; i < hauteurPiece; i++)
            {
                for (int j = 0; j < largeurPiece; j++)
                {
                    representation[j, i] = new Case(j, i, this);
                }
            }
            initialiserPiece();
        }


        public override bool Tourner()
        {
            return true;
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
