
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
            this.refreshbutton.Location = new System.Drawing.Point(13, 202);
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
            this.timingText.Location = new System.Drawing.Point(13, 138);
            this.timingText.Name = "timingText";
            this.timingText.Size = new System.Drawing.Size(76, 20);
            this.timingText.TabIndex = 5;
            this.timingText.Text = "Задержка";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 161);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 27);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 259);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.timingText);
            this.Controls.Add(this.sellermsg);
            this.Controls.Add(this.sellerText);
            this.Controls.Add(this.apikey);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.refreshTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Config";
            this.Text = "SS-LOADER | API-Ключ samp-store";
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
    }
}