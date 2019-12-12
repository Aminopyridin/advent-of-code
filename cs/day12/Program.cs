using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("../../../input.txt").Select(line => line.Trim()).ToArray();
            var test = File.ReadAllLines("../../../test.txt").Select(line => line.Trim()).ToArray();

            var stepsAmount = 1000;
            
           // FirstTask(input, stepsAmount);
            SecondTask(input);
        }

        private static void FirstTask(string[] rawMoons, int stepsToSimulate)
        {
            var moons = GetMoonsCoords(rawMoons);
            var velocities = CreateStartVelocities(rawMoons.Length);

            for (int i = 0; i < stepsToSimulate; i++)
            {
                CalcNewVelocities(moons, velocities);
                AppendVelocities(moons, velocities);
            }

            var energy = CalcSystemEnergy(moons, velocities);
            
            Console.WriteLine(energy);
        }

        private static void SecondTask(string[] rawMoons)
        {
            var moons = GetMoonsCoords(rawMoons);
            var velocities = CreateStartVelocities(rawMoons.Length);


            var xs = moons.Select(i => i.X).ToArray();
            var ys = moons.Select(i => i.Y).ToArray();
            var zs = moons.Select(i => i.Z).ToArray();

            var xSteps = FindPeriod(moons, velocities, () => velocities.All(i => i.X == 0) &&
                                                             Compare(xs, moons.Select(i => i.X).ToArray()));
           
            moons = GetMoonsCoords(rawMoons);
            velocities = CreateStartVelocities(rawMoons.Length);
            var ySteps = FindPeriod(moons, velocities, () => velocities.All(i => i.Y == 0) && 
                                                             Compare(ys, moons.Select(i => i.Y).ToArray()));
            
            moons = GetMoonsCoords(rawMoons);
            velocities = CreateStartVelocities(rawMoons.Length);
            var zSteps = FindPeriod(moons, velocities, () => velocities.All(i => i.Z == 0) && 
                                                             Compare(zs, moons.Select(i => i.Z).ToArray()));

            var gcdXY = GreatestCommonDivisor(xSteps, ySteps);
            var xy = (BigInteger)xSteps * ySteps / gcdXY;
            var gcd = GreatestCommonDivisor(xy, zSteps);
            var res = xy * zSteps / gcd;

            Console.WriteLine(res);
            

        }
        
        private static int GreatestCommonDivisor(int a, int b)
        {
            if (a == 0) return b;
            if (b == 0) return a;

            if (a >= b)
                return GreatestCommonDivisor(a % b, b);
            return GreatestCommonDivisor(a, b % a);
        }
        
        private static BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            if (a == 0) return b;
            if (b == 0) return a;

            if (a >= b)
                return GreatestCommonDivisor(a % b, b);
            return GreatestCommonDivisor(a, b % a);
        }


        private static int FindPeriod(List<Vector> moons, List<Vector> velocities, Func<bool> compare)
        {

            var step = 0;
            while (step == 0 || !compare())
            {
                ++step;
                CalcNewVelocities(moons, velocities);
                AppendVelocities(moons, velocities);
            }
            
            Console.WriteLine(step);
            return step;
        }
       
        private static bool Compare(int[] start, int[] current)
        {
            return start.Where((el, i) => current[i] == el).ToList().Count == start.Length;
        }

        private static bool CheckVelocities(List<Vector> velocities)
        {
            foreach (var velocity in velocities)
            {
                if (velocity.X != 0 || velocity.Y != 0 || velocity.Z != 0)
                    return false;
            }

            return true;
        }

        private static List<Vector> GetMoonsCoords(string[] rawMoons)
        {
            var regexp = new Regex(@"x=(-?\d+).*y=(-?\d+).*z=(-?\d+)");
            var matches = rawMoons.Select(i => regexp.Match(i));

            var moons = new List<Vector>();
            
            foreach (var match in matches)
            {
                var moon = new Vector
                {
                    X = int.Parse(match.Groups[1].Value),
                    Y = int.Parse(match.Groups[2].Value),
                    Z = int.Parse(match.Groups[3].Value),
                };
                
                moons.Add(moon);
            }

            return moons;
        }

        private static List<Vector> CreateStartVelocities(int count)
        {
            var velocities = new List<Vector>();

            for (int i = 0; i < count; i++)
            {
                velocities.Add(new Vector {X = 0, Y = 0, Z = 0});
            }

            return velocities;
        }

        private static void CalcNewVelocities(List<Vector> moons, List<Vector> velocities)
        {
            for (int i = 0; i < moons.Count; i++)
            {
                for (int j = i + 1; j < moons.Count; j++)
                {
                    var moon1 = moons[i];
                    var moon2 = moons[j];
                    var vel1 = velocities[i];
                    var vel2 = velocities[j];

                    var dx = Math.Sign(moon1.X - moon2.X);
                    var dy = Math.Sign(moon1.Y - moon2.Y);
                    var dz = Math.Sign(moon1.Z - moon2.Z);

                    vel1.X -= dx;
                    vel2.X += dx;
                    vel1.Y -= dy;
                    vel2.Y += dy;
                    vel1.Z -= dz;
                    vel2.Z += dz;
                }
            }
        }

        private static void AppendVelocities(List<Vector> moons, List<Vector> velocities)
        {
            for (int i = 0; i < moons.Count; i++)
            {
                var moon = moons[i];
                var vel = velocities[i];

                moon.X += vel.X;
                moon.Y += vel.Y;
                moon.Z += vel.Z;
            }
        }

        private static int CalcSystemEnergy(List<Vector> moons, List<Vector> velocities)
        {
            var sum = 0;
            for (int i = 0; i < moons.Count; i++)
            {
                var pot = CalcEnergy(moons[i]);
                var kin = CalcEnergy(velocities[i]);
                sum += pot * kin;
            }

            return sum;
        }

        private static int CalcEnergy(Vector v)
        {
            return Math.Abs(v.X) + Math.Abs(v.Y) + Math.Abs(v.Z);
        }
        
    }

    class Vector
    {
        public int X;
        public int Y;
        public int Z;
    }
}