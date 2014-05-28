using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LoLCameraSharp.Keyboard
{
    public class Hotkeys
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        public Keys subKey { get { return _subKey; } set { _subKey = value; } }
        public Keys mainKey { get { return _mainKey; } set { _mainKey = value; } }

        Keys _subKey;
        Keys _mainKey;

        public Hotkeys()
        {
        }

        public Hotkeys(Keys subKey, Keys mainKey)
        {
            _subKey = subKey;
            _mainKey = mainKey;
        }

        public bool IsTriggered()
        {
            if (_subKey == Keys.None)
                return Convert.ToBoolean(GetAsyncKeyState(_mainKey));
            else
                return Convert.ToBoolean(GetAsyncKeyState(_subKey)) && Convert.ToBoolean(GetAsyncKeyState(_mainKey));
        }
    }
}
