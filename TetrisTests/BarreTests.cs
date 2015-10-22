using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        public void BarreTest1()
        {
            Barre barre = new Barre();
            for(int i = 0; i < barre.hauteurPiece; i++)
            {
                for(int j = 0; j < barre.largeurPiece; j++)
                {
                    if(i == 1)
                    {
                        Assert.AreEqual(true, barre.representation[j, i].estColore);
                    }
                }
            }
        }
    }
}