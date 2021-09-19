
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
            this.коэффМашинToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.начальныеЦеныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.конфигЗаливаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aPIKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проксиЧекерToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startButton = new System.Windows.Forms.Button();
            this.timeEnd = new System.Windows.Forms.Label();
            this.versions = new System.Windows.Forms.Label();
            this.Author = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowse
            // 
            this.folderBrowse.Location = new System.Drawing.Point(12, 98);
            this.folderBrowse.Name = "folderBrowse";
            this.folderBrowse.Size = new System.Drawing.Size(94, 29);
            this.folderBrowse.TabIndex = 0;
            this.folderBrowse.Text = "Сессия";
            this.folderBrowse.UseVisualStyleBackColor = true;
            this.folderBrowse.Click += new System.EventHandler(this.folderBrowse_Click);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(12, 75);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(44, 20);
            this.pathLabel.TabIndex = 1;
            this.pathLabel.Text = "Путь:";
            this.pathLabel.Click += new System.EventHandler(this.pathLabel_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.проксиЧекерToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(457, 30);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.priceToolStripMenuItem,
            this.конфигЗаливаToolStripMenuItem,
            this.aPIKeyToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(125, 24);
            this.fileToolStripMenuItem.Text = "Конфигурация";
            // 
            // priceToolStripMenuItem
            // 
            this.priceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moneyToolStripMenuItem,
            this.levelToolStripMenuItem,
            this.коэффМашинToolStripMenuItem,
            this.начальныеЦеныToolStripMenuItem});
            this.priceToolStripMenuItem.Name = "priceToolStripMenuItem";
            this.priceToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.priceToolStripMenuItem.Text = "Цены";
            // 
            // moneyToolStripMenuItem
            // 
            this.moneyToolStripMenuItem.Name = "moneyToolStripMenuItem";
            this.moneyToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.moneyToolStripMenuItem.Text = "Валюта";
            this.moneyToolStripMenuItem.Click += new System.EventHandler(this.moneyToolStripMenuItem_Click);
            // 
            // levelToolStripMenuItem
            // 
            this.levelToolStripMenuItem.Name = "levelToolStripMenuItem";
            this.levelToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.levelToolStripMenuItem.Text = "Уровни";
            this.levelToolStripMenuItem.Click += new System.EventHandler(this.levelToolStripMenuItem_Click);
            // 
            // коэффМашинToolStripMenuItem
            // 
            this.коэффМашинToolStripMenuItem.Name = "коэффМашинToolStripMenuItem";
            this.коэффМашинToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.коэффМашинToolStripMenuItem.Text = "Коэфф. Машин";
            this.коэффМашинToolStripMenuItem.Click += new System.EventHandler(this.коэффМашинToolStripMenuItem_Click);
            // 
            // начальныеЦеныToolStripMenuItem
            // 
            this.начальныеЦеныToolStripMenuItem.Name = "начальныеЦеныToolStripMenuItem";
            this.начальныеЦеныToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.начальныеЦеныToolStripMenuItem.Text = "Начальные цены";
            this.начальныеЦеныToolStripMenuItem.Click += new System.EventHandler(this.начальныеЦеныToolStripMenuItem_Click);
            // 
            // конфигЗаливаToolStripMenuItem
            // 
            this.конфигЗаливаToolStripMenuItem.Name = "конфигЗаливаToolStripMenuItem";
            this.конфигЗаливаToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.конфигЗаливаToolStripMenuItem.Text = "Конфиг Залива";
            this.конфигЗаливаToolStripMenuItem.Click += new System.EventHandler(this.конфигЗаливаToolStripMenuItem_Click);
            // 
            // aPIKeyToolStripMenuItem
            // 
            this.aPIKeyToolStripMenuItem.Name = "aPIKeyToolStripMenuItem";
            this.aPIKeyToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.aPIKeyToolStripMenuItem.Text = "Настройки";
            this.aPIKeyToolStripMenuItem.Click += new System.EventHandler(this.aPIKeyToolStripMenuItem_Click);
            // 
            // проксиЧекерToolStripMenuItem
            // 
            this.проксиЧекерToolStripMenuItem.Name = "проксиЧекерToolStripMenuItem";
            this.проксиЧекерToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.проксиЧекерToolStripMenuItem.Text = "Прокси чекер";
            this.проксиЧекерToolStripMenuItem.Click += new System.EventHandler(this.проксиЧекерToolStripMenuItem_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(244, 96);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(201, 31);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Начать";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // timeEnd
            // 
            this.timeEnd.AutoSize = true;
            this.timeEnd.Location = new System.Drawing.Point(12, 55);
            this.timeEnd.Name = "timeEnd";
            this.timeEnd.Size = new System.Drawing.Size(90, 20);
            this.timeEnd.TabIndex = 4;
            this.timeEnd.Text = "Окончание:";
            // 
            // versions
            // 
            this.versions.AutoSize = true;
            this.versions.Location = new System.Drawing.Point(12, 130);
            this.versions.Name = "versions";
            this.versions.Size = new System.Drawing.Size(39, 20);
            this.versions.TabIndex = 5;
            this.versions.Text = "v 1.2";
            // 
            // Author
            // 
            this.Author.AutoSize = true;
            this.Author.Location = new System.Drawing.Point(12, 153);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(121, 20);
            this.Author.TabIndex = 7;
            this.Author.Text = "Author: @iDivine";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(201, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "Завершить поток";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 182);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Author);
            this.Controls.Add(this.versions);
            this.Controls.Add(this.timeEnd);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.folderBrowse);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "SS-LOADER | by Divine";
            this.Load += new System.EventHandler(this.Main_Load);
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
        private System.Windows.Forms.Label timeEnd;
        private System.Windows.Forms.ToolStripMenuItem коэффМашинToolStripMenuItem;
        private System.Windows.Forms.Label versions;
        private System.Windows.Forms.ToolStripMenuItem начальныеЦеныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem конфигЗаливаToolStripMenuItem;
        private System.Windows.Forms.Label Author;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem проксиЧекерToolStripMenuItem;
    }
}

