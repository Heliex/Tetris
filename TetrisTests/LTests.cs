using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace WindowsFormsApplication3.Tests
{
    [TestClass()]
    public class LTests
    {
        [TestMethod()]
        public void LTest()
        {
            L l = new L();
            Assert.AreEqual(0, l.sens);
            Assert.AreEqual(6, l.offsetPieceHorizontal);
            Assert.AreEqual(1, l.offsetPieceVertical);
            Assert.AreEqual(4, l.hauteurPiece);
            Assert.AreEqual(4, l.largeurPiece);
            Assert.AreSame(new Case[l.hauteurPiece, l.largeurPiece].GetType(), l.representation.GetType());
        }

        [TestMethod()]
        public void TournerTest()
        {
            L l = new L();

            l.Tourner(); // La méthode tourner n'a rien fait normalement
            for (int i = 0; i < l.hauteurPiece; i++)
            {
                for (int j = 0; j < l.largeurPiece; j++)
                {
                    // Je test que la pièce n'a pas tournée
                    if (((j == 1 || j == 2) && i == 0) || (i == 1 && j == 2) || (i == 2 && j == 2))
                    {
                        Assert.AreEqual(true, l.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void peuxTournerTest()
        {
            L l = new L();
            bool reponse = l.peuxTourner(2); // Je teste que le L ne peut pas tourner si le plateau n'existe pas
            Assert.AreEqual(false, reponse);
        }

        [TestMethod()]
        public void initialiserPieceTest()
        {
            L l = new L();
            for (int i = 0; i < l.hauteurPiece; i++)
            {
                for (int j = 0; j < l.largeurPiece; j++)
                {
                    if (((j == 1 || j == 2) && i == 0) || (i == 1 && j == 2) || (i == 2 && j == 2))
                    {
                        Assert.AreEqual(true, l.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void decolorerPieceTest()
        {
            L l = new L();
            l.decolorerPiece();
            for (int i = 0; i < l.hauteurPiece; i++)
            {
                for (int j = 0; j < l.largeurPiece; j++)
                {
                    Assert.AreEqual(false, l.representation[j, i].estColore); // On s'assure que la pièce est décolorée
                }
            }
        }

        [TestMethod()]
        public void PeuxDescendreTest()
        {
            L l = new L();
            Assert.AreEqual(false, l.PeuxDescendre()); // On ne peux pas descendre la pièce si le plateau est null (n'existe pas)
        }

        [TestMethod()]
        public void descendreTest()
        {
            L l = new L();
            ArrayList yBefore = new ArrayList(); // Les deux listes font la même tailles
            ArrayList yAfter = new ArrayList();
            for (int i = 0; i < l.hauteurPiece; i++)
            {
                for (int j = 0; j < l.largeurPiece; j++)
                {
                    yBefore.Add(l.representation[j, i].y);// Stocke les ordonnées avant la descente
                }
            }
            l.descendre();
            for (int i = 0; i < l.hauteurPiece; i++)
            {
                for (int j = 0; j < l.largeurPiece; j++)
                {
                    yAfter.Add(l.representation[j, i].y);// Stocke les ordonnées après la descente
                }
            }
            for (int i = 0; i < yBefore.Count; i++)
            {
                Assert.AreEqual((int)yBefore[i] + 1, (int)yAfter[i]);// Teste que l'ordonnée d'avant + 1 est égal à l'ordonnée d'après
            }
        }

        [TestMethod()]
        public void deplacerTest()
        {
            L l = new L();
            ArrayList xAvantDeplacement = new ArrayList(); // Les listes font la même taille
            ArrayList xApresDeplacement = new ArrayList();

            for (int i = 0; i < l.hauteurPiece; i++)
            {
                for (int j = 0; j < l.largeurPiece; j++)
                {
                    xAvantDeplacement.Add(l.representation[j, i].x); // Stocke l'abscisse avant déplacement
                }
            }
            l.deplacer(1);
            for (int i = 0; i < l.hauteurPiece; i++)
            {
                for (int j = 0; j < l.largeurPiece; j++)
                {
                    xApresDeplacement.Add(l.representation[j, i].x); // Stocke l'abscisse après déplacement
                }
            }

            for (int i = 0; i < xAvantDeplacement.Count; i++)
            {
                Assert.AreEqual(xAvantDeplacement[i], xApresDeplacement[i]); // Test que la pièce n'a pas bougée
            }
        }

        [TestMethod()]
        public void peuxDeplacerTest()
        {
            L l = new L();
            Assert.AreEqual(false, l.peuxDeplacer(1)); // On ne peux pas déplacer la pièce si le plateau n'existe pas
        }
    }
}