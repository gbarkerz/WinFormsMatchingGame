using System;
using System.Collections.Generic;

// Ths class copied from https://github.com/guybark/Sa11ytaire.
namespace WinFormsMatchingGame
{
    public class Shuffler
    {
        private readonly Random random;

        public Shuffler()
        {
            this.random = new Random();
        }

        public void Shuffle<T>(IList<T> array)
        {
            for (int i = array.Count; i > 1;)
            {
                int j = this.random.Next(i);

                --i;

                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}
