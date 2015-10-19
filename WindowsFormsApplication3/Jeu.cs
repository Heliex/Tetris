using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using NAudio.Wave;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    /**
    *   Classe Jeu
    *   Cette classe contient toute la logique du jeu
    *   Dernière modification : 19/10/2015 par Christophe GERARD
    **/
    public class Jeu
    {
        // Attributs statiques
        public static bool enPause = false;
        public static int COMPTEUR_LIGNE_COMPLETE = 0;
        public static readonly int NB_CASE_HAUTEUR = 16;
        public static readonly int NB_CASE_LARGEUR = 16;
        public static readonly int TailleCase = 32;
        public static Case[,] plateau;

        // Attibruts 
        public int score = 0;
        public int pointDescente = 5;
        public int pointLigneComplete = 100;
        public int pointFinish = 1;
        // Cet attibrut est volatile, ca veux dire qu'il est modifiable par des Thread différents
        public volatile bool estPerdu;

        // Attributs evenements
        public event EventHandler<RafraichirGUIEvent> RaiseCustomEvent;
        public event EventHandler<GameOverEvent> YouGameOverEvent;

        // Attributs "objets"
        public IWavePlayer waveOutDevice;
        public ArrayList pieces = new ArrayList();
        public Piece pieceCourante { get; set; }
        public AudioFileReader audioFileReader;
        public Random randNum;
        public ManualResetEvent resetEvent;
        // Dictionnaire de données pour regler la vitesse de descente selon le nombre de ligne compléte
        public Dictionary<double, int> intervalle = new Dictionary<double, int>()
        {
            {
                0,500
            },
            {
                10,450
            },
            {
                20,400
            },
            {
                30,350
            }
            ,
            {
                40,300
            }
            ,
            {
                50,250
            }
            ,
            {
                60,200
            }
            ,
            {
                100,150
            }
            ,
            {
                200,100
            }
            ,
            {
                300,50
            }
        };


        public Jeu() // Constructeur par défaut
        {
            // Initialisation des variables
            resetEvent = new ManualResetEvent(true);
            randNum = new Random();
            plateau = new Case[NB_CASE_HAUTEUR, NB_CASE_LARGEUR];
            estPerdu = false;
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader("Musiques/Tetris.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
            for (int i = 0; i < NB_CASE_HAUTEUR; i++)
            {
                for (int j = 0; j < NB_CASE_LARGEUR; j++)
                {
                    plateau[j, i] = new Case(j, i);
                }
            }
            pieceCourante = pieceSuivante(); // On récupére la pièce suivante
        }

        // Méthode qui définit l'evenement RafrachirGUI
        protected virtual void OnRaiseCustomEvent(RafraichirGUIEvent e)
        {
            EventHandler<RafraichirGUIEvent> handler = RaiseCustomEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        // Méthode qui définit l'evenement GameOverEvent
        protected virtual void OnGameOverEvent(GameOverEvent e)
        {
            EventHandler<GameOverEvent> handler = YouGameOverEvent;
            if(handler != null)
            {
                handler(this, e);
            }
        }
        public void lancerJeu() // Methode de jeu principale
        {
            while (!estPerdu) // Boucle infinie
            {
                resetEvent.WaitOne();
                double palier = Math.Round(COMPTEUR_LIGNE_COMPLETE / 10.0) * 10;
                if(intervalle[palier] > 50)
                {
                    Thread.Sleep(intervalle[palier]);
                }
                else
                {
                    Thread.Sleep(50);
                }
                int lastTick = System.Environment.TickCount;

                OnRaiseCustomEvent(new RafraichirGUIEvent()); // Rafraichissement du GUI
                if (pieceCourante.PeuxDescendre()) // Si la piece peux descendre
                {
                    pieceCourante.descendre();
                    for (int i = 0; i < NB_CASE_HAUTEUR; i++) // Parcours de chaque ligne
                    {
                        if (ligneEstComplete(plateau, i)) // Si elle est complete
                        {
                            COMPTEUR_LIGNE_COMPLETE++;
                            score += pointLigneComplete;
                            // Suppression de la ligne
                            for (int j = 0; j < NB_CASE_LARGEUR; j++)
                            {
                                plateau[j, i].estColore = false;
                            }
                            // Décaler toutes les lignes au dessus dans le plateau
                            decalerLignesAuDessus(i);
                        }
                    }
                }
                else if (gameOver())
                {
                    pieceCourante.decolorerPiece();
                    estPerdu = true;
                    RaiseCustomEvent = null;
                    waveOutDevice.Stop();

                    new Task(() => 
                    {
                        AudioFileReader reader = new AudioFileReader("Musiques/gameover.wav");
                        WaveOut newWaveOut = new WaveOut();
                        newWaveOut.Init(reader);
                        newWaveOut.Play();
                    }).Start();
                    
                    for (int i = 0; i < NB_CASE_HAUTEUR; i++)
                    {
                        for (int j = 0; j < NB_CASE_LARGEUR; j++)
                        {
                            plateau[j, i].estColore = false;
                        }
                    }
                    OnGameOverEvent(new GameOverEvent());
                }
                else // Sinon
                {
                    score += pointFinish;
                    // Parcours de la piece
                    for (int i = 0; i < pieceCourante.hauteurPiece; i++)
                    {
                        for (int j = 0; j < pieceCourante.largeurPiece; j++)
                        {
                            Case c = pieceCourante.representation[j, i];
                            if (c.estColore)
                            {
                                if (c.y < 16)
                                {
                                    plateau[c.x, c.y] = c;
                                    if (plateau[c.x, c.y].estColore && c.y == 0)
                                    {
                                        estPerdu = true;
                                    }
                                }
                            }
                        }
                    }
                    pieceCourante = pieceSuivante();
                }
            }
        }

        private void decalerLignesAuDessus(int indice)
        {
            if (indice > 0)
            {
                for (int i = indice; i > 0; i--)
                {
                    for (int j = 0; j < NB_CASE_LARGEUR; j++)
                    {
                        plateau[j, i].ClonerCase(plateau[j, i - 1]);
                    }
                }
            }
        }

        public Piece pieceSuivante()
        {
            // Nouvelle piece
            Piece piece = null;
            int numPiece = randNum.Next(1, 6);
            if (numPiece == 1)
            {
                piece = new Barre();
            }
            if (numPiece == 2)
            {
                piece = new T();
            }
            if (numPiece == 3)
            {
                piece = new L();
            }
            if (numPiece == 4)
            {
                piece = new Carre();
            }
            if (numPiece == 5)
            {
                piece = new LInversee();
            }
            return piece;
        }


        public void draw(Graphics g)
        {
            for (int i = 0; i < NB_CASE_HAUTEUR; i++)
            {
                for (int j = 0; j < NB_CASE_LARGEUR; j++)
                {

                    if (plateau[j, i].estColore)
                    {
                        plateau[j, i].draw(g);
                    }

                }
            }
            pieceCourante.draw(g);

        }

        public bool gameOver()
        {
            int limite = 0;
            
            if(pieceCourante.GetType() == typeof(Barre) || pieceCourante.GetType() == typeof(Carre) || pieceCourante.GetType() == typeof(T))
            {
                limite = 2;
            }
            else if(pieceCourante.GetType() == typeof(L) || pieceCourante.GetType() == typeof(LInversee))
            {
                limite = 3;
            }

            for(int i = limite; i < limite+1; i++)
            {
                for(int j = 0; j < NB_CASE_LARGEUR; j++)
                {
                    if(plateau[j,i].estColore)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool ligneEstComplete(Case[,] plateau, int indice)
        {
            bool estComplete = true;

            for (int j = 0; j < NB_CASE_LARGEUR; j++)
            {
                if (!plateau[j, indice].estColore)
                {
                    estComplete = false;
                    break;
                }
            }
            return estComplete;
        }

        public void pause()
        {
            waveOutDevice.Stop();
            resetEvent.Reset();
            enPause = true;
        }

        public void resume()
        {
            waveOutDevice.Play();
            AudioFileReader reader = new AudioFileReader("Musiques/hereWegoAgain.mp3");
            WaveOut newWaveOut = new WaveOut();
            newWaveOut.Init(reader);
            newWaveOut.Play();
            resetEvent.Set();
            enPause = false;
        }
        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < NB_CASE_HAUTEUR; i++)
            {
                for (int j = 0; j < NB_CASE_LARGEUR; j++)
                {
                    builder.Append((plateau[j, i].estColore ? "1" : "0"));
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
