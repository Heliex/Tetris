using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace WindowsFormsApplication3.Tests
{
    [TestClass()]
    public class ZTests
    {
        [TestMethod()]
        public void ZTest()
        {
            Z z = new Z();
            Assert.AreEqual(0, z.sens);
            Assert.AreEqual(6, z.offsetPieceHorizontal);
            Assert.AreEqual(0, z.offsetPieceVertical);
            Assert.AreEqual(4, z.hauteurPiece);
            Assert.AreEqual(4, z.largeurPiece);
            Assert.AreSame(new Case[z.hauteurPiece, z.largeurPiece].GetType(), z.representation.GetType());
        }

        [TestMethod()]
        public void initialiserPieceTest()
        {
            Z z = new Z();
            for (int i = 0; i < z.hauteurPiece; i++)
            {
                for (int j = 0; j < z.largeurPiece; j++)
                {
                    if ((i == 1 && (j == 1 || j == 2)) || (i == 2 && j > 1))
                    {
                        Assert.AreEqual(true, z.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void decolorerPieceTest()
        {
            Z z = new Z();
            z.decolorerPiece();
            for (int i = 0; i < z.hauteurPiece; i++)
            {
                for (int j = 0; j < z.largeurPiece; j++)
                {
                    Assert.AreEqual(false, z.representation[j, i].estColore); // On s'assure que la pièce est décolorée
                }
            }
        }

        [TestMethod()]
        public void PeuxDescendreTest()
        {
            Z z = new Z();
            Assert.AreEqual(false, z.PeuxDescendre()); // On ne peux pas descendre la pièce si le plateau est null (n'existe pas)
        }

        [TestMethod()]
        public void descendreTest()
        {
            Z z = new Z();
            ArrayList yBefore = new ArrayList(); // Les deux listes font la même tailles
            ArrayList yAfter = new ArrayList();
            for (int i = 0; i < z.hauteurPiece; i++)
            {
                for (int j = 0; j < z.largeurPiece; j++)
                {
                    yBefore.Add(z.representation[j, i].y);// Stocke les ordonnées avant la descente
                }
            }
            z.descendre();
            for (int i = 0; i < z.hauteurPiece; i++)
            {
                for (int j = 0; j < z.largeurPiece; j++)
                {
                    yAfter.Add(z.representation[j, i].y);// Stocke les ordonnées après la descente
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
            Z z = new Z();
            ArrayList xAvantDeplacement = new ArrayList(); // Les listes font la même taille
            ArrayList xApresDeplacement = new ArrayList();

            for (int i = 0; i < z.hauteurPiece; i++)
            {
                for (int j = 0; j < z.largeurPiece; j++)
                {
                    xAvantDeplacement.Add(z.representation[j, i].x); // Stocke l'abscisse avant déplacement
                }
            }
            z.deplacer(1);
            for (int i = 0; i < z.hauteurPiece; i++)
            {
                for (int j = 0; j < z.largeurPiece; j++)
                {
                    xApresDeplacement.Add(z.representation[j, i].x); // Stocke l'abscisse après déplacement
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
            Z z = new Z();
            Assert.AreEqual(false, z.peuxDeplacer(1)); // On ne peux pas déplacer la pièce si le plateau n'existe pas
        }

        [TestMethod()]
        public void peuxTournerTest()
        {
            Z z = new Z();
            bool reponse = z.peuxTourner(2); // Je teste que la barre ne peut pas tourner si le plateau n'existe pas
            Assert.AreEqual(false, reponse);
        }

        [TestMethod()]
        public void TournerTest()
        {
            Z z = new Z();
            z.Tourner(); // La méthode tourner n'a rien fait normalement
            for (int i = 0; i < z.hauteurPiece; i++)
            {
                for (int j = 0; j < z.largeurPiece; j++)
                {
                    if ((i == 1 && (j == 1 || j == 2)) || (i == 2 && j > 1)) // Je test que la pièce n'a pas tournée
                    {
                        Assert.AreEqual(true, z.representation[j, i].estColore);
                    }
                }
            }
        }
    }
}