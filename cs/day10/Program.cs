using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("../../../input.txt").Select(line => line.Trim()).ToArray();
            var test = File.ReadAllLines("../../../test.txt").Select(line => line.Trim()).ToArray();

            FirstTask(lines);
            SecondTask(lines);
        }

        private static void FirstTask(string[] input)
        {
            var asteroids = GetAllAsteroids(input);

            var bestAsteroid = FindBestAsteroid(asteroids);
            Console.WriteLine($"{bestAsteroid.X}, {bestAsteroid.Y}");
        }

        private static void SecondTask(string[] input)
        {
            var asteroids = GetAllAsteroids(input);
            var bestAsteroid = FindBestAsteroid(asteroids);
            var asteroidsByDirections = FindAsteroidsInVisibleDirections(asteroids, bestAsteroid);

            var directions = asteroidsByDirections.Keys.ToArray();
            var angles = CalcAngles(directions);

            var sortedDirections = asteroidsByDirections.OrderBy(i => angles[i.Key]).ToArray();

            var element = sortedDirections[199]
                .Value
                .OrderBy(i => Math.Abs(i.X - bestAsteroid.X) + Math.Abs(i.Y - bestAsteroid.Y))
                .ToList()[0];
            Console.WriteLine($"{element.X}, {element.Y}");
        }

        private static List<Vector> GetAllAsteroids(string[] input)
        {
            var asteroids = new List<Vector>();
            for (int line = 0; line < input.Length; line++)
            {
                for (int i = 0; i < input[line].Length; i++)
                {
                    if (input[line][i] == '#')
                        asteroids.Add(new Vector{X = i, Y = line});
                }
            }

            return asteroids;
        }

        private static Vector FindBestAsteroid(List<Vector> asteroids)
        {
            var maxVisible = 0;
            var bestAsteroid = asteroids[0];
            foreach (var asteroid in asteroids)
            {
                var visibleDirections = FindAllVisibleDirections(asteroids, asteroid);

                if (visibleDirections.Count > maxVisible)
                {
                    maxVisible = visibleDirections.Count;
                    bestAsteroid = asteroid;
                }
            }
            
            Console.WriteLine(maxVisible);

            return bestAsteroid;
        }

        private static HashSet<string> FindAllVisibleDirections(List<Vector> asteroids, Vector center)
        {
            var visibleDirections = new HashSet<string>();

            foreach (var otherAsteroid in asteroids)
            {
                if (center == otherAsteroid) continue;

                var dx = otherAsteroid.X - center.X;
                var dy = otherAsteroid.Y - center.Y;
                var gcd = GreatestCommonDivisor(Math.Abs(dx), Math.Abs(dy));

                visibleDirections.Add($"{dx / gcd}_{dy / gcd}");
            }

            return visibleDirections;
        }
        
        private static Dictionary<string, List<Vector>> FindAsteroidsInVisibleDirections(List<Vector> asteroids, Vector center)
        {
            var visibleDirections = new Dictionary<string, List<Vector>>();
            
            foreach (var otherAsteroid in asteroids)
            {
                if (center == otherAsteroid) continue;

                var dx = otherAsteroid.X - center.X;
                var dy = otherAsteroid.Y - center.Y;
                var gcd = GreatestCommonDivisor(Math.Abs(dx), Math.Abs(dy));

                var direction = $"{dx / gcd}_{dy / gcd}";

                if (!visibleDirections.ContainsKey(direction))
                    visibleDirections[direction] = new List<Vector>{otherAsteroid};
                else
                    visibleDirections[direction].Add(otherAsteroid);
            }

            return visibleDirections;
        }

        
        private static Dictionary<string, double> CalcAngles(string[] directions)
        {
            var asteroidAngles = new Dictionary<string, double>();
            foreach (var direction in directions)
            {
                var parts = direction.Split("_").Select(int.Parse).ToArray();
                var dx = parts[0];
                var dy = parts[1];
                
                var distance = Math.Sqrt(dx * dx + dy * dy);
                var cosA = -dy / distance;
                var angle = Math.Acos(cosA);

                if (dx < 0)
                    angle = Math.Acos(-cosA) + Math.PI;

                asteroidAngles[direction] = angle;
            }

            return asteroidAngles;
        } 
        
        private static int GreatestCommonDivisor(int a, int b)
        {
            if (a == 0) return b;
            if (b == 0) return a;

            if (a >= b)
                return GreatestCommonDivisor(a % b, b);
            return GreatestCommonDivisor(a, b % a);
        }
    }

    class Vector
    {
        public int X;
        public int Y;
    }
}