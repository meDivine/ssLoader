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
        #region ArizonaRP
        public List<string> CheckNameArizonaBrainburg(string path)
        {
            string line;
            var accs = new List<string>();
            
            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Brainburg\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaChandler(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Chandler\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaGilbert(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Gilbert\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaGlendale(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Glendale\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaMesa(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Glendale\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaPayson(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Payson\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaPhoenix(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Phoenix\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaPrescott(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Prescott\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaRedRock(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Red Rock\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaSaintRose(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Saint Rose\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaScottdale(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Scottdale\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaSurprise(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Surprise\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaTucson(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Tucson\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaWinslow(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Winslow\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameArizonaYuma(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Yuma\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion

        #region SampRP

        #endregion
    }
}
