using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace WindowsFormsApplication3.Tests
{
    [TestClass()]
    public class TTests
    {
        [TestMethod()]
        public void TTest()
        {
            T t = new T();
            Assert.AreEqual(0, t.sens);
            Assert.AreEqual(6, t.offsetPieceHorizontal);
            Assert.AreEqual(0, t.offsetPieceVertical);
            Assert.AreEqual(4, t.hauteurPiece);
            Assert.AreEqual(4, t.largeurPiece);
            Assert.AreSame(new Case[t.hauteurPiece, t.largeurPiece].GetType(), t.representation.GetType());
        }

        [TestMethod()]
        public void initialiserPieceTest()
        {
            T t = new T();
            for (int i = 0; i < t.hauteurPiece; i++)
            {
                for (int j = 0; j < t.largeurPiece; j++)
                {
                    if ((i == 1 && j > 0) || (i == 2 && j == 2))
                    {
                        Assert.AreEqual(true, t.representation[j, i].estColore);
                    }
                }
            }
        }

        [TestMethod()]
        public void decolorerPieceTest()
        {
            T t = new T();
            t.decolorerPiece();
            for (int i = 0; i < t.hauteurPiece; i++)
            {
                for (int j = 0; j < t.largeurPiece; j++)
                {
                    Assert.AreEqual(false, t.representation[j, i].estColore); // On s'assure que la pièce est décolorée
                }
            }
        }

        [TestMethod()]
        public void PeuxDescendreTest()
        {
            T t = new T();
            Assert.AreEqual(false, t.PeuxDescendre()); // On ne peux pas descendre la pièce si le plateau est null (n'existe pas)
        }

        [TestMethod()]
        public void descendreTest()
        {
            T t = new T();
            ArrayList yBefore = new ArrayList(); // Les deux listes font la même tailles
            ArrayList yAfter = new ArrayList();
            for (int i = 0; i < t.hauteurPiece; i++)
            {
                for (int j = 0; j < t.largeurPiece; j++)
                {
                    yBefore.Add(t.representation[j, i].y);// Stocke les ordonnées avant la descente
                }
            }
            t.descendre();
            for (int i = 0; i < t.hauteurPiece; i++)
            {
                for (int j = 0; j < t.largeurPiece; j++)
                {
                    yAfter.Add(t.representation[j, i].y);// Stocke les ordonnées après la descente
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
            T t = new T();
            ArrayList xAvantDeplacement = new ArrayList(); // Les listes font la même taille
            ArrayList xApresDeplacement = new ArrayList();

            for (int i = 0; i < t.hauteurPiece; i++)
            {
                for (int j = 0; j < t.largeurPiece; j++)
                {
                    xAvantDeplacement.Add(t.representation[j, i].x); // Stocke l'abscisse avant déplacement
                }
            }
            t.deplacer(1);
            for (int i = 0; i < t.hauteurPiece; i++)
            {
                for (int j = 0; j < t.largeurPiece; j++)
                {
                    xApresDeplacement.Add(t.representation[j, i].x); // Stocke l'abscisse après déplacement
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
            T t = new T();
            Assert.AreEqual(false, t.peuxDeplacer(1)); // On ne peux pas déplacer la pièce si le plateau n'existe pas
        }

        [TestMethod()]
        public void peuxTournerTest()
        {
            T t = new T();
            bool reponse = t.peuxTourner(2); // Je teste que la barre ne peut pas tourner si le plateau n'existe pas
            Assert.AreEqual(false, reponse);
        }

        [TestMethod()]
        public void TournerTest()
        {
            T t = new T();
            t.Tourner(); // La méthode tourner n'a rien fait normalement
            for (int i = 0; i < t.hauteurPiece; i++)
            {
                for (int j = 0; j < t.largeurPiece; j++)
                {
                    if ((i == 1 && j > 0) || (i == 2 && j == 2)) // Je test que la pièce n'a pas tournée
                    {
                        Assert.AreEqual(true, t.representation[j, i].estColore);
                    }
                }
            }
        }
    }
}