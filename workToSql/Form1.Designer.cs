﻿namespace workToSql
{
    partial class Form1
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
            this.treeFolders = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeFolders
            // 
            this.treeFolders.Location = new System.Drawing.Point(12, 12);
            this.treeFolders.Name = "treeFolders";
            this.treeFolders.Size = new System.Drawing.Size(177, 97);
            this.treeFolders.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 541);
            this.Controls.Add(this.treeFolders);
            this.Name = "Form1";
            this.Text = "frmWithSQL";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeFolders;
    }
}