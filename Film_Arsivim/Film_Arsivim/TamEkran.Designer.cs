﻿namespace Film_Arsivim
{
    partial class TamEkran
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
            this.panelTamEkran = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelTamEkran
            // 
            this.panelTamEkran.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTamEkran.Location = new System.Drawing.Point(0, 0);
            this.panelTamEkran.Name = "panelTamEkran";
            this.panelTamEkran.Size = new System.Drawing.Size(800, 450);
            this.panelTamEkran.TabIndex = 0;
            // 
            // TamEkran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelTamEkran);
            this.Name = "TamEkran";
            this.Text = "TamEkran";
            this.Load += new System.EventHandler(this.TamEkran_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTamEkran;
    }
}