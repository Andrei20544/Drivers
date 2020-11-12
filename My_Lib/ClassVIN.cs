using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIN_LIB
{
    public class ClassVIN
    {
        public char[] Simv= { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'U',
        'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        public char[] Simv1 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T' };

        public char[] SimYr = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 
            '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public char[] Sim = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'X' };
        public char[] Num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public char[] cont = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        public char[] cont1 = { 'J', 'K', 'L', 'M', 'N', 'P', 'R' };
        public char[] cont2 = { 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public Dictionary<char, int> countries;
        public Dictionary<int, int> weight;
        public Dictionary<string, string> cc;
        public Dictionary<char, int> YR;

        public ClassVIN()
        {
            countries = new Dictionary<char, int>();
            countries.Add('A', 1);
            countries.Add('B', 2);
            countries.Add('C', 3);
            countries.Add('D', 4);
            countries.Add('E', 5);
            countries.Add('F', 6);
            countries.Add('G', 7);
            countries.Add('H', 8);
            countries.Add('J', 1);
            countries.Add('K', 2);
            countries.Add('L', 3);
            countries.Add('M', 4);
            countries.Add('N', 5);
            countries.Add('P', 7);
            countries.Add('R', 9);
            countries.Add('S', 2);
            countries.Add('T', 3);
            countries.Add('U', 4);
            countries.Add('V', 5);
            countries.Add('W', 6);
            countries.Add('X', 7);
            countries.Add('Y', 8);
            countries.Add('Z', 9);

            weight = new Dictionary<int, int>();
            weight.Add(1, 8);
            weight.Add(2, 7);
            weight.Add(3, 6);
            weight.Add(4, 5);
            weight.Add(5, 4);
            weight.Add(6, 3);
            weight.Add(7, 2);
            weight.Add(8, 10);
            weight.Add(10, 9);
            weight.Add(11, 8);
            weight.Add(12, 7);
            weight.Add(13, 6);
            weight.Add(14, 5);
            weight.Add(15, 4);
            weight.Add(16, 3);
            weight.Add(17, 2);

            YR = new Dictionary<char, int>();
            int y = 1980;
            for (int i = 0; i < SimYr.Length; i++)
            {
                YR.Add(SimYr.ElementAt(i), y);
                y += 1;
            }
        }

        public bool CheckVIN (string vin)
        {
            if (vin.Length > 17 || vin.Length < 17) return false;
            string WMI = vin.Substring(0, 3);

            if (!Array.Exists(Simv, El => El.Equals(WMI[0])) || !Array.Exists(Simv, El => El.Equals(WMI[1])) || !Array.Exists(Simv, El => El.Equals(WMI[2]))) return false;
            string VDS = vin.Substring(3, 5);

            for (int i = 0; i < VDS.Length; i++)
                if (!Array.Exists(Sim, El => El.Equals(VDS[i]))) return false;

            string VIS = vin.Substring(9, 8);
            if (!Array.Exists(Simv, El => El.Equals(VIS[0])) || !Array.Exists(Simv, El => El.Equals(VIS[1])) || !Array.Exists(Simv, El => El.Equals(VIS[2])) ||
                !Array.Exists(Simv, El => El.Equals(VIS[3]))) return false;

            int ch =int.Parse(VIS.Substring(4, 4));
            if (ch < 1000 && ch > 10000) return false;
            int sum = 0;
            for (int i = 0; i < vin.Length; i++)
            {
                if (i != 8)
                {
                    int ves = weight[i + 1];
                    int c = countries[vin[i]];
                    sum += ves * c;
                }
            }
            int s = sum / 11;
            int d = s * 11;
            int CHK = sum - d;
            if (!(CHK == 10 && vin[8] == 'X') || !(int.Parse(vin[8].ToString()) >= 0 && int.Parse(vin[8].ToString()) <= 9)) return false;
            return true;
        }

        public string GetVINCountry(string vin)
        {
            string WMI = vin.Substring(0, 2);

            string Zone = " ";
            string Countries = " ";
            int c = 0;

            char[] Simv1_1 = cont1;
            cc = new Dictionary<string, string>();
            cc.Clear();

            //
            if (Array.Exists(Num, El => El.Equals(WMI[0])))
                c = int.Parse(WMI[0].ToString());
            //


            //Страны Африки в словарь
            if (Array.Exists(cont, El => El.Equals(WMI[0])))
            {
                for (int i = 0; i < cont.Length; i++)
                {
                    cc.Add($"A{cont.ElementAt(i)}", "ЮАР");
                }

                Simv1_1 = cont1.Take(5).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"A{Simv1_1.ElementAt(i)}", "Кот-д’Ивуар");
                }
            }

            //Страны Азии в словарь
            if (Array.Exists(cont1, El => El.Equals(WMI[0])))
            {
                Simv1_1 = Simv.Take(17).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"J{Simv1_1.ElementAt(i)}", "Япония");
                }

                Simv1_1 = cont.Take(5).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"K{Simv1_1.ElementAt(i)}", "Шри Ланка");
                }
            }

            //Страны Европы в словарь
            if (Array.Exists(cont2, El => El.Equals(WMI[0])))
            {
                Simv1_1 = Simv.Take(12).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"S{Simv1_1.ElementAt(i)}", "Великобритания");
                }

                Simv1_1 = Simv1;
                Array.Reverse(Simv1_1);
                Array.Resize(ref Simv1_1, 5);
                Simv1_1 = Simv1.Take(5).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"S{Simv1_1.ElementAt(i)}", "Германия");
                }
            }

            //Страны Северной Америки в словарь
            if (c >= 1 && c <= 5)
            {
                Simv1_1 = Simv.Take(24).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"1{Simv1_1.ElementAt(i)}", "США");
                }

                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"2{Simv1_1.ElementAt(i)}", "Канада");
                }
            }

            //Страны Океании в словарь
            if (c >= 6 && c <= 7)
            {
                Simv1_1 = Simv.Take(20).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"6{Simv1_1.ElementAt(i)}", "Австралия");
                }

                Simv1_1 = Simv;
                Array.Reverse(Simv1_1);
                Array.Resize(ref Simv1_1, 13);
                Simv1_1 = Simv1_1.Take(13).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"6{Simv1_1.ElementAt(i)}", "Не используетсяа");
                }
            }

            //Страны ЮЖНОЙ АМЕРИКИ в словарь
            if (c == 8 || c == 9)
            {
                Simv1_1 = cont.Take(5).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"8{Simv1_1.ElementAt(i)}", "Аргентина");
                }

                Simv1_1 = Simv;
                Array.Resize(ref Simv1_1, 10);
                Array.Reverse(Simv1_1);
                Array.Resize(ref Simv1_1, 5);
                Array.Reverse(Simv1_1);
                Simv1_1 = Simv1_1.Take(5).ToArray();
                for (int i = 0; i < Simv1_1.Length; i++)
                {
                    cc.Add($"8{Simv1_1.ElementAt(i)}", "Чили");
                }
            }

            if (Array.Exists(cont, El => El.Equals(WMI[0]))) Zone = "Африка";
            else if (Array.Exists(cont1, El => El.Equals(WMI[0]))) Zone = "Азия";
            else if (Array.Exists(cont2, El => El.Equals(WMI[0]))) Zone = "Европа";
            else if (c >= 1 && c <= 5) Zone = "Северная Америка";
            else if (c == 6 || c == 7) Zone = "Океания";
            else if (c == 8 || c == 9) Zone = "Южная Америка";

            if (cc.ContainsKey(WMI)) return Countries = $"{Zone} - {cc[WMI]}";
          
            return " ";
        }

        public int GetTransportYear(string vin)
        {
            string VIS = vin.Substring(9, 8);
            DateTime dat = new DateTime();
            int year = dat.Year;

            if (YR.ContainsKey(VIS[0]))
            {
                if (YR[VIS[0]] <= year) return YR[VIS[0]];
                else return YR[VIS[0]] + 30;
            }

            return 0;
        }
    }
}
