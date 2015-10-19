using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    /**
    *   Interface estTournable
    *   Cette interface permet de faire passer un contrat à la classe pièce qui dit que chaque pièce doit implémenter ces 2 méthodes
    *   Dernière modification : 19/10/2015 par Christophe GERARD
    **/
    interface estTournable
    {
       void Tourner(); // Méthode qui va définir la rotation d'une pièce
       bool peuxTourner(int direction); // Méthode qui va dire si la pièce peuxTourner ou non
    }
}
