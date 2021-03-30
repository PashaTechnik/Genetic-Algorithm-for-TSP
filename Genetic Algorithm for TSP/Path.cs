using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetic_Algorithm_for_TSP
{
    public class Path
    {
        
        double[,] distance;

        public int[] path;

        public double CurrentDistance;
        

        public Path(Cities map)
        {

            distance = new double[map.Coordinate.Length, map.Coordinate.Length];

            for (int j = 0; j < map.Coordinate.Length; j++)
            {
                distance[j, j] = 0;

                for (int i = 0; i < map.Coordinate.Length; i++)
                {
                    double value = Math.Sqrt(Math.Pow(map.Coordinate[i].X - map.Coordinate[j].X, 2) +
                                             Math.Pow(map.Coordinate[i].Y - map.Coordinate[j].Y, 2));
                    distance[i, j] = distance[j, i] = value;
                }
            }

            
            path = new int[map.Coordinate.Length + 1];
            for (int i = 0; i < map.Coordinate.Length; i++)
            {
                path[i] = i;
            }
            path[map.Coordinate.Length] = 0;
        }

        public void FindPath()
        {
            Random rnd = new Random();
            List<int[]> population = new List<int[]>();
            
            int[] basicGeneration = path.Where(val => val != 0).ToArray();

            for (int i = 0; i < 10; i++)
            {
                int[] ShuffleArray = basicGeneration.OrderBy(x => rnd.Next()).ToArray(); 
                population.Add(ShuffleArray);
            }

            for (int l = 0; l < 50000; l++)
            {

                population = GetNewPopulation(population);
                population = Mutation(population);
                population = population.OrderBy(x => CalcPathLength(x)).ToList();
                CurrentDistance = CalcPathLength(population.First());
                population = population.Take(population.Count / 2).ToList();
                Console.WriteLine(CurrentDistance);
            }

        }

        public List<int[]> GetNewPopulation(List<int[]> population)
        {
            List<int[]> newPopulation = new List<int[]>(population);
            int alpha = 3;
            Random rnd = new Random();

            while (population.Count != 0)
            {
                
                int index = rnd.Next(population.Count);
                var parent1 = population[index];
                population.Remove(parent1);
                index = rnd.Next(population.Count);
                var parent2 = population[index];
                population.Remove(parent2);

                var child1 = parent1.Take(alpha).ToList();
                foreach (var i in parent2)
                {
                    if (!child1.Contains(i))
                    {
                        child1.Add(i);
                    }
                }

                var child2 = parent2.Take(alpha).ToList();
                foreach (var i in parent1)
                {
                    if (!child2.Contains(i))
                    {
                        child2.Add(i);
                    }
                }

                newPopulation.Add(child1.ToArray());
                newPopulation.Add(child2.ToArray());
            }

            return newPopulation;

        }


        public List<int[]> Mutation(List<int[]> population)
        {
            Random rnd = new Random();

            foreach (var child in population)
            {
                var prob = rnd.Next(1,100);

                if (prob <= 5)
                {
                    ReverseChromosome(child);
                }
            }

            return population;
        }

        public int[] ReverseChromosome(int[] chromosome)
        {
            Random rnd = new Random();
            int start = rnd.Next(chromosome.Length);
            int end = rnd.Next(chromosome.Length);
            int[] subArr;
            if (start < end)
            {
                subArr = chromosome.Skip(start).Take(end - start).ToArray();
            }
            else
            {
                subArr = chromosome.Skip(end).Take(start - end).ToArray();
            }

            subArr = subArr.Reverse().ToArray();
            
            int k = 0;
            for (int i = start<end?start:end; i < (start<end?end:start); i++)
            {
                chromosome[i] = subArr[k];
                k++;
            }

            return chromosome;
        }

        public double PathLength()
        {
            double pathSum = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                pathSum += distance[path[i], path[i + 1]];
            }
            return pathSum;
        }

        public double CalcPathLength(int[] path)
        {
            

            var k = path.ToList();
            k.Insert(0,0);
            k.Add(0);
            path = k.ToArray();
            
            double pathSum = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                pathSum += distance[path[i], path[i + 1]];
            }
            return pathSum;
        }
        
    }
    
    
    
}
