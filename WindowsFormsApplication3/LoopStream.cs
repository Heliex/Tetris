using NAudio.Wave;


namespace WindowsFormsApplication3
{
    /**
    *   Classe LoopStream
    *   Permet de lancer un son en boucle
    *   Dernière modification : 22/10/2015 par Christophe GERARD
    **/
    class LoopStream : WaveStream
    {
        public bool enableLooping { get; set; } // Booléen qui dit si on loop ou pas
        WaveStream sourceStream; // Source
        public LoopStream(WaveStream sourceStream) // COnstructeur
        {
            this.sourceStream = sourceStream;
            this.enableLooping = true;
        }

        public override WaveFormat WaveFormat // Renvoie le format du son lu
        {
            get
            {
                return sourceStream.WaveFormat;
            }
        }
        
        public override long Length // Renvoie la taille du son lu
        {
            get
            {
                return sourceStream.Length;
            }
        }

        public override long Position // Renvoie la position actuelle dans le son lu
        {
            get
            {
                return sourceStream.Position;
            }

            set
            {
                sourceStream.Position = value;
            }
        }

        // Cette méthode permet de savoir quand relancer le son
        public override int Read(byte[] buffer, int offset, int count) // Réécriture de la méthode lire
        {
            int totalBytesRead = 0;
            while(totalBytesRead < count) // Si le total lu est inférieur à count
            {
                int bytesRead = sourceStream.Read(buffer,offset+totalBytesRead,count - totalBytesRead); // Alors on appel la méthode de façon récursive.
                if(bytesRead == 0) // Condition d'arrêt - Si on n'a plus rien à lire
                {
                    if(sourceStream.Position == 0 || !enableLooping) // Si on est au départ ou qu'on loop pas encore
                    {
                        break; // Alors on break;
                    }
                    sourceStream.Position = 0; // Sinon on set la position à 0
                }
                totalBytesRead += bytesRead; // On ajoute au total les bytes lu
            }
            return totalBytesRead;
        }
    }
}
