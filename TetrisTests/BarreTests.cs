using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace WindowsFormsApplication3.Tests
{
    [TestClass()]
    public class BarreTests
    {
        // Test qui vérifie qu'une barre est bien crée quand on appelle le constructeur de Barre.
        [TestMethod()]
        public void BarreTest()
        {
            Barre barre = new Barre();
            Assert.AreEqual(0, barre.sens);
            Assert.AreEqual(6, barre.offsetPieceHorizontal);
            Assert.AreEqual(0, barre.offsetPieceVertical);
            Assert.AreEqual(4, barre.hauteurPiece);
            Assert.AreEqual(4, barre.largeurPiece);
            Assert.AreSame(new Case[barre.hauteurPiece, barre.largeurPiece].GetType(), barre.representation.GetType());
        }

        // Test qui vérifie qu'a l'initialisation de la barre on à bien coloré les bonnes partie
        [TestMethod()]
        public void initialiserPieceTest()
        {
            Barre barre = new Barre();
            for (int i = 0; i < barre.hauteurPiece; i++)
            {
                for (int j = 0; j < barre.largeurPiece; j++)
                {
                    if (i == 1)
                    {
                        Assert.AreEqual(true, barre.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void descendreTest() // Test qui test la méthode de descente d'une pièce
        {
            Barre barre = new Barre();
            ArrayList yBefore = new ArrayList(); // Les deux listes font la même tailles
            ArrayList yAfter = new ArrayList();
            for (int i = 0; i < barre.hauteurPiece; i++)
            {
                for (int j = 0; j < barre.largeurPiece; j++)
                {
                    yBefore.Add(barre.representation[j, i].y);// Stocke les ordonnées avant la descente
                }
            }
            barre.descendre();
            for (int i = 0; i < barre.hauteurPiece; i++)
            {
                for (int j = 0; j < barre.largeurPiece; j++)
                {
                    yAfter.Add(barre.representation[j, i].y);// Stocke les ordonnées après la descente
                }
            }
            for (int i = 0; i < yBefore.Count; i++)
            {
                Assert.AreEqual((int)yBefore[i] + 1, (int)yAfter[i]);// Teste que l'ordonnée d'avant + 1 est égal à l'ordonnée d'après
            }
        }

        [TestMethod()]
        public void peuxDeplacerTest() // Test qui test la méthode peuxDeplacer
        {
            Barre barre = new Barre();
            Assert.AreEqual(false, barre.peuxDeplacer(1)); // On ne peux pas déplacer la pièce si le plateau n'existe pas
        }

        [TestMethod()]
        public void PeuxDescendreTest() // Test qui test la méthode peuxDescendre
        {
            Barre barre = new Barre();
            Assert.AreEqual(false, barre.PeuxDescendre()); // On ne peux pas descendre la pièce si le plateau est null (n'existe pas)
        }

        [TestMethod()]
        public void decolorerPieceTest() // Test qui test la méthode decolorerPiece
        {
            Barre barre = new Barre();
            barre.decolorerPiece();
            for (int i = 0; i < barre.hauteurPiece; i++)
            {
                for (int j = 0; j < barre.largeurPiece; j++)
                {
                    Assert.AreEqual(false, barre.representation[j, i].estColore); // On s'assure que la pièce est décolorée
                }
            }
        }

        [TestMethod()]
        public void deplacerTest() // Test la méthode de déplacement
        {
            Barre barre = new Barre();
            ArrayList xAvantDeplacement = new ArrayList(); // Les listes font la même taille
            ArrayList xApresDeplacement = new ArrayList();

            for (int i = 0; i < barre.hauteurPiece; i++)
            {
                for (int j = 0; j < barre.largeurPiece; j++)
                {
                    xAvantDeplacement.Add(barre.representation[j, i].x); // Stocke l'abscisse avant déplacement
                }
            }
            barre.deplacer(1);
            for (int i = 0; i < barre.hauteurPiece; i++)
            {
                for (int j = 0; j < barre.largeurPiece; j++)
                {
                    xApresDeplacement.Add(barre.representation[j, i].x); // Stocke l'abscisse après déplacement
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
            Barre barre = new Barre();

            barre.Tourner(); // La méthode tourner n'a rien fait normalement
            for (int i = 0; i < barre.hauteurPiece; i++)
            {
                for (int j = 0; j < barre.largeurPiece; j++)
                {
                    if (i == 1) // Je test que la pièce n'a pas tournée
                    {
                        Assert.AreEqual(true, barre.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void peuxTournerTest()
        {
            Barre barre = new Barre();
            bool reponse = barre.peuxTourner(2); // Je teste que la barre ne peut pas tourner si le plateau n'existe pas
            Assert.AreEqual(false, reponse);
        }
    }
}