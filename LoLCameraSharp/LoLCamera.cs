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
        //Memory Functions:
        MemoryEditor m = new MemoryEditor("League of Legends");
        Pattern p = new Pattern();

        //Address Cache:
        IntPtr YawAddress;
        IntPtr PitchAddress;
        IntPtr YHeightAddress;
        IntPtr FoVAddress;
        IntPtr XPositionAddress;
        IntPtr YPositionAddress;

        //Timer Ticks:

        //General Global Vars
        bool PatternsFound = false;

        public LoLCamera()
        {
            InitializeComponent();
        }

        private void FindGameTick()
        {
            if (m.gameFound)
            {
                if(!PatternsFound)
                    PatternsFound = GetCameraOffsets();
            }
            else
            {
                PatternsFound = false;
                if (m.FindGame("League of Legends"))
                    PatternsFound = GetCameraOffsets();
            }
        }

        private bool GetCameraOffsets()
        {
            //Sanity Check
            if (m.gameFound)
            {
                uint patternAddr = p.FindPattern("\\xA1\\x00\\x00\\x00\\x00\\x83\\xC4\\x14\\x8B\\x00\\x8B\\x0D\\x00\\x00\\x00\\x00\\xD9\\x80\\x00\\x00\\x00\\x00", "x????xxxxxxx????xx????", ref m);

                if (patternAddr == 0)
                    return false; //Pattern is out of date

                //Increment Addr by one to get to the location where the pointer is
                patternAddr++;

                uint pointerAddr = m.ReadUInt((IntPtr)patternAddr);
                pointerAddr = m.ReadUInt((IntPtr)pointerAddr);
                pointerAddr = m.ReadUInt((IntPtr)pointerAddr);

                if (pointerAddr == 0)
                    return false; //Pointer hasn't been setup yet

                FoVAddress = (IntPtr)(pointerAddr + 0x130);
                PitchAddress = (IntPtr)(pointerAddr + 0x124);
                YawAddress = (IntPtr)(pointerAddr + 0x120);
                YPositionAddress = (IntPtr)(pointerAddr + 0x10C);
                XPositionAddress = (IntPtr)(pointerAddr + 0x104);

                patternAddr = p.FindPattern("\\xC7\\x87\\x00\\x00\\x00\\x00\\x00\\x00\\x00\\x00\\xE8\\x00\\x00\\x00\\x00\\x8B\\x87\\x00\\x00\\x00\\x00\\x8D\\x8F\\x00\\x00\\x00\\x00\\x89\\x45\\xCC", "xx????????x????xx????xx????xxx", ref m);
                
                if (patternAddr == 0)
                    return false; //Pattern is out of date

                YHeightAddress = (IntPtr)(patternAddr + 0x06);

                addressView.Lines = DisplayAddresses();
                return true;
            }
            return false;
        }

        private string[] DisplayAddresses()
        {

            List<string>  AddressDisplay = new List<string>();
            AddressDisplay.Add(string.Concat(new object[] { "Yaw Address: ", YawAddress.ToString("X"), ",  Pitch Address: ", PitchAddress.ToString("X") }));
            AddressDisplay.Add(string.Concat(new object[] { "Yaw: ", m.ReadFloat(YawAddress).ToString(), ",  Pitch: ", m.ReadFloat(PitchAddress).ToString() }));
            AddressDisplay.Add(string.Concat(new object[] { "X Position Address: ", XPositionAddress.ToString("X"), ",  Y Position Address: ", YPositionAddress.ToString("X") }));
            AddressDisplay.Add(string.Concat(new object[] { "X: ", m.ReadFloat(XPositionAddress).ToString(), ",  Y: ", m.ReadFloat(YPositionAddress).ToString() }));
            AddressDisplay.Add(string.Concat(new object[] { "Camera Height Address: ", YHeightAddress.ToString("X"), ",  FoV Address: ", FoVAddress.ToString("X") }));

            return AddressDisplay.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindGameTick();
        }
    }
}
