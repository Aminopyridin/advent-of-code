using System;
using System.Collections.Generic;
using System.Linq;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var input =
                "3,8,1001,8,10,8,105,1,0,0,21,34,59,76,101,114,195,276,357,438,99999,3,9,1001,9,4,9,1002,9,4,9,4,9,99,3,9,102,4,9,9,101,2,9,9,102,4,9,9,1001,9,3,9,102,2,9,9,4,9,99,3,9,101,4,9,9,102,5,9,9,101,5,9,9,4,9,99,3,9,102,2,9,9,1001,9,4,9,102,4,9,9,1001,9,4,9,1002,9,3,9,4,9,99,3,9,101,2,9,9,1002,9,3,9,4,9,99,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99";
            var test1 = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            var test2 = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            var test3 =
                "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";

            var test4 = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            var test5 = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";

            FirstPart(input);
            SecondPart(input);
        }

        static void FirstPart(string rowInstructions)
        {

            var inputForFirstAmplifier = 0;

            var perms = CreatePermutations(0, 4);

            var max = int.MinValue;
            var maxPerm = perms[0];
            foreach (var perm in perms)
            {
                var input = inputForFirstAmplifier; 
                var instructions = rowInstructions.Split(",").Select(int.Parse).ToArray();

                foreach (var i in perm)
                {
                    var inputs = new Queue<int>();
                    inputs.Enqueue(i);
                    inputs.Enqueue(input);
                    input = IntCodeComputer.Run(instructions, inputs);
                }

                
                if (input > max)
                {
                    max = input;
                    maxPerm = perm;
                }
            }
            
            Console.WriteLine("max: {0}, maxPerm: {1}", max, string.Join(",", maxPerm));

        }

        static void SecondPart(string rowInstructions)
        {
            var inputForFirstAmplifier = 0;
            var maxValue = int.MinValue;
            
            var perms = CreatePermutations(5, 9);


            foreach (var perm in perms)
            {
                int lastSignal = 0;
                var inputs = new Queue<int>[5];
                var computerStates = new[] {true, true, true, true, true};
                var indexes = new[] {0, 0, 0, 0, 0};
                var instructions = new[]
                {
                    rowInstructions.Split(",").Select(int.Parse).ToArray(),
                    rowInstructions.Split(",").Select(int.Parse).ToArray(),
                    rowInstructions.Split(",").Select(int.Parse).ToArray(),
                    rowInstructions.Split(",").Select(int.Parse).ToArray(),
                    rowInstructions.Split(",").Select(int.Parse).ToArray(),
                };

                for (int i = 0; i < perm.Length; i++)
                {
                    var input = new Queue<int>();
                    input.Enqueue(perm[i]);
                    if (i == 0) input.Enqueue(inputForFirstAmplifier);
                    inputs[i] = input;
                }

                while (computerStates.Any(i => i))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var result = IntCodeComputer.ChooseOperation2(instructions[i], inputs[i], indexes[i]);

                        if (!result.Item3)
                            computerStates[i] = false;

                        if (result.Item2.HasValue)
                        {
                            var nextIndex = (i + 1) % 5;
                            var value = result.Item2.Value;
                            inputs[nextIndex].Enqueue(value);
                            Console.WriteLine("computer {0} output {1}", i, result.Item2.Value);
                            if (i == 4 && value != 0)
                            {
                                lastSignal = value;
                            }
                        }
                        indexes[i] = result.Item1;
                    }
                }

                maxValue = Math.Max(maxValue, lastSignal);
            }
           
            
            Console.WriteLine(maxValue);

        }
        
        static List<int[]> CreatePermutations(int min, int max)
        {
            var permutations = new List<int[]>();
            for (int a = min; a <= max; a++)
                for (int b = min; b <= max; b++)
                    for (int c = min; c <= max; c++)
                        for (int d = min; d <= max; d++)
                            for (int e = min; e <= max; e++)
                                permutations.Add(new []{a,b,c,d,e});

            return permutations.Where(item => item.Length == item.Distinct().Count()).ToList();
            
        }
    }

    class IntCodeComputer
    {
        public static int Run(string rowInstructions, Queue<int> inputs)
        {
            var instructions = rowInstructions.Split(",").Select(int.Parse).ToArray();
            return ChooseOperation(instructions, inputs);
        }
        
        public static int Run(int[] instructions, Queue<int> inputs)
        {
            return ChooseOperation(instructions, inputs);
        }


        private static int ChooseOperation(int[] program, Queue<int> inputs)
        {
            var index = 0;

            while (program[index] != 99)
            {
                var opCode = program[index] % 10;
                int[] readParams;
                var input = inputs.Count > 1 ? inputs.Dequeue() : inputs.Peek();
                switch (opCode)
                {
                    case 8:
                        readParams = GetThreeParamsValues(index, program);
                        program[readParams[2]] = readParams[0] == readParams[1] ? 1 : 0;
                        index = index + 4;
                        break;
                    case 7:
                        readParams = GetThreeParamsValues(index, program);
                        program[readParams[2]] = readParams[0] < readParams[1] ? 1 : 0;
                        index = index + 4;
                        break;
                    case 6:
                        readParams = GetTwoParamsValues(index, program);
                        index = readParams[0] != 0 ? index + 3 : readParams[1];
                        break;
                    case 5:
                        readParams = GetTwoParamsValues(index, program);
                        index = readParams[0] == 0 ? index + 3 : readParams[1];
                        break;
                    case 4:
                        var result = GetOutput(index, program);
                        return result;
                    case 3:
                        WriteInput(index, program, input);
                        index = index + 2;
                        break;
                    case 2:
                        Multiply(index, program);
                        index = index + 4;
                        break;
                    case 1:
                        Sum(index, program);
                        index = index + 4;
                        break;
                    default:
                        Console.WriteLine("Unknown {0}", opCode);
                        break;
                }
            }

            Console.WriteLine("end Of while");

            return 99;
        }
        
        public static Tuple<int, int?, bool> ChooseOperation2(int[] program, Queue<int> inputs, int index)
        {
            if (program[index] == 99)
            {
                return new Tuple<int, int?, bool>(0,0, false);
            }
            
            var opCode = program[index] % 10;
            int[] readParams;
            int newIndex = index;
            int? output = null;
            
            switch (opCode)
            {
                case 8:
                    readParams = GetThreeParamsValues(index, program);
                    program[readParams[2]] = readParams[0] == readParams[1] ? 1 : 0;
                    newIndex = index + 4;
                    break;
                case 7:
                    readParams = GetThreeParamsValues(index, program);
                    program[readParams[2]] = readParams[0] < readParams[1] ? 1 : 0;
                    newIndex = index + 4;
                    break;
                case 6:
                    readParams = GetTwoParamsValues(index, program);
                    newIndex = readParams[0] != 0 ? index + 3 : readParams[1];
                    break;
                case 5:
                    readParams = GetTwoParamsValues(index, program);
                    newIndex = readParams[0] == 0 ? index + 3 : readParams[1];
                    break;
                case 4:
                    output = GetOutput(index, program);
                    newIndex += 2;
                    break;
                case 3:
                    if (inputs.Count == 0)
                    {
                        return new Tuple<int, int?, bool>(newIndex, output, true);
                    }
                    var input = inputs.Dequeue();
                    WriteInput(index, program, input);
                    newIndex += 2;
                    break;
                case 2:
                    Multiply(index, program);
                    newIndex = index + 4;
                    break;
                case 1:
                    Sum(index, program);
                    newIndex = index + 4;
                    break;
                default:
                    Console.WriteLine("Unknown {0}", opCode);
                    break;
            }
            
            return new Tuple<int, int?, bool>(newIndex, output, true);
        }

        private static void WriteInput(int index, int[] program, int input)
        {
            var indexOfOperator = index + 1;
            var indexToWrite = program[indexOfOperator];
            program[indexToWrite] = input;
        }

        private static int GetOutput(int index, int[] program)
        {
            var opCode = program[index];

            var parameterValue = program[index + 1];
            var parameterMode = opCode / 100 % 10;
            var parameter = parameterMode == 1 ? parameterValue : program[parameterValue];

            return parameter;
        }

        private static void Multiply(int index, int[] program)
        {
            CalculateThreeParams(index, program, (a, b) => a * b);
        }

        private static void Sum(int index, int[] program)
        {
            CalculateThreeParams(index, program, (a, b) => a + b);
        }

        private static void CalculateThreeParams(int index, int[] program, Func<int, int, int> calc)
        {
            var opCode = program[index];
            var firstParameterValue = program[index + 1];
            var secondParameterValue = program[index + 2];
            var thirdParameterValue = program[index + 3];


            var firstParameterMode = opCode / 100 % 10;
            var secondParameterMode = opCode / 1000 % 10;

            var firstParameter = firstParameterMode == 1 ? firstParameterValue : program[firstParameterValue];
            var secondParameter = secondParameterMode == 1 ? secondParameterValue : program[secondParameterValue];

            program[thirdParameterValue] = calc(firstParameter, secondParameter);
        }

        private static int[] GetTwoParamsValues(int index, int[] program)
        {
            var opCode = program[index];
            var firstParameterValue = program[index + 1];
            var secondParameterValue = program[index + 2];


            var firstParameterMode = opCode / 100 % 10;
            var secondParameterMode = opCode / 1000 % 10;

            var firstParameter = firstParameterMode == 1 ? firstParameterValue : program[firstParameterValue];
            var secondParameter = secondParameterMode == 1 ? secondParameterValue : program[secondParameterValue];

            return new[] {firstParameter, secondParameter};
        }

        private static int[] GetThreeParamsValues(int index, int[] program)
        {
            var opCode = program[index];
            var firstParameterValue = program[index + 1];
            var secondParameterValue = program[index + 2];
            var thirdParameterValue = program[index + 3];


            var firstParameterMode = opCode / 100 % 10;
            var secondParameterMode = opCode / 1000 % 10;

            var firstParameter = firstParameterMode == 1 ? firstParameterValue : program[firstParameterValue];
            var secondParameter = secondParameterMode == 1 ? secondParameterValue : program[secondParameterValue];

            return new[] {firstParameter, secondParameter, thirdParameterValue};
        }
    }
}