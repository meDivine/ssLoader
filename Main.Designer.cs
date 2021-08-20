﻿
namespace ssLoader
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowse = new System.Windows.Forms.Button();
            this.pathLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moneyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aPIKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowse
            // 
            this.folderBrowse.Location = new System.Drawing.Point(12, 191);
            this.folderBrowse.Name = "folderBrowse";
            this.folderBrowse.Size = new System.Drawing.Size(94, 29);
            this.folderBrowse.TabIndex = 0;
            this.folderBrowse.Text = "Browse";
            this.folderBrowse.UseVisualStyleBackColor = true;
            this.folderBrowse.Click += new System.EventHandler(this.folderBrowse_Click);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(12, 168);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(40, 20);
            this.pathLabel.TabIndex = 1;
            this.pathLabel.Text = "Path:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(380, 30);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.priceToolStripMenuItem,
            this.aPIKeyToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.fileToolStripMenuItem.Text = "Configurate";
            // 
            // priceToolStripMenuItem
            // 
            this.priceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moneyToolStripMenuItem,
            this.levelToolStripMenuItem});
            this.priceToolStripMenuItem.Name = "priceToolStripMenuItem";
            this.priceToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.priceToolStripMenuItem.Text = "Цены";
            // 
            // moneyToolStripMenuItem
            // 
            this.moneyToolStripMenuItem.Name = "moneyToolStripMenuItem";
            this.moneyToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.moneyToolStripMenuItem.Text = "Валюта";
            this.moneyToolStripMenuItem.Click += new System.EventHandler(this.moneyToolStripMenuItem_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(145, 26);
            this.levelToolStripMenuItem.Text = "Уровни";
            this.levelToolStripMenuItem.Click += new System.EventHandler(this.levelToolStripMenuItem_Click);
            // 
            // aPIKeyToolStripMenuItem
            // 
            this.aPIKeyToolStripMenuItem.Name = "aPIKeyToolStripMenuItem";
            this.aPIKeyToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aPIKeyToolStripMenuItem.Text = "Настройки";
            this.aPIKeyToolStripMenuItem.Click += new System.EventHandler(this.aPIKeyToolStripMenuItem_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(282, 189);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(86, 31);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 253);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.folderBrowse);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "SS-LOADER | by Divine";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button folderBrowse;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moneyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aPIKeyToolStripMenuItem;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ToolStripMenuItem levelToolStripMenuItem;
        public System.Windows.Forms.Label label1;
    }
}

