using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LoLCameraSharp.MemoryFunctions;

namespace LoLCameraSharp
{
    public partial class LoLCamera : Form
    {
        public LoLCamera()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Function Testing!

            MemoryEditor m = new MemoryEditor("League of Legends");
            Pattern p = new Pattern();

            if (m.gameFound)
            {
                uint patternAddr = p.FindPattern("\\xA1\\x00\\x00\\x00\\x00\\x83\\xC4\\x14\\x8B\\x00\\x8B\\x0D\\x00\\x00\\x00\\x00\\xD9\\x80\\x00\\x00\\x00\\x00", "x????xxxxxxx????xx????", ref m);

                //Increment Addr by one to get to the location where the pointer is
                patternAddr++;

                uint pointerAddr = m.ReadUInt((IntPtr) patternAddr);
                pointerAddr = m.ReadUInt((IntPtr) pointerAddr);

                //Pointer + 0x130, to get to location we want to for rotation, + 0x104 to get to X/Y Coord
                uint FoVAddr = m.ReadUInt((IntPtr)pointerAddr) + 0x130;
                uint YRotationAddr = FoVAddr - 0x0C;
                uint XRotationAddr = FoVAddr - 0x10;

                textBox1.Text = FoVAddr.ToString("X");

                patternAddr = p.FindPattern("\\xC7\\x87\\x00\\x00\\x00\\x00\\x00\\x00\\x00\\x00\\xE8\\x00\\x00\\x00\\x00\\x8B\\x87\\x00\\x00\\x00\\x00\\x8D\\x8F\\x00\\x00\\x00\\x00\\x89\\x45\\xCC", "xx????????x????xx????xx????xxx", ref m);
                uint ZPositionAddr = patternAddr + 0x06;
            }
        }
    }
}
