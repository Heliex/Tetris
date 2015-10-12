﻿using System;
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
                decolorerPiece();
                switch (sens)
                {
                    case 0: 
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
                        break;

                    case 1: // Vers le haut
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
                    break;

                    case 2: // Vers le haut
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
                        break;
                    case 3:
                        initialiserPiece();
                        sens = 0;
                        break;
                    default:
                        break;
                }
            }


        public override bool peuxTourner(int direction)
        {
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
