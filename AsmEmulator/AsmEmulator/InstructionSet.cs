using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsmEmulator
{
    public abstract class InstructionSet
    {
        public Dictionary<byte, Instruction> BytecodeInstructions { get; private set; }
        public Dictionary<string, Instruction> PlaintextInstructions { get; private set; }

        protected Configuration _configuration;

        public InstructionSet(Configuration configuration)
        {
            _configuration = configuration;
            BytecodeInstructions = new Dictionary<byte, Instruction>();
            PlaintextInstructions = new Dictionary<string, Instruction>();
            Initialize();
        }

        protected virtual void AddInstruction(Instruction instruction)
        {
            var opcode = Convert.ToByte(instruction.Opcode, 2);
            BytecodeInstructions.Add(opcode, instruction);
            PlaintextInstructions.Add(instruction.Identifier, instruction);
        }

        public void MicroinstructionAdvanceMainMemory()
        {
            var pointer = _configuration.Memory.GetPointer();
            pointer += 1;
            _configuration.Memory.SetPointer(pointer);
        }

        protected abstract void Initialize();
    }
}
