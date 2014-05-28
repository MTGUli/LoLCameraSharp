namespace LoLCameraSharp.Keyboard
{
    partial class HotkeyBinding
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotkeyBinding));
            this.HotkeyView = new System.Windows.Forms.TextBox();
            this.btnConfirmHotkey = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HotkeyView
            // 
            this.HotkeyView.Location = new System.Drawing.Point(12, 12);
            this.HotkeyView.Name = "HotkeyView";
            this.HotkeyView.ReadOnly = true;
            this.HotkeyView.Size = new System.Drawing.Size(215, 20);
            this.HotkeyView.TabIndex = 0;
            // 
            // btnConfirmHotkey
            // 
            this.btnConfirmHotkey.Location = new System.Drawing.Point(12, 38);
            this.btnConfirmHotkey.Name = "btnConfirmHotkey";
            this.btnConfirmHotkey.Size = new System.Drawing.Size(215, 21);
            this.btnConfirmHotkey.TabIndex = 1;
            this.btnConfirmHotkey.Text = "Confirm";
            this.btnConfirmHotkey.UseVisualStyleBackColor = true;
            this.btnConfirmHotkey.Click += new System.EventHandler(this.btnConfirmHotkey_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 65);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(215, 21);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 92);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(215, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // HotkeyBinding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 113);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnConfirmHotkey);
            this.Controls.Add(this.HotkeyView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(HandleInput);
            this.Name = "HotkeyBinding";
            this.Text = "Bind a Hotkey";
            this.Load += new System.EventHandler(this.HotkeyBinding_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox HotkeyView;
        private System.Windows.Forms.Button btnConfirmHotkey;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
    }
}