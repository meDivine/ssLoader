using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssLoader.Arizona
{
    public class GetAccounts
    {
        public List<string> CheckNameArizona(string path)
        {
            string line;
            var arizona = new List<string>();
            
            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Brainburg\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                string output = line.Substring(0, line.IndexOf(':'));
                arizona.Add(output);
            }
            file.Close();
            return arizona;
        }
    }
}
