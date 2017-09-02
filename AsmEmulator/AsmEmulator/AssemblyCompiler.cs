using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AsmEmulator
{
    public class SyntaxException : Exception
    {
        public SyntaxException(int line, string message) : base(string.Format("Line {0}: {1}", line + 1, message)) { }
    }

    public class AssemblyCompiler
    {
        public static Dictionary<string, int> Points;
        //public static Dictionary<string, string> InstructionSet = new Dictionary<string, string>() {
        //    {"add", "00000001"}, // Add
        //    {"sub", "00000010"}, // Substract
        //    {"jmp", "00000011"}, // Jump
        //    {"jie", "00000100"}, // Jump If Equal
        //    {"jgt", "00000101"}, // Jump Greater Than
        //    {"jlt", "00000110"}, // Jump Less Than
        //    {"lda", "00000111"}, // Load
        //    {"sto", "00001000"}, // Store
        //    {"out", "00001001"}, // Output
        //    {"ota", "00001010"}, // Output from address
        //    {"inp", "00001011"}, // Input
        //    {"hlt", "00001100"}, // Halt
        //    {"and", "00001101"}, // And
        //    {"set", "00001110"}, // Set
        //    {"rnd", "00001111"}, // Random
        //    {"drw", "00010000"}  // Draw
        //};
        //public static Dictionary<string, string> InstructionSet = new Dictionary<string, string>()
        //{
        //        {"set", "0001"}, // Set
        //        {"sto", "0010"}, // Store
        //        {"add", "0011"}, // Add
        //        {"add", "0100"}, // Add
        //        {"out", "0101"}, // Output
        //        {"jmp", "0110"}, // Jump
        //        {"hlt", "1111"}  // Halt
        //};

        public static Dictionary<string, string> InstructionSet;

        private static void ValidateInstruction(int line, string instruction)
        {
            if (InstructionSet.ContainsKey(instruction)) { return; } 
            if (Points.ContainsKey(instruction)) { return; }
            long value;
            if (!long.TryParse(instruction, out value)) { throw new SyntaxException(line, "Invalid instruction"); }
            if (value > 255) { throw new SyntaxException(line, "Number too big"); }
        }

        public static void CompileAssembly(Dictionary<string, string> instructionSet, string sourceName, string destinationName)
        {
            InstructionSet = instructionSet;
            var lines = File.ReadAllLines(sourceName);

            Points = new Dictionary<string, int>();
            var instructions = new string[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (!line.Contains(';')) { throw new SyntaxException(i, "Missing semicolon"); }
                var instruction = line.Substring(0, line.IndexOf(';'));
                //ValidateInstruction(i, instruction);
                instructions[i] = instruction;
                if (line.Length == line.IndexOf(';') + 1) { continue; }
                var point = line.Substring(line.IndexOf(';') + 1);
                if (Points.ContainsKey(point)) { throw new SyntaxException(i, "Jump point already defined"); }
                Points[point] = i;
            }

            var output = new string[lines.Length];

            var jmpInstructions = new List<string>();
            jmpInstructions.Add("jmp");
            //jmpInstructions.Add("jie");
            //jmpInstructions.Add("jgt");
            //jmpInstructions.Add("jlt");

            for (var i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i];
                ValidateInstruction(i, instruction);
                if (InstructionSet.ContainsKey(instruction))
                {
                    output[i] = InstructionSet[instruction];
                    if (jmpInstructions.Contains(instruction))
                    {
                        i++;
                        if (instruction != "jmp")
                        {
                            output[i] = ConvertToBinary(Convert.ToByte(instructions[i]));
                            i++;
                        }
                        output[i] = ConvertToBinary((byte)Points[instructions[i]]);
                    }
                    continue;
                }

                if (Points.ContainsKey(instruction)) { throw new Exception("IMPOSSIBRU!!!"); }

                output[i] = ConvertToBinary(Convert.ToByte(instruction));
            }

            if (File.Exists(destinationName)) { File.Delete(destinationName); }
            File.WriteAllLines(destinationName, output);
        }

        private static string ConvertToBinary(byte value)
        {
            var result = Convert.ToString(value, 2);
            result = new string('0', 8 - result.Length) + result;
            return result;
        }
    }
}
