using System.Collections.Generic;
using System.IO;

namespace ssLoader.Arizona
{
    public class GetAccounts
    {
        #region ArizonaRP
        public List<string> CheckNameArizonaBrainburg(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Arizona RP\Brainburg\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Chandler\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Gilbert\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Glendale\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Mesa\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Kingman\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Payson\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Phoenix\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Prescott\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Red Rock\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Saint Rose\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Scottdale\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Surprise\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Tucson\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Winslow\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Arizona RP\Yuma\goods\ALL_GOODS.txt")) return null;
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

        public List<string> CheckNameArizonaShowLow(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Arizona RP\Show Low\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Arizona RP\Show Low\goods\ALL_GOODS.txt");
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
            if (!File.Exists($@"{path}\Samp RP\02\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Samp RP\Legacy\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Samp RP\Revolution\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Samp RP\Classic\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Advance RP\Blue\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Advance RP\Green\goods\ALL_GOODS.txt")) return null;
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
            if (!File.Exists($@"{path}\Advance RP\Lime\goods\ALL_GOODS.txt")) return null;
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
        #region RadmirRP
        public List<string> CheckNameRadmir1(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 1\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 1\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir2(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 2\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 2\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir3(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 3\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 3\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir4(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 4\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 4\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir5(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 5\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 5\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir6(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 6\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 6\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir7(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 7\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 7\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir8(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 8\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 8\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir9(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 9\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 9\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir10(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 10\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 10\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir11(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 11\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 11\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir12(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 12\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 12\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRadmir13(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Radmir RP\Server 13\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Radmir RP\Server 13\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion
        #region DiamondRP
        public List<string> CheckNameDiamondEmerald(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Diamond RP\Emerald\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Diamond RP\Emerald\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameDiamondTrilliant(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Diamond RP\Trilliant\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Diamond RP\Trilliant\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameDiamondRuby(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Diamond RP\Ruby\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Diamond RP\Ruby\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion
        #region EvolveRP
        public List<string> CheckNameEvolve1(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Evolve RP\01\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Evolve RP\01\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameEvolve2(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Evolve RP\02\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Evolve RP\02\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameEvolve3(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Evolve RP\03\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Evolve RP\03\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion
        #region RodinaRP
        public List<string> CheckNameRodinaVO(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Rodina RP\Восточный Округ\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Rodina RP\Восточный Округ\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRodinaSO(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Rodina RP\Северный Округ\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Rodina RP\Северный Округ\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRodinaCO(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Rodina RP\Центральный Округ\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Rodina RP\Центральный Округ\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameRodinaYO(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Rodina RP\Южный Округ\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Южный RP\Центральный Округ\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion
        #region TrinitiRP
        public List<string> CheckNameTrinity1(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Trinity RP\01\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Trinity RP\01\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameTrinity2(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Trinity RP\02\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Trinity RP\02\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion
        #region GTARP
        public List<string> CheckNameGTARP1(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\GTA RP\#1\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\GTA RP\#1\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameGTARP2(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\GTA RP\#2\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\GTA RP\#2\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        #endregion
        #region AmazingRP
        public List<string> CheckNameAmazingRed(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Amazing RP\Red\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Amazing RP\Red\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameAmazingYellow(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Amazing RP\Yellow\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Amazing RP\Yellow\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameAmazingGreen(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Amazing RP\Green\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Amazing RP\Green\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameAmazingAzure(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Amazing RP\Azure\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Amazing RP\Azure\goods\ALL_GOODS.txt");
            while ((line = file.ReadLine()) != null)
            {
                var output = line.Substring(0, line.IndexOf(':'));
                accs.Add(output);
            }
            file.Close();
            return accs;
        }
        public List<string> CheckNameAmazingSilver(string path)
        {
            string line;
            var accs = new List<string>();
            if (!File.Exists($@"{path}\Amazing RP\Silver\goods\ALL_GOODS.txt")) return null;
            var file =
                new System.IO.StreamReader($@"{path}\Amazing RP\Silver\goods\ALL_GOODS.txt");
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
