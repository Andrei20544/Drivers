using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace REG_MARK_LIB
{
    public class ClassREG
    {
        char[] simv = { 'А', 'В', 'Е', 'К', 'М', 'Н', 'О', 'Р', 'С', 'Т', 'У', 'Х' };

        public Dictionary<char, int> sm;
        public Dictionary<int, char> sm2;

        public ClassREG()
        {
            int k = 1;
            sm = new Dictionary<char, int>();
            sm2 = new Dictionary<int, char>();

            for (int i = 0; i < simv.Length; i++)
            {
                sm.Add(simv.ElementAt(i), k);
                k++;
            }
            k = 1;
            for (int i = 0; i < simv.Length; i++)
            {
                sm2.Add(k, simv.ElementAt(i));
                k++;
            }

        }

        public bool CheckMark(string mark)
        {
            string mk = mark;

            if (!Array.Exists(simv, El => El.Equals(mk[0])) && !Array.Exists(simv, El => El.Equals(mk[4])) && !Array.Exists(simv, El => El.Equals(mk[5]))) return false;
            mk = mk.Substring(1, 3);
            if (Array.Exists(simv, El => El.Equals(int.Parse(mk)))) return false;
            mk = mark;
            mk = mk.Substring(6, 3);
            if (Array.Exists(simv, El => El.Equals(int.Parse(mk)))) return false;

            if (!(int.Parse(mk) >= 1) && !(int.Parse(mk) <= 92)) return false;

            return true;
        }

        public string GetNextMarkAfter(string mark)
        {
            string mk = mark.Substring(0, 6);

            int rn;
            if (mk[1] == '0' && mk[2] == '0') rn = int.Parse(mk[3].ToString());
            else if (mk[1] == '0') rn = int.Parse(mk.Substring(2, 2));
            else rn = int.Parse(mk.Substring(1, 3));


            string s = mk[0] + mk.Substring(4, 2);
            string zn = "";

            if (rn < 999)
            {
                rn++;

                if (rn < 100 && rn < 10) zn = s[0] + $"00{rn.ToString()}" + s[1] + s[2];
                else if (rn < 100 && rn >= 10) zn = s[0] + $"0{rn.ToString()}" + s[1] + s[2];
                else zn = s[0] + rn.ToString() + s[1] + s[2];

                return zn;
            }
            else if (rn == 999)
            {
                if (sm[s[2]] < 12)
                {
                    zn = s[0] + "001" + s[1] + (sm2[sm[s[2]] + 1]);
                    return zn;
                }
                else if (sm[s[2]] == 12)
                {
                    if (sm[s[1]] < 12)
                    {
                        zn = s[0] + "001" + (sm2[sm[s[1]] + 1]) + "А";
                        return zn;
                    }
                    else if (sm[s[1]] == 12)
                    {
                        if (sm[s[0]] < 12)
                        {
                            zn = (sm2[sm[s[0]] + 1]) + "001" + "А" + "А";
                            return zn;
                        }
                        else if (sm[s[0]] == 12)
                        {
                            zn = "А" + "001" + "А" + "А";
                            return zn;
                        }

                    }
                }
            }

            return mark;
        }

        public string GetNextMarkAfterInRange(string prevMark, string rangeStart, string rangeEnd)
        {
            string prmk = prevMark.Substring(0, 6);
            int rn = int.Parse(prevMark.Substring(1, 3));

            string Rgstart = rangeStart.Substring(0, 6);
            int rnRgstart = int.Parse(rangeStart.Substring(1, 3));

            string Rgend = rangeEnd.Substring(0, 6);
            int rnRgend = int.Parse(rangeEnd.Substring(1, 3));

            int prevMarkInt = (sm[prmk[0]] * 100) + (sm[prmk[4]] * 10) + sm[prmk[5]];
            int rangeStartInt = (sm[Rgstart[0]] * 100) + (sm[Rgstart[4]] * 10) + sm[Rgstart[5]];
            int rangeEndInt = (sm[Rgend[0]] * 100) + (sm[Rgend[4]] * 10) + sm[Rgend[5]];

            if (rangeStartInt < rangeEndInt)
            {
                if (prevMarkInt >= rangeStartInt && prevMarkInt <= rangeEndInt)
                {
                    prmk = GetNextMarkAfter(prmk + 178);
                    return prmk;
                }
                else if (prevMarkInt < rangeStartInt || prevMarkInt > rangeEndInt)
                {
                    return "out of stock";
                }
            }
            else
            {
                if (prevMarkInt <= rangeStartInt && prevMarkInt >= rangeEndInt)
                {
                    prmk = GetNextMarkAfter(prmk + 178);
                    return prmk;
                }
                else if (prevMarkInt > rangeStartInt || prevMarkInt < rangeEndInt)
                {
                    return "out of stock";
                }
            }          

            return "";
        }

        public int GetCombinationsCountInRange(string mark1, string mark2)
        {
            string mk1 = mark1.Substring(0, 6);
            int rn1 = int.Parse(mk1.Substring(1, 3));

            string mk2 = mark2.Substring(0, 6);
            int rn2 = int.Parse(mk2.Substring(1, 3));

            int count = 0;

            int cc1 = (sm[mk1[0]] * 100 ) + (sm[mk1[4]] * 10) + sm[mk1[5]];
            int cc2 = (sm[mk2[0]] * 100) + (sm[mk2[4]] * 10) + sm[mk2[5]];

            if (cc1 > cc2)
            {
                while (mk1.Contains(mk2) != true)
                {
                    mk2 = GetNextMarkAfter(mk2 + 178);
                    count++;
                }
                return count;
            }
            else if (cc1 == cc2)
            {
                return Math.Abs(rn1 - rn2);
            }
            else
            {
                while (mk2.Contains(mk1) != true)
                {
                    mk1 = GetNextMarkAfter(mk1 + 178);
                    count++;
                }
                return count;
            }           

        }
    }
}
