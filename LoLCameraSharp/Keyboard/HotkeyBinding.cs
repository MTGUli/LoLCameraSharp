using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoLCameraSharp.Keyboard
{
    public partial class HotkeyBinding : Form
    {
        Hotkeys hotkey = new Hotkeys();
        LoLCamera parentForm;

        public HotkeyBinding(LoLCamera parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            hotkey.subKey = Keys.None;
            hotkey.mainKey = Keys.None;
            HotkeyView.Text = "Binding: <BLANK>";
        }

        private void HotkeyBinding_Load(object sender, EventArgs e)
        {
            HotkeyView.Text = "Binding: <BLANK>";
        }

        private void HandleInput(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Alt || e.KeyCode == Keys.ShiftKey)
                hotkey.subKey = e.KeyCode;
            else
                hotkey.mainKey = e.KeyCode;

            if (hotkey.subKey != Keys.None)
                HotkeyView.Text = "Binding: " + hotkey.subKey.ToString() + "  " + hotkey.mainKey.ToString();
            else
                HotkeyView.Text = "Binding: " + hotkey.mainKey.ToString();
        }

        private void btnConfirmHotkey_Click(object sender, EventArgs e)
        {
            parentForm.ProcessHotkey(hotkey, this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            parentForm.CancelHotkey(this);
        }
    }
}
