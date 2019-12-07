using System;
using System.Linq;

namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            const string instructions = "3,225,1,225,6,6,1100,1,238,225,104,0,101,20,183,224,101,-63,224,224,4,224,1002,223,8,223,101,6,224,224,1,223,224,223,1101,48,40,225,1101,15,74,225,2,191,40,224,1001,224,-5624,224,4,224,1002,223,8,223,1001,224,2,224,1,223,224,223,1101,62,60,225,1102,92,15,225,102,59,70,224,101,-885,224,224,4,224,1002,223,8,223,101,7,224,224,1,224,223,223,1,35,188,224,1001,224,-84,224,4,224,102,8,223,223,1001,224,2,224,1,223,224,223,1001,66,5,224,1001,224,-65,224,4,224,102,8,223,223,1001,224,3,224,1,223,224,223,1002,218,74,224,101,-2960,224,224,4,224,1002,223,8,223,1001,224,2,224,1,224,223,223,1101,49,55,224,1001,224,-104,224,4,224,102,8,223,223,1001,224,6,224,1,224,223,223,1102,43,46,225,1102,7,36,225,1102,76,30,225,1102,24,75,224,101,-1800,224,224,4,224,102,8,223,223,101,2,224,224,1,224,223,223,1101,43,40,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1008,226,226,224,1002,223,2,223,1005,224,329,1001,223,1,223,8,226,677,224,102,2,223,223,1006,224,344,1001,223,1,223,1007,226,677,224,1002,223,2,223,1005,224,359,101,1,223,223,1008,677,226,224,102,2,223,223,1006,224,374,1001,223,1,223,1107,226,677,224,1002,223,2,223,1006,224,389,1001,223,1,223,107,677,677,224,1002,223,2,223,1006,224,404,101,1,223,223,1007,226,226,224,1002,223,2,223,1006,224,419,101,1,223,223,7,677,226,224,1002,223,2,223,1005,224,434,1001,223,1,223,1007,677,677,224,1002,223,2,223,1006,224,449,101,1,223,223,107,226,226,224,1002,223,2,223,1006,224,464,1001,223,1,223,1108,677,677,224,1002,223,2,223,1005,224,479,101,1,223,223,8,677,226,224,1002,223,2,223,1006,224,494,101,1,223,223,7,226,677,224,102,2,223,223,1005,224,509,1001,223,1,223,1107,677,226,224,102,2,223,223,1005,224,524,1001,223,1,223,1108,677,226,224,1002,223,2,223,1005,224,539,1001,223,1,223,1108,226,677,224,102,2,223,223,1006,224,554,101,1,223,223,108,226,677,224,102,2,223,223,1005,224,569,1001,223,1,223,8,677,677,224,1002,223,2,223,1005,224,584,101,1,223,223,108,677,677,224,1002,223,2,223,1005,224,599,1001,223,1,223,108,226,226,224,102,2,223,223,1006,224,614,101,1,223,223,1008,677,677,224,102,2,223,223,1006,224,629,1001,223,1,223,107,226,677,224,102,2,223,223,1006,224,644,101,1,223,223,1107,677,677,224,1002,223,2,223,1005,224,659,1001,223,1,223,7,226,226,224,1002,223,2,223,1005,224,674,101,1,223,223,4,223,99,226";
            
            FirstPart(instructions);
            SecondPart(instructions);
        }

        private static void FirstPart(string rowInstructions)
        {
            const int input = 1;

            var instructions = rowInstructions.Split(",").Select(int.Parse).ToArray();

            var index = 0;
            while (instructions[index] != 99)
            {
                index = ChooseOperation(index, instructions, input);
            }
            Console.WriteLine("end Of while");
        }

        private static void SecondPart(string rowInstructions)
        {
            const int input = 5;
            var instructions = rowInstructions.Split(",").Select(int.Parse).ToArray();

            var index = 0;
            while (instructions[index] != 99) 
            {
                index = ChooseOperation(index, instructions, input);
            }
            Console.WriteLine("end Of while");
        }

        private static int ChooseOperation(int operationIndex, int[] program,  int input)
        {
            var opCode = program[operationIndex] % 10;
            var newIndex = operationIndex; 
            int[] readParams;
            switch (opCode)
            {
                case 8:
                    readParams = GetThreeParamsValues(operationIndex, program);
                    program[readParams[2]] = readParams[0] == readParams[1] ? 1 : 0;
                    newIndex = operationIndex + 4;
                    break;
                case 7:
                    readParams = GetThreeParamsValues(operationIndex, program);
                    program[readParams[2]] = readParams[0] < readParams[1] ? 1 : 0;
                    newIndex = operationIndex + 4;
                    break;
                case 6:
                    readParams = GetTwoParamsValues(operationIndex, program);
                    newIndex = readParams[0] != 0 ? operationIndex + 3 : readParams[1];
                    break;
                case 5:
                    readParams = GetTwoParamsValues(operationIndex, program);
                    newIndex = readParams[0] == 0 ? operationIndex + 3 : readParams[1];
                    break;
                case 4:
                    var result = GetOutput(operationIndex, program);
                    Console.WriteLine(result);
                    newIndex = operationIndex + 2;
                    break;
                case 3:
                    WriteInput(operationIndex, program, input);
                    newIndex = operationIndex + 2;
                    break;
                case 2:
                    Multiply(operationIndex, program);
                    newIndex = operationIndex + 4;
                    break;
                case 1:
                    Sum(operationIndex, program);
                    newIndex = operationIndex + 4;
                    break;
                default:
                    Console.WriteLine("Unknown {0}", opCode);
                    break;
            }

            return newIndex;
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

            return new []{firstParameter, secondParameter};
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

            return new []{firstParameter, secondParameter, thirdParameterValue};
        }
    }
}