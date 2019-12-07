using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("../../../input.txt");
            var test = File.ReadAllLines("../../../test.txt");
            
            FirstTask(lines);
            SecondTask(lines);
        }

        private static void FirstTask(string[] input)
        {
            var orbitalPairs = input.Select(line => line.Split(")")).ToArray();
            var planets = CreatePlanetsDictionary(orbitalPairs);
            var planetSteps = CalculateStepsFromCenter(planets);

            var counter = 0;

            foreach (var step in planetSteps)
            {
                counter += step.Value;
            }
            
            Console.WriteLine(counter);
        }

        private static void SecondTask(string[] input)
        { 
            var orbitalPairs = input.Select(line => line.Split(")")).ToArray();
            var planets = CreatePlanetsWithParentDictionary(orbitalPairs);

            var you = "YOU";
            var santa = "SAN";

            var pathToYou = FindPathTo(you, planets);
            var pathToSanta = FindPathTo(santa, planets);

            var step = 0;

            foreach (var segment in pathToYou)
            {
                if (pathToSanta[step] == segment)
                {
                    step++;
                    continue;
                }

                step--; // последний совпавший
                break;
            }

            var diffToYou = pathToYou.Count - step;
            var diffToSanta = pathToSanta.Count - step;
            
            
            Console.WriteLine(pathToYou.Count);
            Console.WriteLine(pathToSanta.Count);
            Console.WriteLine(diffToYou + diffToSanta - 4);

        }

        private static Dictionary<string, List<string>> CreatePlanetsDictionary(string[][] orbitalPairs)
        {
            var planets = new Dictionary<string, List<string>>();

            foreach (var pair in orbitalPairs)
            {
                var parent = pair[0];
                var child = pair[1];
                if (planets.ContainsKey(parent))
                    planets[parent].Add(child);
                else
                    planets.Add(parent, new List<string>{child});
                
                if (!planets.ContainsKey(child))
                    planets.Add(child, new List<string>());

            }

            return planets;
        }
        
        private static Dictionary<string, Dictionary<string, List<string>>> CreatePlanetsWithParentDictionary(string[][] orbitalPairs)
        {
            var planets = new Dictionary<string, Dictionary<string, List<string>>>();

            foreach (var pair in orbitalPairs)
            {
                var parent = pair[0];
                var child = pair[1];
                if (planets.ContainsKey(parent))
                    planets[parent]["children"].Add(child);
                else
                {
                    planets[parent] = new Dictionary<string, List<string>>();
                    planets[parent]["children"] = new List<string> {child};
                }
                
                if (!planets.ContainsKey(child))
                {
                    planets[child] = new Dictionary<string, List<string>>();
                    planets[child]["children"] = new List<string>();
                }
                planets[child]["parent"] = new List<string> {parent};
            }

            return planets;
        }
        
        private static Dictionary<string, int> CalculateStepsFromCenter(Dictionary<string, List<string>> planets)
        {
            var center = "COM";
            var planetSteps = new Dictionary<string, int>();
            planetSteps[center] = 0;

            OneChildrenLevel(planets, planetSteps, center);

            return planetSteps;
        }

        private static void OneChildrenLevel(Dictionary<string, List<string>> planets, 
            Dictionary<string, int> planetSteps, string parent)
        {
            var parentSteps = planetSteps[parent];
            var children = planets[parent];

            foreach (var child in children)
            {
                planetSteps[child] = parentSteps + 1;
                OneChildrenLevel(planets, planetSteps, child);
            }
        }

        private static List<string> FindPathTo(string target,
            Dictionary<string, Dictionary<string, List<string>>> planets)
        {
            var path = new List<string>{target};
            var currentPoint = target;

            while (currentPoint != "COM")
            {
                var parent = planets[currentPoint]["parent"][0];
                path.Add(parent);
                currentPoint = parent;
            }

            path.Reverse();
            
            return path;
        }
    }
}