using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void folderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderPath = new FolderBrowserDialog();
            DialogResult res = folderPath.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                pathLabel.Text = $"Path: {folderPath.SelectedPath}";
            }
        }
    }
}
