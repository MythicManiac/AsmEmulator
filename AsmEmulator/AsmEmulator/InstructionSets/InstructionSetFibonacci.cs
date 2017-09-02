using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsmEmulator.Configurations;

namespace AsmEmulator.InstructionSets
{
    class InstructionSetFibonacci : InstructionSet
    {
        public ConfigurationFibonacci Configuration { get { return (ConfigurationFibonacci)_configuration; } }
        public InstructionSetFibonacci(ConfigurationFibonacci configuration) : base(configuration) { }

        protected override void Initialize()
        {
            AddInstruction(new Instruction("0001", "set", Set));
            AddInstruction(new Instruction("0010", "sto", Store));
            AddInstruction(new Instruction("0011", "add", Add));
            AddInstruction(new Instruction("0100", "ada", AddFromAddress));
            AddInstruction(new Instruction("0101", "out", Output));
            AddInstruction(new Instruction("0110", "jmp", Jump));
            AddInstruction(new Instruction("0111", "lda", Load));
            AddInstruction(new Instruction("1111", "hlt", Halt));
        }

        private void MicroinstructionJump()
        {
            var pointer = Configuration.Memory.GetValue();
            Configuration.Memory.SetPointer(pointer);
        }

        private void Add()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var value2 = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            var result = value1 + value2;
            if (result > 255) result = 255;
            Configuration.Accumulator.SetValue((byte)result);
        }

        private void AddFromAddress()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var location = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            Configuration.RAM.SetPointer(location);
            var value2 = Configuration.RAM.GetValue();
            var result = value1 + value2;
            if (result > 255) result = 255;
            Configuration.Accumulator.SetValue((byte)result);
        }

        private void Jump()
        {
            MicroinstructionJump();
        }

        private void Set()
        {
            var value = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            Configuration.Accumulator.SetValue(value);
        }

        private void Store()
        {
            var pointer = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            Configuration.RAM.SetPointer(pointer);
            var value = Configuration.Accumulator.GetValue();
            Configuration.RAM.SetValue(value);
        }

        private void Load()
        {
            var pointer = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            Configuration.RAM.SetPointer(pointer);
            var value = Configuration.RAM.GetValue();
            Configuration.Accumulator.SetValue(value);
        }

        private void Output()
        {
            var value = Configuration.Accumulator.GetValue();
            Configuration.SegmentDisplay.Input(value);
        }

        private void Halt()
        {
        }
    }
}
