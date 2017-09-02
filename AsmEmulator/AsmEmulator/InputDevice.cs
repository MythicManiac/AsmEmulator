using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsmEmulator
{
    public abstract class InputDevice
    {
        public InputDevice() { }
        public abstract void UpdateState();
        public abstract byte GetState();
    }
}
