using Newtonsoft.Json;
using ssLoader.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader
{
    public partial class Config : Form
    {
        private string getCurrDir = Directory.GetCurrentDirectory();

        public Config()
        {
            OnConnect();
            InitializeComponent();
        }
    }
}
