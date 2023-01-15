using System;

namespace AlgorytmGenetyczny3
{
    internal class Graph
    {
        /// <summary>
        /// Zmienna do tworzenia liczb losowych.
        /// </summary>
        private Random rand;

        /// <summary>
        /// Tablica z poziomami feromonów na każdym wierzchołku.
        /// </summary>
        private float[][] globalPheromoneLevels;

        /// <summary>
        /// Wygenerowane ścieżki.
        /// </summary>
        private Path[] generatedPaths;

        /// <summary>
        /// Liczba przedmiotów w plecaku.
        /// </summary>
        private int numberOfItems = 15;

        /// <summary>
        /// Liczba ścieżek/osobników w populacji = mrówek.
        /// </summary>
        private int numberOfAnts;

        /// <summary>
        /// Poziom wyparowywania feromonów.
        /// </summary>
        private float pheromoneEvaporationRate;

        /// <summary>
        /// Konstruktor grafu.
        /// </summary>
        /// <param name="numberOfPaths">Liczba ścieżek do wygenerowania</param>
        /// <param name="pheromoneEvaporationRate">Poziom wyparowywania feromonów.</param>
        internal Graph(int numberOfPaths, float pheromoneEvaporationRate)
        {
            this.numberOfAnts = numberOfPaths;
            this.pheromoneEvaporationRate = pheromoneEvaporationRate;
            this.rand = new Random();

            this.globalPheromoneLevels = new float[this.numberOfItems][];
            for (int i = 0; i < this.numberOfItems; i++)
            {
                /// 2, bo do wyboru mamy tylko 1 lub 0, dwie opcje.
                this.globalPheromoneLevels[i] = new float[2];
            }
            this.generatedPaths = new Path[this.numberOfAnts];
        }

        /// <summary>
        /// Losowe generowanie feromonów na start.
        /// </summary>
        internal void RandomPheromoneGeneration()
        {
            for (int i = 0; i < this.numberOfItems; i++)
            {
                for (int b = 0; b < 2; b++)
                {/// 2, bo do wyboru mamy tylko 1 lub 0, dwie opcje.
                    this.globalPheromoneLevels[i][b] = (float)rand.NextDouble();
                }
            }
        }

        /// <summary>
        /// Generowanie ścieżek, którymi pójdą mrówki.
        /// </summary>
        internal void GeneratePathsForAnts()
        {
            this.generatedPaths = new Path[this.numberOfAnts];

            for (int ant = 0; ant < this.numberOfAnts; ant++)
            {
                this.generatedPaths[ant] = GeneratePath();
            }
        }

        /// <summary>
        /// Generowanie ścieżki.
        /// </summary>
        /// <returns>Wygenerowana ścieżka.</returns>
        internal Path GeneratePath()
        {
            Path path = new Path();

            for (int i = 0; i < this.numberOfItems; i++)
            {
                path.AddItem(SelectPath(i));
            }
            return path;
        }

        /// <summary>
        /// Mrówka wybiera czy idzie po przedmiot czy nie, 1 czy 0.
        /// </summary>
        /// <param name="itemNumber">Numer przedmiotu o którym decyduje mrówka.</param>
        /// <returns>1 lub 0</returns>
        internal int SelectPath(int itemNumber)
        {
            float[] pheromoneLevels = globalPheromoneLevels[itemNumber];

            double pheromoneSum = 0.0;
            foreach (float f in pheromoneLevels)
            {/// Obliczanie sumy feromonów.
                pheromoneSum += f;
            }

            /// Losujemy wartość w zakresie 0 -> suma feromonów.
            double random = rand.NextDouble() * pheromoneSum;

            /// Wybór ścieżki według prawdopodobieństwa.
            if (random <= pheromoneLevels[0])
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Nakładanie nowych feromonów na ścieżkę.
        /// </summary>
        internal void AddNewPheromonesToPath()
        {
            for (int i = 0; i < this.numberOfAnts; i++)
            { /// Dla każdej mrówki/ściezki:
                Path currentPath = this.generatedPaths[i];

                /// Obliczanie funkcji przystosowania dla obecnej ścieżki.
                int fitness = currentPath.FitnessFunction();

                /// Pobranie rozwiązania z obecnej ścieżki.
                int[] knapsackFromPath = currentPath.GetKnapsack();

                for (int path = 0; path < this.numberOfItems; path++)
                { /// Dla każdego przedmiotu na ścieżce:
                    int currentItem = knapsackFromPath[path];
                    globalPheromoneLevels[path][currentItem] += fitness;
                }
            }
        }

        /// <summary>
        /// Zabieranie starych feromonów ze ścieżki.
        /// </summary>
        internal void EvaporatePheromoneForLinks()
        {
            for (int i = 0; i < this.numberOfItems; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    globalPheromoneLevels[i][j] *= this.pheromoneEvaporationRate;
                }
            }
        }

        /// <summary>
        /// Wypisanie wyników.
        /// </summary>
        internal void PrintFinalResult()
        {
            int averageSolutionFitness = 0;
            int min = int.MaxValue;
            int max = int.MinValue;

            for (int path = 0; path < this.numberOfAnts; path++)
            {
                Path currentPath = this.generatedPaths[path];
                int fitness = currentPath.FitnessFunction();
                averageSolutionFitness += fitness;

                if (fitness > max)
                {
                    max = fitness;
                }

                if (fitness < min)
                {
                    min = fitness;
                }
            }

            averageSolutionFitness /= this.numberOfAnts;

            Console.WriteLine("max: {0}, min: {1}, avg: {2}", max, min, averageSolutionFitness);
        }
    }
}