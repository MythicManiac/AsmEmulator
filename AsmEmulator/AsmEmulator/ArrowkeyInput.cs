using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace AsmEmulator
{
    public class ArrowkeyInput : InputDevice
    {
        private byte _state;
        private KeyboardState _lastState;

        public ArrowkeyInput() : base()
        {
            _lastState = Keyboard.GetState();
        }

        public override byte GetState()
        {
            return _state;
        }

        public override void UpdateState()
        {
            var kbs = Keyboard.GetState();
            if (kbs.IsKeyDown(Keys.A) && _lastState.IsKeyUp(Keys.A)) { _state = 1; }
            if (kbs.IsKeyDown(Keys.W) && _lastState.IsKeyUp(Keys.W)) { _state = 2; }
            if (kbs.IsKeyDown(Keys.D) && _lastState.IsKeyUp(Keys.D)) { _state = 4; }
            if (kbs.IsKeyDown(Keys.S) && _lastState.IsKeyUp(Keys.S)) { _state = 8; }
            _lastState = kbs;
        }
    }
}
