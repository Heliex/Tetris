using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace WindowsFormsApplication3.Tests
{
    [TestClass()]
    public class STests
    {
        [TestMethod()]
        public void STest()
        {
            S s = new S();
            Assert.AreEqual(0, s.sens);
            Assert.AreEqual(6, s.offsetPieceHorizontal);
            Assert.AreEqual(0, s.offsetPieceVertical);
            Assert.AreEqual(4, s.hauteurPiece);
            Assert.AreEqual(4, s.largeurPiece);
            Assert.AreSame(new Case[s.hauteurPiece, s.largeurPiece].GetType(), s.representation.GetType());
        }

        [TestMethod()]
        public void initialiserPieceTest()
        {
            S s = new S();
            for (int i = 0; i < s.hauteurPiece; i++)
            {
                for (int j = 0; j < s.largeurPiece; j++)
                {
                    if ((i == 1 && j > 1) || (i == 2 && (j == 1 || j == 2)))
                    {
                        Assert.AreEqual(true, s.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void decolorerPieceTest()
        {
            S s = new S();
            s.decolorerPiece();
            for (int i = 0; i < s.hauteurPiece; i++)
            {
                for (int j = 0; j < s.largeurPiece; j++)
                {
                    Assert.AreEqual(false, s.representation[j, i].estColore); // On s'assure que la pièce est décolorée
                }
            }
        }

        [TestMethod()]
        public void PeuxDescendreTest()
        {
            S s = new S();
            Assert.AreEqual(false, s.PeuxDescendre()); // On ne peux pas descendre la pièce si le plateau est null (n'existe pas)
        }

        [TestMethod()]
        public void descendreTest()
        {
            S s = new S();
            ArrayList yBefore = new ArrayList(); // Les deux listes font la même tailles
            ArrayList yAfter = new ArrayList();
            for (int i = 0; i < s.hauteurPiece; i++)
            {
                for (int j = 0; j < s.largeurPiece; j++)
                {
                    yBefore.Add(s.representation[j, i].y);// Stocke les ordonnées avant la descente
                }
            }
            s.descendre();
            for (int i = 0; i < s.hauteurPiece; i++)
            {
                for (int j = 0; j < s.largeurPiece; j++)
                {
                    yAfter.Add(s.representation[j, i].y);// Stocke les ordonnées après la descente
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
            S s = new S();
            ArrayList xAvantDeplacement = new ArrayList(); // Les listes font la même taille
            ArrayList xApresDeplacement = new ArrayList();

            for (int i = 0; i < s.hauteurPiece; i++)
            {
                for (int j = 0; j < s.largeurPiece; j++)
                {
                    xAvantDeplacement.Add(s.representation[j, i].x); // Stocke l'abscisse avant déplacement
                }
            }
            s.deplacer(1);
            for (int i = 0; i < s.hauteurPiece; i++)
            {
                for (int j = 0; j < s.largeurPiece; j++)
                {
                    xApresDeplacement.Add(s.representation[j, i].x); // Stocke l'abscisse après déplacement
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
            S s = new S();
            Assert.AreEqual(false, s.peuxDeplacer(1)); // On ne peux pas déplacer la pièce si le plateau n'existe pas
        }

        [TestMethod()]
        public void peuxTournerTest()
        {
            S s = new S();
            bool reponse = s.peuxTourner(2); // Je teste que la barre ne peut pas tourner si le plateau n'existe pas
            Assert.AreEqual(false, reponse);
        }

        [TestMethod()]
        public void TournerTest()
        {
            S s = new S();
            s.Tourner(); // La méthode tourner n'a rien fait normalement
            for (int i = 0; i < s.hauteurPiece; i++)
            {
                for (int j = 0; j < s.largeurPiece; j++)
                {
                    if ((i == 1 && j > 1) || (i == 2 && (j == 1 || j == 2))) // Je test que la pièce n'a pas tournée
                    {
                        Assert.AreEqual(true, s.representation[j, i].estColore);
                    }
                }
            }
        }
    }
}