using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class LInversee : Piece
    {
        public LInversee()
        {
            sens = 0;
            hauteurPiece = 4;
            largeurPiece = 4;
            couleur = Color.GreenYellow;
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
            bool aTourner = true;
            decolorerPiece();
            switch (sens)
            {
                case 0: // Vers le haut
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
                    break;
                case 1: // Vers le bas
                    
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
                    break;
                case 2: // Vers la gauche
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
                    break;
                case 3: // Vers la droite
                    initialiserPiece();
                    sens = 0;
                    break;
                default:
                    aTourner = false;
                    break;
            }
            return aTourner;
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
