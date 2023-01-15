namespace AlgorytmGenetyczny3
{
    internal class Path
    {
        /// <summary>
        /// Gdzie znajduje się mrówka na ścieżce.
        /// </summary>
        private int currentIndex = 0;

        /// <summary>
        /// Plecak.
        /// </summary>
        private int[] knapsack;

        private int[] knapsackValues = { 135, 139, 149, 150, 156, 163, 173, 184, 192, 201, 210, 214, 221, 229, 240 };
        private int[] knapsackWeights = { 70, 73, 77, 80, 82, 87, 90, 94, 98, 106, 110, 113, 115, 118, 120 };

        /// <summary>
        /// Ilość przedmiotów w problemie.
        /// </summary>
        private int numberOfItems = 15;

        /// <summary>
        /// Konstruktor ścieżki.
        /// </summary>
        internal Path()
        {
            knapsack = new int[numberOfItems];
        }

        /// <summary>
        /// Dodawanie przedmiotu do plecaka.
        /// </summary>
        /// <param name="item">1 lub 0 w zależności czy wkładamy przedmiot, czy nie.</param>
        internal void AddItem(int item)
        {
            knapsack[currentIndex] = item;
            currentIndex++;
        }

        /// <summary>
        /// Funkcja przystosowania.
        /// </summary>
        /// <returns></returns>
        internal int FitnessFunction()
        {
            int fitnessScore = 0;
            int weight = 0;
            for (int i = 0; i < 15; i++)
            {
                fitnessScore += knapsack[i] * knapsackValues[i];
                weight += knapsack[i] * knapsackWeights[i];
            }
            if (weight > 750)
            {
                fitnessScore = 0;
            }
            return fitnessScore;
        }

        /// <summary>
        /// Zwracanie ścieżki po której przeszła mrówka.
        /// </summary>
        /// <returns></returns>
        internal int[] GetKnapsack()
        {
            return this.knapsack;
        }
    }
}