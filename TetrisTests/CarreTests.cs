using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace WindowsFormsApplication3.Tests
{
    [TestClass()]
    public class CarreTests
    {
        // Test qui vérifie qu'un carré est bien crée quand on appelle le constructeur de Carre.
        [TestMethod()]
        public void CarreTest()
        {
            Carre carre = new Carre();
            Assert.AreEqual(0, carre.sens);
            Assert.AreEqual(6, carre.offsetPieceHorizontal);
            Assert.AreEqual(0, carre.offsetPieceVertical);
            Assert.AreEqual(4, carre.hauteurPiece);
            Assert.AreEqual(4, carre.largeurPiece);
            Assert.AreSame(new Case[carre.hauteurPiece, carre.largeurPiece].GetType(), carre.representation.GetType());
        }

        // Test qui vérifie qu'a l'initialisation du carré on à bien coloré les bonnes partie
        [TestMethod()]
        public void initialiserPieceTest()
        {
            Carre carre = new Carre();
            for (int i = 0; i < carre.hauteurPiece; i++)
            {
                for (int j = 0; j < carre.largeurPiece; j++)
                {
                    if (i == 1 || i == 2)
                    {
                        if (j > 0 && j < 3)
                        {
                            Assert.AreEqual(true, carre.representation[j, i].estColore);
                        }
                    }
                }
            }
        }

        [TestMethod()]
        public void descendreTest() // Test qui test la méthode Descendre
        {
            Carre carre = new Carre();
            ArrayList yBefore = new ArrayList(); // Les deux listes font la même tailles
            ArrayList yAfter = new ArrayList();
            for (int i = 0; i < carre.hauteurPiece; i++)
            {
                for (int j = 0; j < carre.largeurPiece; j++)
                {
                    yBefore.Add(carre.representation[j, i].y); // Stocke les ordonnées avant la descente
                }
            }
            carre.descendre();
            for (int i = 0; i < carre.hauteurPiece; i++)
            {
                for (int j = 0; j < carre.largeurPiece; j++)
                {
                    yAfter.Add(carre.representation[j, i].y); // Stocke les ordonnées après la descente
                }
            }
            for (int i = 0; i < yBefore.Count; i++)
            {
                Assert.AreEqual((int)yBefore[i] + 1, (int)yAfter[i]); // Teste que l'ordonnée d'avant + 1 est égal à l'ordonnée d'après
            }
        }

        [TestMethod()]
        public void peuxDeplacerTest() // Test qui test la méthode peuxDeplacer
        {
            Carre carre = new Carre();
            Assert.AreEqual(false, carre.peuxDeplacer(-1)); // On ne peux pas déplacer la pièce si le plateau n'existe pas
            Assert.AreEqual(false, carre.peuxDeplacer(1));
        }

        [TestMethod()]
        public void PeuxDescendreTest() // Test qui test la méthode peuxDescendre
        {
            Carre carre = new Carre();
            Assert.AreEqual(false, carre.PeuxDescendre()); // On ne peux pas descendre la pièce si le plateau est null (n'existe pas)
        }

        [TestMethod()]
        public void decolorerPieceTest() // Test qui test la méthode décolorerPiece
        {
            Carre carre = new Carre();
            carre.decolorerPiece();
            for (int i = 0; i < carre.hauteurPiece; i++)
            {
                for (int j = 0; j < carre.largeurPiece; j++)
                {
                    Assert.AreEqual(false, carre.representation[j, i].estColore); // On s'assure que la pièce est décolorée
                }
            }
        }

        [TestMethod()]
        public void deplacerTest()
        {
            Carre carre = new Carre();
            ArrayList xAvantDeplacement = new ArrayList(); // Les listes font la même taille
            ArrayList xApresDeplacement = new ArrayList();

            for (int i = 0; i < carre.hauteurPiece; i++)
            {
                for (int j = 0; j < carre.largeurPiece; j++)
                {
                    xAvantDeplacement.Add(carre.representation[j, i].x); // Stocke l'abscisse avant déplacement
                }
            }
            carre.deplacer(1);
            for (int i = 0; i < carre.hauteurPiece; i++)
            {
                for (int j = 0; j < carre.largeurPiece; j++)
                {
                    xApresDeplacement.Add(carre.representation[j, i].x); // Stocke l'abscisse après déplacement
                }
            }

            for (int i = 0; i < xAvantDeplacement.Count; i++)
            {
                Assert.AreEqual(xAvantDeplacement[i], xApresDeplacement[i]); // Test que la pièce n'a pas bougée
            }
        }

        [TestMethod()]
        public void peuxTournerTest()
        {
            Carre carre = new Carre();
            bool reponse = carre.peuxTourner(2); // Je vérifie que la pièce ne peux pas tourner si le plateau n'existe pas
            Assert.AreEqual(false, reponse);
        }

        [TestMethod()]
        public void TournerTest()
        {
            Carre carre = new Carre();
            carre.Tourner(); // La méthode tourner n'a rien fait normalement
            for (int i = 0; i < carre.hauteurPiece; i++)
            {
                for (int j = 0; j < carre.largeurPiece; j++)
                {
                    // Les cases colorées sont normalement les même qu'a l'initialisation
                    if (i == 1 || i == 2)// Je test que la pièce n'a pas tournée
                    {
                        if (j > 0 && j < 3)
                        {
                            Assert.AreEqual(true, carre.representation[j, i].estColore);
                        }
                    }
                }
            }
        }
    }
}