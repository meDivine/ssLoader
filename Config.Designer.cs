
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
            this.refreshbutton.Location = new System.Drawing.Point(12, 152);
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
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 194);
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
    }
}