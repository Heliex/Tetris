﻿using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using NAudio.Wave;
using System.Text;

namespace WindowsFormsApplication3
{
    public class Jeu
    {
        public static readonly int NB_CASE_HAUTEUR = 16;
        public static readonly int NB_CASE_LARGEUR = 16;
        public static readonly int TailleCase = 32;
        public ArrayList pieces = new ArrayList();
        public static Case[,] plateau;
        public Piece pieceCourante { get; set; }
        public volatile bool estPerdu;
        public IWavePlayer waveOutDevice;
        public AudioFileReader audioFileReader;
        public Random randNum;
        public event EventHandler<RafraichirGUIEvent> RaiseCustomEvent;
        public ManualResetEvent resetEvent;


        public Jeu()
        {
            resetEvent = new ManualResetEvent(true);
            randNum = new Random();
            plateau = new Case[NB_CASE_HAUTEUR, NB_CASE_LARGEUR];
            estPerdu = false;
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader("C:\\Users\\Christophe\\Music\\Tetris.mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
            for (int i = 0; i < NB_CASE_HAUTEUR; i++)
            {
                for (int j = 0; j < NB_CASE_LARGEUR; j++)
                {
                    plateau[j, i] = new Case(j, i);
                }
            }
            pieceCourante = pieceSuivante();
        }

        protected virtual void OnRaiseCustomEvent(RafraichirGUIEvent e)
        {
            EventHandler<RafraichirGUIEvent> handler = RaiseCustomEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void lancerJeu()
        {
            while (!estPerdu)
            {
                resetEvent.WaitOne();
                Thread.Sleep(300);
                OnRaiseCustomEvent(new RafraichirGUIEvent()); // Rafraichissement du GUI
                if (pieceCourante.PeuxDescendre()) // Si la piece peux descendre
                {
                    pieceCourante.descendre();
                    for (int i = 0; i < NB_CASE_HAUTEUR; i++) // Parcours de chaque ligne
                    {
                        if (ligneEstComplete(plateau, i)) // Si elle est complete
                        {
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
                else // Sinon
                {
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
            if(indice > 0)
            {
                for(int i = indice; i > 0; i--)
                {
                    for (int j = 0; j < NB_CASE_LARGEUR; j++)
                    {
                      plateau[j, i].ClonerCase(plateau[j,i-1]);
                    }
                }
            }
        }

        public void finirJeu()
        {
            estPerdu = true;
        }

        public Piece pieceSuivante()
        {
            // Nouvelle piece
            Piece piece = null;
            int numPiece = randNum.Next(1,6);
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
       }

        public void resume()
        {
            waveOutDevice.Play();
            AudioFileReader reader = new AudioFileReader("C:\\Users\\Christophe\\Music\\hereWegoAgain.mp3");
            WaveOut newWaveOut = new WaveOut();
            newWaveOut.Init(reader);
            newWaveOut.Play();
            resetEvent.Set();
        }
        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();

            for(int i = 0; i < NB_CASE_HAUTEUR; i++)
            {
                for(int j = 0; j < NB_CASE_LARGEUR; j++)
                {
                    builder.Append((plateau[j, i].estColore ? "1" : "0"));
                }
                builder.AppendLine();
            }
            builder.AppendLine();
            return builder.ToString();
        }
    }
}