using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace day11
{
    enum Rotate
    {
        Left,
        Right,
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = "3,8,1005,8,330,1106,0,11,0,0,0,104,1,104,0,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,1001,8,0,28,1,1103,17,10,1006,0,99,1006,0,91,1,102,7,10,3,8,1002,8,-1,10,101,1,10,10,4,10,108,1,8,10,4,10,1002,8,1,64,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,102,1,8,86,2,4,0,10,1006,0,62,2,1106,13,10,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,0,10,4,10,101,0,8,120,1,1109,1,10,1,105,5,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,1002,8,1,149,1,108,7,10,1006,0,40,1,6,0,10,2,8,9,10,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1002,8,1,187,1,1105,10,10,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1002,8,1,213,1006,0,65,1006,0,89,1,1003,14,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,102,1,8,244,2,1106,14,10,1006,0,13,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,1001,8,0,273,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,1,8,10,4,10,1001,8,0,295,1,104,4,10,2,108,20,10,1006,0,94,1006,0,9,101,1,9,9,1007,9,998,10,1005,10,15,99,109,652,104,0,104,1,21102,937268450196,1,1,21102,1,347,0,1106,0,451,21101,387512636308,0,1,21102,358,1,0,1105,1,451,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21101,0,97751428099,1,21102,1,405,0,1105,1,451,21102,1,179355806811,1,21101,416,0,0,1106,0,451,3,10,104,0,104,0,3,10,104,0,104,0,21102,1,868389643008,1,21102,439,1,0,1105,1,451,21102,1,709475853160,1,21102,450,1,0,1105,1,451,99,109,2,22102,1,-1,1,21101,0,40,2,21101,482,0,3,21102,1,472,0,1105,1,515,109,-2,2106,0,0,0,1,0,0,1,109,2,3,10,204,-1,1001,477,478,493,4,0,1001,477,1,477,108,4,477,10,1006,10,509,1101,0,0,477,109,-2,2105,1,0,0,109,4,2101,0,-1,514,1207,-3,0,10,1006,10,532,21101,0,0,-3,21202,-3,1,1,22101,0,-2,2,21101,1,0,3,21101,0,551,0,1105,1,556,109,-4,2106,0,0,109,5,1207,-3,1,10,1006,10,579,2207,-4,-2,10,1006,10,579,22102,1,-4,-4,1105,1,647,21201,-4,0,1,21201,-3,-1,2,21202,-2,2,3,21101,0,598,0,1106,0,556,22101,0,1,-4,21102,1,1,-1,2207,-4,-2,10,1006,10,617,21101,0,0,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,639,22102,1,-1,1,21102,1,639,0,105,1,514,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2105,1,0";
            
            FirstTask(input);
            SecondTask(input);
        }

        private static void FirstTask(string input)
        {
            var instructions = input.Split(",").Select(BigInteger.Parse).ToList();

            var colors = GetColoredPanels(instructions, 0);
            
            Console.WriteLine(colors.Count);

        }

        private static void SecondTask(string input)
        {
            var instructions = input.Split(",").Select(BigInteger.Parse).ToList();

            var colors = GetColoredPanels(instructions, 1);

            var rows = colors.GroupBy(i => int.Parse(i.Key.Split('_')[1])).OrderBy(i => i.Key).ToList();

            for (int y = 0; y < rows.Count; y++)
            {
                for (int x = 0; x < 45; x++)
                {
                    var positionStr = $"{x}_{y}";

                    if (colors.ContainsKey(positionStr) && colors[positionStr] == 1)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        private static Dictionary<string, int> GetColoredPanels(List<BigInteger> instructions, int startColor)
        {
            var colors = new Dictionary<string, int>();
            var directions = new[] {new[] {0, -1}, new[] {1, 0}, new[] {0, 1}, new[] {-1, 0}};
            var direction = 0;
            var position = new Vector{ X = 0, Y = 0 };
            var positionStr = $"{position.X}_{position.Y}";
            colors[positionStr] = startColor;
            var index = 0;
            var relativeBase = 0;
            
            while (true)
            {
                var res = IntCodeComputer.Run(instructions, colors[positionStr], index, relativeBase);

                if (!res.Item1.HasValue)
                {
                    break;
                }

                index = res.Item1.Value;
                relativeBase = res.Item2;

                var outputs = res.Item3;
                colors[positionStr] = outputs[0];

                direction += outputs[1] == 0 ? -1 : 1;
                direction = direction % directions.Length;

                if (direction < 0)
                {
                    direction = directions.Length - 1;
                }
                
                position.X += directions[direction][0];
                position.Y += directions[direction][1];
                positionStr = $"{position.X}_{position.Y}";
                
                if (!colors.ContainsKey(positionStr))
                    colors[positionStr] = 0;
            }
            
            return colors;
        }
    }
    
    class IntCodeComputer
    {
        public static Tuple<int?, int, List<int>> Run(List<BigInteger> instructions, BigInteger input, int index, int relativeBase)
        {

            var outputs = new List<int>();

            while (instructions[index] != 99)
            {
                var res = ChooseOperation(instructions, input, index, relativeBase);
                
                index = res.Item1;
                relativeBase = res.Item2;

                if (res.Item3.HasValue)
                {
                    outputs.Add(res.Item3.Value);
                }

                if (outputs.Count == 2)
                {
                    break;
                }
            }

            if (instructions[index] == 99)
            {
                Console.WriteLine("end Of while");
                return new Tuple<int?, int, List<int>>(null, 0, outputs);
            }

            return new Tuple<int?, int, List<int>>(index, relativeBase, outputs);
        }
        
        private static Tuple<int, int, int?> ChooseOperation(List<BigInteger> program, BigInteger input,
            int index, int relativeBase)
        {
            var opCode = (int)(program[index] % 10);
            var newIndex = index;
            var newRelativeBase = relativeBase;
            int? output = null;
            switch (opCode)
            {
                case 9:
                    newRelativeBase = (int)GetNewRelativeBase(index, program, relativeBase);
                    newIndex = index + 2;
                    break;
                case 8:
                    Equals(index, program, relativeBase);
                    newIndex = index + 4;
                    break;
                case 7:
                    LessThen(index, program, relativeBase);
                    newIndex = index + 4;
                    break;
                case 6:
                    newIndex = JumpIfFalse(index, program, relativeBase);
                    break;
                case 5:
                    newIndex = JumpIfTrue(index, program, relativeBase);
                    break;
                case 4:
                    var result = GetOutput(index, program, relativeBase);
                    output = (int)result;
                    newIndex = index + 2;
                    break;
                case 3:
                    WriteInput(index, program, input, relativeBase);
                    newIndex = index + 2;
                    break;
                case 2:
                    Multiply(index, program, relativeBase);
                    newIndex = index + 4;
                    break;
                case 1:
                    Sum(index, program, relativeBase);
                    newIndex = index + 4;
                    break;
                default:
                    Console.WriteLine("Unknown {0}", opCode);
                    break;
            }

            return new Tuple<int, int, int?>(newIndex, newRelativeBase, output);
        }
        
        private static void Equals(int index, List<BigInteger> program, int relativeBase)
        {
            var readParams = GetThreeParamsValues(index, program, relativeBase);
            var writeIndex = (int) readParams[2];
            
            if (writeIndex >= program.Count)
            {
                AddNewElementsToLength(writeIndex + 1, program);
            }
            
            program[writeIndex] = readParams[0] == readParams[1] ? 1 : 0;
        }

        private static void LessThen(int index, List<BigInteger> program, int relativeBase)
        {
            var readParams = GetThreeParamsValues(index, program, relativeBase);
            var writeIndex = (int) readParams[2];
            
            if (writeIndex >= program.Count)
            {
                AddNewElementsToLength(writeIndex + 1, program);
            }
            
            program[writeIndex] = readParams[0] < readParams[1] ? 1 : 0;
        }
        
        private static int JumpIfFalse(int index, List<BigInteger> program, int relativeBase)
        {
            var readParams = GetTwoParamsValues(index, program, relativeBase);
            
            return readParams[0] != 0 ? index + 3 : (int)readParams[1];
        }
        
        private static int JumpIfTrue(int index, List<BigInteger> program, int relativeBase)
        {
            var readParams = GetTwoParamsValues(index, program, relativeBase);
            
            return readParams[0] == 0 ? index + 3 : (int)readParams[1];
        }
        
        private static void WriteInput(int index, List<BigInteger> program, BigInteger input, int relativeBase)
        {
            var opCode = (int)program[index];

            var indexOfOperator = index + 1;
            var indexToWrite = (int)program[indexOfOperator];
            var parameterMode = opCode / 100 % 10;

            if (parameterMode == 2)
            {
                program[indexToWrite + relativeBase] = input;
            }
            
            
            program[indexToWrite] = input;
        }

        private static BigInteger GetOutput(int index, List<BigInteger> program, int relativeBase)
        {
            var opCode = (int)program[index];

            var parameterValue = program[index + 1];
            var parameterMode = opCode / 100 % 10;
            
            return GetValueInMode(parameterValue, program, parameterMode, relativeBase);
        }

        private static void Multiply(int index, List<BigInteger> program, int relativeBase)
        {
            CalculateThreeParams(index, program, relativeBase, (a, b) => a * b);
        }

        private static void Sum(int index, List<BigInteger> program, int relativeBase)
        {
            CalculateThreeParams(index, program, relativeBase, (a, b) => a + b);
        }

        private static void CalculateThreeParams(int index, List<BigInteger> program,int relativeBase,
            Func<BigInteger, BigInteger, BigInteger> calc)
        {
            var readParams = GetThreeParamsValues(index, program, relativeBase);
            var thirdParameterValue = (int)readParams[2];

            if (thirdParameterValue >= program.Count)
            {
                AddNewElementsToLength(thirdParameterValue + 1, program);
            }
            
            program[thirdParameterValue] = calc(readParams[0], readParams[1]);
        }

        private static BigInteger[] GetTwoParamsValues(int index, List<BigInteger> program, int relativeBase)
        {
            var opCode = (int)program[index];
            var firstParameterValue = program[index + 1];
            var secondParameterValue = program[index + 2];


            var firstParameterMode = opCode / 100 % 10;
            var secondParameterMode = opCode / 1000 % 10;

            var firstParameter = GetValueInMode(firstParameterValue, program, firstParameterMode, relativeBase);
            var secondParameter = GetValueInMode(secondParameterValue, program, secondParameterMode, relativeBase);
            return new[] {firstParameter, secondParameter};
        }

        private static BigInteger[] GetThreeParamsValues(int index, List<BigInteger> program, int relativeBase)
        {
            var opCode = (int) program[index];
            var firstParameterValue = program[index + 1];
            var secondParameterValue = program[index + 2];
            var thirdParameterValue = program[index + 3];


            var firstParameterMode = opCode / 100 % 10;
            var secondParameterMode = opCode / 1000 % 10;
            var thirdParameterMode = opCode / 10000 % 10;
            
            var firstParameter = GetValueInMode(firstParameterValue, program, firstParameterMode, relativeBase);
            var secondParameter = GetValueInMode(secondParameterValue, program, secondParameterMode, relativeBase);
            var thirdParameter = thirdParameterValue;

            if (thirdParameterMode == 2)
            {
                thirdParameter += relativeBase;
            }

            return new[] {firstParameter, secondParameter, thirdParameter};
        }
        
        private static BigInteger GetValueInMode(BigInteger value, List<BigInteger> program, int mode, int relativeBase)
        {
            int index;
            if (mode == 1)
                return value;
            if (mode == 2)
            {
                index = (int) (value + relativeBase);
                if (index >= program.Count)
                {
                    return 0;
                }
                return program[(int) (value + relativeBase)];
            }

            index = (int)value;
            if (index >= program.Count)
            {
                return 0;
            }
            
            return program[(int) value];
        }
        
        private static BigInteger GetNewRelativeBase(int index, List<BigInteger> program, int relativeBase)
        {
            var opCode = (int)program[index];
            var parameterValue = program[index + 1];
            var parameterMode = opCode / 100 % 10;
            

            return relativeBase + GetValueInMode(parameterValue, program, parameterMode, relativeBase);
        }

        private static void AddNewElementsToLength(int length, List<BigInteger> program)
        {
            while (program.Count < length)
            {
                program.Add(new BigInteger(0));
            }
        }
    }

    class Vector
    {
        public int X;
        public int Y;
    }
}