using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using LoLCameraSharp.MemoryFunctions;

using LoLCameraSharp.Keyboard;

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
        IntPtr ZoomAddress;
        IntPtr ViewDistanceAddress;

        //Timer Ticks:
        Timer UpdateCamera = new Timer();
        Timer SearchForGame = new Timer();

        //General Global Vars
        bool PatternsFound = false;
        Stopwatch deltaTime = new Stopwatch();

        public LoLCamera()
        {
            InitializeComponent();
        }

        private void FindGameTick(object sender, EventArgs e)
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

            if (m.gameFound && PatternsFound)
            {
                SearchForGame.Enabled = false;
                deltaTime.Restart();
                UpdateCamera.Enabled = true;
            }
        }

        private void UpdateCameraTick(object sender, EventArgs e)
        {
            if (m.gameFound)
            {
                if (tabControlView.SelectedTab == tabDebug)
                    addressView.Lines = DisplayAddresses();

                float dt = (float)(deltaTime.Elapsed.TotalMilliseconds / 1000f);
                deltaTime.Restart();
                HandleCamera(dt);  
            }
            else
            {
                addressView.Clear();
                UpdateCamera.Enabled = false;
                SearchForGame.Enabled = true;
            }
        }

        private void HandleCamera(float deltaTime)
        {
            // Check Hotkeys for key presses and adjust camera accordingly, get mouse location etc!
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
                YawAddress = (IntPtr)(pointerAddr + 0x124);
                PitchAddress = (IntPtr)(pointerAddr + 0x120);
                YPositionAddress = (IntPtr)(pointerAddr + 0x10C);
                XPositionAddress = (IntPtr)(pointerAddr + 0x104);
                ZoomAddress = (IntPtr)(pointerAddr + 0x1BC);

                patternAddr = p.FindPattern("\\xC7\\x87\\x00\\x00\\x00\\x00\\x00\\x00\\x00\\x00\\xE8\\x00\\x00\\x00\\x00\\x8B\\x87\\x00\\x00\\x00\\x00\\x8D\\x8F\\x00\\x00\\x00\\x00\\x89\\x45\\xCC", "xx????????x????xx????xx????xxx", ref m);
                
                if (patternAddr == 0)
                    return false; //Pattern is out of date

                YHeightAddress = (IntPtr)(patternAddr + 0x06);

                patternAddr = p.FindPattern("\\xF3\\x0F\\x58\\x15\\x00\\x00\\x00\\x00\\xF3\\x0F\\x11\\x45\\xB8", "xxxx????xxxxx", ref m);

                if (patternAddr == 0)
                    return false; //Pattern is out of date

                patternAddr += 0x4;
                pointerAddr = m.ReadUInt((IntPtr)patternAddr);

                ViewDistanceAddress = (IntPtr)(pointerAddr + 0x10);

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
            AddressDisplay.Add(string.Concat(new object[] { "Camera Height: ", m.ReadFloat(YHeightAddress).ToString(), ",  Field of View: ", m.ReadFloat(FoVAddress).ToString() }));
            AddressDisplay.Add(string.Concat(new object[] { "Zoom Address: ", ZoomAddress.ToString("X"), ",  Value: ", m.ReadFloat(ZoomAddress).ToString() }));
            AddressDisplay.Add(string.Concat(new object[] { "View Distance Address: ", ViewDistanceAddress.ToString("X"), ",  Value: ", m.ReadFloat(ViewDistanceAddress).ToString() }));

            return AddressDisplay.ToArray();
        }

        private void LoLCamera_Load(object sender, EventArgs e)
        {
            //Search for game every second
            SearchForGame.Interval = 1000;
            SearchForGame.Tick += new EventHandler(this.FindGameTick);
            SearchForGame.Enabled = true;

            UpdateCamera.Interval = 1;
            UpdateCamera.Tick += new EventHandler(this.UpdateCameraTick);
        }

        //Hotkey Handling:
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            HotkeyBinding form = new HotkeyBinding(this);
            form.Visible = true;
            this.Visible = false;
        }

        public void ProcessHotkey(Hotkeys hotkey, HotkeyBinding form)
        {
            form.Close();
            this.Visible = true;
        }
        public void CancelHotkey(HotkeyBinding form)
        {
            form.Close();
            this.Visible = true;
        }
    }
}
