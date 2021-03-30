using System;
using System.Drawing;
using System.IO;

namespace Genetic_Algorithm_for_TSP
{
    public class Cities
    {
        //массив коррдинат городов
        public Point[] Coordinate;
        

        public Cities(int N, string Path)
        {
            int i = 0;
            Coordinate = new Point[N];
            using (StreamReader sr = new StreamReader(Path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] point = line.Split(" ");
                    Coordinate[i].X = (int)double.Parse(point[0]);
                    Coordinate[i].Y = (int)double.Parse(point[1]);
                    i++;
                }
            }
        }
    }
}