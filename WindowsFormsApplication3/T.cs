using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    
        public class T : Piece
        {

            public T()
            {
                sens = 0;
                hauteurPiece = 4;
                largeurPiece = 4;
                couleur = Color.Blue;
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
            public override void Tourner()
            {
                switch (sens)
                {
                    case 0:
     
                    if (peuxTourner(0))
                    {
                        decolorerPiece();
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if ((i == 0 && j == 2) || (i == 1 && j > 1) || (i == 2 && j == 2))
                                {
                                    representation[j, i].estColore = true;
                                }
                            }
                        }
                        sens++;
                    }
                    break;
                    
                    case 1: // Vers le haut
                    if(peuxTourner(1))
                    {
                        decolorerPiece();
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if ((i == 0 && j == 2) || (i == 1 && j > 0))
                                {
                                    representation[j, i].estColore = true;
                                }
                            }
                        }
                        sens++;
                    }
                    break;

                    case 2: // Vers le haut
                    if(peuxTourner(2))
                    {
                        decolorerPiece();
                        for (int i = 0; i < hauteurPiece; i++)
                        {
                            for (int j = 0; j < largeurPiece; j++)
                            {
                                if ((i == 0 && j == 2) || (i == 1 && j > 0 && j < 3) || (i == 2 && j == 2))
                                {
                                    representation[j, i].estColore = true;
                                }
                            }
                        }
                        sens++;
                    }
                        break;
                    case 3:
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


        public override bool peuxTourner(int direction)
        {
            switch (direction)
            {
                case 0:
                    for (int i = 0; i < hauteurPiece; i++)
                    {
                        for (int j = 0; j < largeurPiece; j++)
                        {
                            Case c = representation[j, i];
                            if (c.x < 0 || c.x >= Jeu.NB_CASE_HAUTEUR || Jeu.plateau[c.x, c.y].estColore)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < hauteurPiece; i++)
                    {
                        for (int j = 0; j < largeurPiece; j++)
                        {
                            Case c = representation[j, i];
                            if (c.x < 0 || c.x >= Jeu.NB_CASE_HAUTEUR || Jeu.plateau[c.x, c.y].estColore)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case 2: // Vers le haut
                    for (int i = 0; i < hauteurPiece; i++)
                    {
                        for (int j = 0; j < largeurPiece; j++)
                        {
                            Case c = representation[j, i];
                            if (c.x < 0 || c.x >= Jeu.NB_CASE_HAUTEUR || Jeu.plateau[c.x, c.y].estColore)
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case 3: // Vers le haut
                    for (int i = 0; i < hauteurPiece; i++)
                    {
                        for (int j = 0; j < largeurPiece; j++)
                        {
                            if ((i == 1 && j > 0) || (i == 2 && j == 2))
                            {
                                Case c = representation[j, i];
                                if (c.x < 0 || c.x >= Jeu.NB_CASE_HAUTEUR || Jeu.plateau[c.x, c.y].estColore)
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
                        if ((i == 1 && j > 0) || (i == 2 && j == 2))
                        {
                            representation[j, i].estColore = true;
                        }
                    }
                }
            }
        }
}
