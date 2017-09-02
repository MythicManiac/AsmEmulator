using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsmEmulator
{
    public class Instruction
    {
        public string Opcode { get; private set; }
        public string Identifier { get; private set; }
        public Action Action { get; private set; }

        public Instruction(string opcode, string identifier, Action action)
        {
            var isBinary = opcode.All(i => i == '0' || i == '1');
            if (!isBinary) { throw new FormatException("Opcode code must be in binary"); }
            if (opcode.Length != 4) { throw new FormatException("Invalid opcode length"); }

            Opcode = opcode;
            Identifier = identifier;
            Action = action;
        }
    }
}
