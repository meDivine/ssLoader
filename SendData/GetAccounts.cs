using System;
using System.Collections.Generic;
using System.IO;
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
                new System.IO.StreamReader($@"{path}\Arizona RP\Mesa\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }

        public List<string> CheckNameArizonaKingman(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Kingman\goods\ALL_GOODS.txt");
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
        public List<string> CheckNameSrpZerotwo(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Samp RP\02\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameSrplegacytwo(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Samp RP\Legacy\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameSrpRevolution(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Samp RP\Revolution\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameSrpClassic(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Samp RP\Classic\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion

        #region AdvanceRP
        public List<string> CheckNameAdvanceBlue(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Advance RP\Blue\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameAdvanceRed(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Advance RP\Red\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Advance RP\Red\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameAdvanceGreen(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Advance RP\Green\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameAdvanceLime(string path)
        {
            string line;
            var accs = new List<string>();

            var file =
                new System.IO.StreamReader($@"{path}\Advance RP\Lime\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion
    }
}
