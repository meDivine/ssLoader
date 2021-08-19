
namespace ssLoader
{
    partial class ApiKey
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
            this.SuspendLayout();
            // 
            // refreshTextBox
            // 
            this.refreshTextBox.Location = new System.Drawing.Point(13, 55);
            this.refreshTextBox.Name = "refreshTextBox";
            this.refreshTextBox.Size = new System.Drawing.Size(337, 27);
            this.refreshTextBox.TabIndex = 0;
            // 
            // refreshbutton
            // 
            this.refreshbutton.Location = new System.Drawing.Point(13, 88);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(94, 29);
            this.refreshbutton.TabIndex = 1;
            this.refreshbutton.Text = "Обновить";
            this.refreshbutton.UseVisualStyleBackColor = true;
            // 
            // ApiKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.refreshTextBox);
            this.Name = "ApiKey";
            this.Text = "SS-LOADER | API-Ключ samp-store";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox refreshTextBox;
        private System.Windows.Forms.Button refreshbutton;
    }
}