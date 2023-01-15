namespace AlgorytmGenetyczny
{
    using System;
    using System.Linq;

    internal class Program
    {
        // P07 is a set of 15 weights and profits for a knapsack of capacity 750, from Kreher and Stinson, with an optimal profit of 1458.
        // https://people.sc.fsu.edu/~jburkardt/datasets/knapsack_01/knapsack_01.html
        public static Random _rand = new Random();

        public static Item[] _items =
            {
            new Item(135, 70), new Item(139, 73), new Item(149, 77),
            new Item(150, 80), new Item(156, 82), new Item(163, 87),
            new Item(173, 90), new Item(184, 94), new Item(192, 98),
            new Item(201, 106), new Item(210, 110), new Item(214, 113),
            new Item(221, 115), new Item(229, 118), new Item(240, 120),
            };

        public static int _backpackMaxWeight = 750;
        public static int _crossoverProbability = 10;
        public static int _mutateProbability = 1;
        public static int _inversionProbability = 10;

        private static void Main(string[] args)
        {
            for (int j = 0; j < 10; j++)
            {
                Console.WriteLine(_crossoverProbability + " = CO probability");
                for (int i = 0; i < 5; i++)
                {
                    Program.RunProgram();
                }

                Program._crossoverProbability += 10;
            }

            Console.ReadKey();
        }

        private static void RunProgram()
        {
            Individual[] population = Population.GenerateFirstPopulation(100);
            population = Population.SortPopulationByFitness(population);
            Individual[] newPopulation = Population.SelectFromPopulationByRankingMethod(population);
            newPopulation = Population.GenerateChildren(newPopulation);
            newPopulation = Population.MutatePopulation(newPopulation);
            newPopulation = Population.InversePopulation(newPopulation);
            newPopulation = Population.SortPopulationByFitness(newPopulation);
            int top10Avg = 0;
            int stagnantCounter = 0;
            int g = 0;
            for (g = 0; g < 100; g++)
            {
                newPopulation = Population.SelectFromPopulationByRankingMethod(newPopulation);
                newPopulation = Population.GenerateChildren(newPopulation);
                newPopulation = Population.MutatePopulation(newPopulation);
                newPopulation = Population.InversePopulation(newPopulation);
                newPopulation = Population.SortPopulationByFitness(newPopulation);
                ////Population.DisplayPopulation(newPopulation, "Population number " + (g + 2) + " sorted:");
                if (top10Avg == Algorithm.GetTop10Avg(newPopulation))
                {
                    stagnantCounter++;
                }
                else
                {
                    top10Avg = Algorithm.GetTop10Avg(newPopulation);
                }

                if (stagnantCounter > 11)
                {
                    break;
                }
            }

            Population.DisplayPopulation(newPopulation, " " + g);
        }
    }

    internal class Item
    {
        internal Item(int value, int weight)
        {
            this.Value = value;
            this.Weight = weight;
        }

        internal int Value { get; set; }

        internal int Weight { get; set; }
    }

    internal class Individual
    {
        internal Individual(int[] chromosome)
        {
            this.Chromosome = chromosome;
        }

        internal int[] Chromosome { get; set; }

        internal static Individual MutateIndividual(Individual individual)
        {
            if (Program._rand.Next(0, 100) <= Program._mutateProbability)
            {
                int mutation = Program._rand.Next(0, 15);
                individual.Chromosome[mutation] = Math.Abs(individual.Chromosome[mutation] - 1);
            }

            return individual;
        }

        internal static Individual InverseIndividual(Individual individual)
        {
            if (Program._rand.Next(0, 100) <= Program._inversionProbability)
            {
                int mutation = Program._rand.Next(0, 11);
                int temp1 = individual.Chromosome[mutation];
                int temp2 = individual.Chromosome[mutation + 1];
                int temp3 = individual.Chromosome[mutation + 2];
                int temp4 = individual.Chromosome[mutation + 3];
                individual.Chromosome[mutation] = temp4;
                individual.Chromosome[mutation + 1] = temp3;
                individual.Chromosome[mutation + 2] = temp2;
                individual.Chromosome[mutation + 3] = temp1;
            }

            return individual;
        }

        internal static Individual[] CreateChildrenSinglePoint(Individual parent1, Individual parent2)
        {
            Individual[] children = new Individual[2];
            if (Program._rand.Next(0, 100) < Program._crossoverProbability)
            {
                children[0] = new Individual(new int[]
                {
                    parent1.Chromosome[0], parent1.Chromosome[1], parent1.Chromosome[2], parent1.Chromosome[3], parent1.Chromosome[4],
                    parent1.Chromosome[5], parent1.Chromosome[6], parent1.Chromosome[7], parent2.Chromosome[8], parent2.Chromosome[9],
                    parent2.Chromosome[10], parent2.Chromosome[11], parent2.Chromosome[12], parent2.Chromosome[13], parent2.Chromosome[14]
                });
                children[1] = new Individual(new int[]
                {
                    parent2.Chromosome[0], parent2.Chromosome[1], parent2.Chromosome[2], parent2.Chromosome[3], parent2.Chromosome[4],
                    parent2.Chromosome[5], parent2.Chromosome[6], parent2.Chromosome[7], parent1.Chromosome[8], parent1.Chromosome[9],
                    parent1.Chromosome[10], parent1.Chromosome[11], parent1.Chromosome[12], parent1.Chromosome[13], parent1.Chromosome[14]
                });
            }
            else
            {
                children[0] = new Individual(parent1.Chromosome);
                children[1] = new Individual(parent2.Chromosome);
            }

            return children;
        }

        internal static Individual[] CreateChildrenTwoPoint(Individual parent1, Individual parent2)
        {
            Individual[] children = new Individual[2];
            if (Program._rand.Next(0, 100) < Program._crossoverProbability)
            {
                children[0] = new Individual(new int[]
                {
                    parent1.Chromosome[0], parent1.Chromosome[1], parent1.Chromosome[2], parent1.Chromosome[3], parent1.Chromosome[4],
                    parent2.Chromosome[5], parent2.Chromosome[6], parent2.Chromosome[7], parent2.Chromosome[8], parent2.Chromosome[9],
                    parent1.Chromosome[10], parent1.Chromosome[11], parent1.Chromosome[12], parent1.Chromosome[13], parent1.Chromosome[14]
                });
                children[1] = new Individual(new int[]
                {
                    parent2.Chromosome[0], parent2.Chromosome[1], parent2.Chromosome[2], parent2.Chromosome[3], parent2.Chromosome[4],
                    parent1.Chromosome[5], parent1.Chromosome[6], parent2.Chromosome[7], parent1.Chromosome[8], parent1.Chromosome[9],
                    parent2.Chromosome[10], parent2.Chromosome[11], parent2.Chromosome[12], parent2.Chromosome[13], parent2.Chromosome[14]
                });
            }
            else
            {
                children[0] = new Individual(parent1.Chromosome);
                children[1] = new Individual(parent2.Chromosome);
            }

            return children;
        }

        internal static Individual[] CreateChildrenRandom(Individual parent1, Individual parent2)
        {
            Individual[] children = new Individual[2];
            if (Program._rand.Next(0, 100) < Program._crossoverProbability)
            {
                children[0] = new Individual(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                children[1] = new Individual(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                for (int i = 0; i < 15; i++)
                {
                    if (Program._rand.Next(0, 2) <= 1)
                    {
                        children[0].Chromosome[i] = parent1.Chromosome[i];
                        children[1].Chromosome[i] = parent2.Chromosome[i];
                    }
                    else
                    {
                        children[0].Chromosome[i] = parent2.Chromosome[i];
                        children[1].Chromosome[i] = parent1.Chromosome[i];
                    }
                }
            }
            else
            {
                children[0] = new Individual(parent1.Chromosome);
                children[1] = new Individual(parent2.Chromosome);
            }

            return children;
        }

        internal static Individual GenerateIndividual()
        {
            int[] chromosome = new int[Program._items.Length];
            for (int i = 0; i < Program._items.Length; i++)
            {
                int gene = Program._rand.Next(0, 2);
                chromosome[i] = gene;
            }

            return new Individual(chromosome);
        }
    }

    internal class Population
    {
        internal static Individual[] SelectFromPopulationByRankingMethod(Individual[] population)
        {
            Individual[] newPopulation = new Individual[population.Length];
            for (int i = 0; i < 50; i++)
            {
                newPopulation[i] = population[i];
            }

            return newPopulation;
        }

        internal static Individual[] SortPopulationByFitness(Individual[] testPopulation)
        {
            Tuple<Individual, int>[] scoredPopulation = new Tuple<Individual, int>[testPopulation.Length];
            for (int i = 0; i < testPopulation.Length; i++)
            {
                scoredPopulation[i] = new Tuple<Individual, int>(testPopulation[i], Algorithm.FitnessFunction(testPopulation[i].Chromosome));
            }

            var result = from tuples in scoredPopulation
                         orderby tuples.Item2 descending
                         select tuples;
            Individual[] sortedPopulation = new Individual[testPopulation.Length];
            int j = 0;
            foreach (Tuple<Individual, int> tuple in result)
            {
                sortedPopulation[j++] = tuple.Item1;
            }

            return sortedPopulation;
        }

        internal static Individual[] GenerateFirstPopulation(int sizeOfPupulation)
        {
            Individual[] newPopulation = new Individual[sizeOfPupulation];
            for (int i = 0; i < sizeOfPupulation; i++)
            {
                newPopulation[i] = Individual.GenerateIndividual();
            }

            return newPopulation;
        }

        internal static Individual[] GenerateChildren(Individual[] newPopulation)
        {
            for (int i = 50; i < 100; i = i + 2)
            {
                int tempRand = Program._rand.Next(0, 3);
                /* if (tempRand <= 1)
                 {
                     Individual[] children = Individual.CreateChildrenSinglePoint(newPopulation[i - 50], newPopulation[99 - i]);
                     newPopulation[i] = children[0];
                     newPopulation[i + 1] = children[1];
                }
                 else if (tempRand >= 2)
                 {
                     Individual[] children = Individual.CreateChildrenTwoPoint(newPopulation[i - 50], newPopulation[99 - i]);
                     newPopulation[i] = children[0];
                     newPopulation[i + 1] = children[1];
                 }
                 else
                 {*/
                Individual[] children = Individual.CreateChildrenRandom(newPopulation[i - 50], newPopulation[99 - i]);
                newPopulation[i] = children[0];
                newPopulation[i + 1] = children[1];
                /*}*/
            }

            return newPopulation;
        }

        internal static void DisplayPopulation(Individual[] population, string message)
        {
            Array.ForEach(population[0].Chromosome, Console.Write);
            Console.Write(" - " + Algorithm.FitnessFunction(population[0].Chromosome) + " - " + message);
            Console.Write("\n");
        }

        internal static Individual[] MutatePopulation(Individual[] population)
        {
            for (int i = 0; i < 100; i++)
            {
                population[i] = Individual.MutateIndividual(population[i]);
            }

            return population;
        }

        internal static Individual[] InversePopulation(Individual[] population)
        {
            for (int i = 0; i < 100; i++)
            {
                population[i] = Individual.InverseIndividual(population[i]);
            }

            return population;
        }
    }

    internal class Algorithm
    {
        internal static int FitnessFunction(int[] chromosome)
        {
            int sumWeight = 0;
            int sumValue = 0;
            for (int i = 0; i < Program._items.Length; i++)
            {
                sumValue += chromosome[i] * Program._items[i].Value;
                sumWeight += chromosome[i] * Program._items[i].Weight;
            }

            if (sumWeight > Program._backpackMaxWeight)
            {
                return 0;
            }
            else
            {
                return sumValue;
            }
        }

        internal static int GetTop10Avg(Individual[] newPopulation)
        {
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += FitnessFunction(newPopulation[i].Chromosome);
            }

            return (int)(sum / 10);
        }
    }
}