using System;

namespace Genetic_Algorithm_for_TSP
{
    class Program
    {
        static void Main(string[] args)
        {
            SolveSalesmanProblem("berlin52", 52);
            
        }
        static void SolveSalesmanProblem(string Path, int N)
        {
            
            Cities Berlin = new Cities(N, $"/Users/admin/Desktop/Genetic Algorithm for TSP/{Path}.txt");
            Path path = new Path(Berlin);
            
            // foreach (var city in path.path)
            // {
            //     Console.Write(city);
            //     Console.Write(" ");
            // }

            //Console.WriteLine(path.PathLength());
            path.FindPath();

            //Console.WriteLine(path.CurrentDistance);
            
            
        }
    }
    
}