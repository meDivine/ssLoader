
using System;

namespace ssLoader
{
    partial class Config
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
            this.refreshTextBox = new System.Windows.Forms.TextBox();
            this.refreshbutton = new System.Windows.Forms.Button();
            this.apikey = new System.Windows.Forms.Label();
            this.messageboxSeler = new System.Windows.Forms.TextBox();
            this.message = new System.Windows.Forms.Label();
            this.sellerText = new System.Windows.Forms.Label();
            this.sellermsg = new System.Windows.Forms.TextBox();
            this.timingText = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.proxyCheckBox = new System.Windows.Forms.CheckBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // refreshTextBox
            // 
            this.refreshTextBox.Location = new System.Drawing.Point(13, 55);
            this.refreshTextBox.Name = "refreshTextBox";
            this.refreshTextBox.Size = new System.Drawing.Size(365, 27);
            this.refreshTextBox.TabIndex = 0;
            this.refreshTextBox.TextChanged += new System.EventHandler(this.refreshTextBox_TextChanged);
            // 
            // refreshbutton
            // 
            this.refreshbutton.Location = new System.Drawing.Point(13, 337);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(102, 29);
            this.refreshbutton.TabIndex = 1;
            this.refreshbutton.Text = "Применить";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // apikey
            // 
            this.apikey.AutoSize = true;
            this.apikey.Location = new System.Drawing.Point(13, 32);
            this.apikey.Name = "apikey";
            this.apikey.Size = new System.Drawing.Size(74, 20);
            this.apikey.TabIndex = 2;
            this.apikey.Text = "API ключ ";
            // 
            // messageboxSeler
            // 
            this.messageboxSeler.Location = new System.Drawing.Point(12, 112);
            this.messageboxSeler.Multiline = true;
            this.messageboxSeler.Name = "messageboxSeler";
            this.messageboxSeler.Size = new System.Drawing.Size(366, 34);
            this.messageboxSeler.TabIndex = 3;
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(12, 89);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(182, 20);
            this.message.TabIndex = 4;
            this.message.Text = "Сообщение от продавца";
            // 
            // sellerText
            // 
            this.sellerText.AutoSize = true;
            this.sellerText.Location = new System.Drawing.Point(12, 85);
            this.sellerText.Name = "sellerText";
            this.sellerText.Size = new System.Drawing.Size(182, 20);
            this.sellerText.TabIndex = 3;
            this.sellerText.Text = "Сообщение от продавца";
            // 
            // sellermsg
            // 
            this.sellermsg.Location = new System.Drawing.Point(13, 108);
            this.sellermsg.Name = "sellermsg";
            this.sellermsg.Size = new System.Drawing.Size(365, 27);
            this.sellermsg.TabIndex = 4;
            // 
            // timingText
            // 
            this.timingText.AutoSize = true;
            this.timingText.Location = new System.Drawing.Point(11, 251);
            this.timingText.Name = "timingText";
            this.timingText.Size = new System.Drawing.Size(76, 20);
            this.timingText.TabIndex = 5;
            this.timingText.Text = "Задержка";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 274);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 27);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // proxyCheckBox
            // 
            this.proxyCheckBox.AutoSize = true;
            this.proxyCheckBox.Location = new System.Drawing.Point(13, 307);
            this.proxyCheckBox.Name = "proxyCheckBox";
            this.proxyCheckBox.Size = new System.Drawing.Size(183, 24);
            this.proxyCheckBox.TabIndex = 7;
            this.proxyCheckBox.Text = "Использовать прокси";
            this.proxyCheckBox.UseVisualStyleBackColor = true;
            this.proxyCheckBox.CheckedChanged += new System.EventHandler(this.proxyCheckBox_CheckedChanged);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(13, 161);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(365, 27);
            this.textBoxTitle.TabIndex = 9;
            this.textBoxTitle.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(12, 138);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(143, 20);
            this.titleLabel.TabIndex = 8;
            this.titleLabel.Text = "Описание аккаунта";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(452, 60);
            this.label1.TabIndex = 10;
            this.label1.Text = "Пример: Уровень %level% | Денег: %money% | Машины: %cars%\r\n%money% = 123к\r\n%money" +
    "2% = 123000\r\n";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 388);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.proxyCheckBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.timingText);
            this.Controls.Add(this.sellermsg);
            this.Controls.Add(this.sellerText);
            this.Controls.Add(this.apikey);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.refreshTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Config";
            this.Text = "SS-LOADER |  Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.TextBox refreshTextBox;
        private System.Windows.Forms.Button refreshbutton;
        private System.Windows.Forms.Label apikey;
        private System.Windows.Forms.TextBox messageboxSeler;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.Label sellerText;
        private System.Windows.Forms.TextBox sellermsg;
        private System.Windows.Forms.Label timingText;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox proxyCheckBox;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label label1;
    }
}