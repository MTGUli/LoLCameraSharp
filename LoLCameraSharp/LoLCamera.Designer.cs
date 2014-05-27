namespace LoLCameraSharp
{
    partial class LoLCamera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoLCamera));
            this.tabControlView = new System.Windows.Forms.TabControl();
            this.tabDefault = new System.Windows.Forms.TabPage();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.addressView = new System.Windows.Forms.TextBox();
            this.tabControlView.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlView
            // 
            this.tabControlView.Controls.Add(this.tabDefault);
            this.tabControlView.Controls.Add(this.tabDebug);
            this.tabControlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlView.Location = new System.Drawing.Point(0, 0);
            this.tabControlView.Name = "tabControlView";
            this.tabControlView.SelectedIndex = 0;
            this.tabControlView.Size = new System.Drawing.Size(356, 338);
            this.tabControlView.TabIndex = 0;
            // 
            // tabDefault
            // 
            this.tabDefault.Location = new System.Drawing.Point(4, 22);
            this.tabDefault.Name = "tabDefault";
            this.tabDefault.Size = new System.Drawing.Size(348, 312);
            this.tabDefault.TabIndex = 0;
            this.tabDefault.Text = "Hotkeys and Information";
            this.tabDefault.UseVisualStyleBackColor = true;
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.addressView);
            this.tabDebug.Location = new System.Drawing.Point(4, 22);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Size = new System.Drawing.Size(348, 312);
            this.tabDebug.TabIndex = 0;
            this.tabDebug.Text = "Debug Information";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // addressView
            // 
            this.addressView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addressView.Enabled = false;
            this.addressView.Location = new System.Drawing.Point(0, 0);
            this.addressView.Multiline = true;
            this.addressView.Name = "addressView";
            this.addressView.ReadOnly = true;
            this.addressView.Size = new System.Drawing.Size(348, 312);
            this.addressView.TabIndex = 3;
            this.addressView.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoLCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 338);
            this.Controls.Add(this.tabControlView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoLCamera";
            this.Text = "SkinSpotlights LoL Camera";
            this.Load += new System.EventHandler(this.LoLCamera_Load);
            this.tabControlView.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            this.tabDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlView;
        private System.Windows.Forms.TabPage tabDefault;
        private System.Windows.Forms.TabPage tabDebug;
        private System.Windows.Forms.TextBox addressView;

    }
}

