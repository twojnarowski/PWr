using System;
using System.Diagnostics;
using System.IO;

namespace AlgorytmGenetyczny3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /// Zapisywanie wyników do pliku.
            FileStream outputStream;
            StreamWriter streamWriter;
            TextWriter consoleStream = Console.Out;
            try
            {
                outputStream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//output.txt", FileMode.OpenOrCreate, FileAccess.Write);
                streamWriter = new StreamWriter(outputStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(streamWriter);

            /// Początek liczenia czasu.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            /// Zmienne którymi można modyfikować algorytm
            /// Populacja mrówek.
            int numberOfAnts = 1000;
            /// Poziom wyparowywania feromonów - im bliżej 0, tym więcej feromonów wyparowuje
            float pheromoneEvaporationRate = 1.0f;

            Calculate5Populations(numberOfAnts, pheromoneEvaporationRate);

            /// Koniec liczenia czasu.
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            /// Koniec przechwytywania danych z konsoli do pliku.
            Console.SetOut(consoleStream);
            streamWriter.Close();
            outputStream.Close();
            Console.WriteLine("Done");
        }

        /// <summary>
        /// Uruchomienie algorytmu 5 razy.
        /// </summary>
        /// <param name="numberofAnts">Liczba mrówek/ścieżek.</param>
        /// <param name="pheromoneEvaporationRate">Poziom wyaprowywania feromonów.</param>
        private static void Calculate5Populations(int numberofAnts, float pheromoneEvaporationRate)
        {
            int numberOfPopulations = 5;
            int numberOfEvaluations = 10000 / numberofAnts;

            /// Wypisanie danych przebiegu do konsoli.
            Console.WriteLine("Liczba mrówek:" + numberofAnts + " Wyparowywanie:" + pheromoneEvaporationRate);

            /// Pętla dla każdego przebiegu.
            for (int trial = 0; trial < numberOfPopulations; trial++)
            {
                /// Stworzenie nowego grafu.
                Graph graph = new Graph(numberofAnts, pheromoneEvaporationRate);

                /// Losowe wygenerowanie feromonów na start.
                graph.RandomPheromoneGeneration();

                /// Symulacja przejścia mrówek.
                for (int i = 0; i < numberOfEvaluations; i++)
                {
                    /// Generowanie ścieżek.
                    graph.GeneratePathsForAnts();
                    /// Dodanie feromonów do ścieżek.
                    graph.AddNewPheromonesToPath();
                    /// Wyparowanie starych feromonów.
                    graph.EvaporatePheromoneForLinks();
                }
                graph.PrintFinalResult();
            }
        }
    }
}