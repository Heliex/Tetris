using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace WindowsFormsApplication3.Tests
{
    [TestClass()]
    public class LInverseeTests
    {
        [TestMethod()]
        public void LInverseeTest()
        {
            LInversee linversee = new LInversee();
            Assert.AreEqual(0, linversee.sens);
            Assert.AreEqual(6, linversee.offsetPieceHorizontal);
            Assert.AreEqual(1, linversee.offsetPieceVertical);
            Assert.AreEqual(4, linversee.hauteurPiece);
            Assert.AreEqual(4, linversee.largeurPiece);
            Assert.AreSame(new Case[linversee.hauteurPiece, linversee.largeurPiece].GetType(), linversee.representation.GetType());
        }

        [TestMethod()]
        public void initialiserPieceTest()
        {
            LInversee linversee = new LInversee();
            for (int i = 0; i < linversee.hauteurPiece; i++)
            {
                for (int j = 0; j < linversee.largeurPiece; j++)
                {
                    if ((j == 2 && (i == 0 || i == 1)) || (i == 2 && (j == 1 || j == 2)))
                    {
                        Assert.AreEqual(true, linversee.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void descendreTest()
        {
            LInversee linversee = new LInversee();
            ArrayList yBefore = new ArrayList(); // Les deux listes font la même tailles
            ArrayList yAfter = new ArrayList();
            for (int i = 0; i < linversee.hauteurPiece; i++)
            {
                for (int j = 0; j < linversee.largeurPiece; j++)
                {
                    yBefore.Add(linversee.representation[j, i].y);// Stocke les ordonnées avant la descente
                }
            }
            linversee.descendre();
            for (int i = 0; i < linversee.hauteurPiece; i++)
            {
                for (int j = 0; j < linversee.largeurPiece; j++)
                {
                    yAfter.Add(linversee.representation[j, i].y);// Stocke les ordonnées après la descente
                }
            }
            for (int i = 0; i < yBefore.Count; i++)
            {
                Assert.AreEqual((int)yBefore[i] + 1, (int)yAfter[i]);// Teste que l'ordonnée d'avant + 1 est égal à l'ordonnée d'après
            }
        }

        [TestMethod()]
        public void peuxDeplacerTest()
        {
            LInversee linversee = new LInversee();
            Assert.AreEqual(false, linversee.peuxDeplacer(1)); // On ne peux pas déplacer la pièce si le plateau n'existe pas
        }

        [TestMethod()]
        public void PeuxDescendreTest()
        {
            LInversee linversee = new LInversee();
            Assert.AreEqual(false, linversee.PeuxDescendre()); // On ne peux pas descendre la pièce si le plateau est null (n'existe pas)
        }

        [TestMethod()]
        public void decolorerPieceTest()
        {
            LInversee linversee = new LInversee();
            linversee.decolorerPiece();
            for (int i = 0; i < linversee.hauteurPiece; i++)
            {
                for (int j = 0; j < linversee.largeurPiece; j++)
                {
                    Assert.AreEqual(false, linversee.representation[j, i].estColore); // On s'assure que la pièce est décolorée
                }
            }
        }

        [TestMethod()]
        public void deplacerTest()
        {
            LInversee linversee = new LInversee();
            ArrayList xAvantDeplacement = new ArrayList(); // Les listes font la même taille
            ArrayList xApresDeplacement = new ArrayList();

            for (int i = 0; i < linversee.hauteurPiece; i++)
            {
                for (int j = 0; j < linversee.largeurPiece; j++)
                {
                    xAvantDeplacement.Add(linversee.representation[j, i].x); // Stocke l'abscisse avant déplacement
                }
            }
            linversee.deplacer(1);
            for (int i = 0; i < linversee.hauteurPiece; i++)
            {
                for (int j = 0; j < linversee.largeurPiece; j++)
                {
                    xApresDeplacement.Add(linversee.representation[j, i].x); // Stocke l'abscisse après déplacement
                }
            }

            for (int i = 0; i < xAvantDeplacement.Count; i++)
            {
                Assert.AreEqual(xAvantDeplacement[i], xApresDeplacement[i]); // Test que la pièce n'a pas bougée
            }
        }

        [TestMethod()]
        public void TournerTest()
        {
            LInversee linversee = new LInversee();

            linversee.Tourner(); // La méthode tourner n'a rien fait normalement
            for (int i = 0; i < linversee.hauteurPiece; i++)
            {
                for (int j = 0; j < linversee.largeurPiece; j++)
                {
                    if ((j == 2 && (i == 0 || i == 1)) || (i == 2 && (j == 1 || j == 2))) // Je test que la pièce n'a pas tournée
                    {
                        Assert.AreEqual(true, linversee.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void peuxTournerTest()
        {
            LInversee linversee = new LInversee();
            bool reponse = linversee.peuxTourner(2); // Je teste que la barre ne peut pas tourner si le plateau n'existe pas
            Assert.AreEqual(false, reponse);
        }
    }
}