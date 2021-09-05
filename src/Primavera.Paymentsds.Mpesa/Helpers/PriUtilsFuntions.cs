using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primavera.Paymentsds.Mpesa.Helpers
{
    public static class PriUtilsFuntions
    {
        public static string StripAlpha(this string self)
        {
            return new string(self.Where(c => Char.IsLetter(c)).ToArray());
        }

        public static string StripNonNumeric(this string self)
        {
            string palavra = new string(self.Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray()); return new string(self.Where(c => (Char.IsDigit(c) || c == '.' || c == ',')).ToArray());  // See Vlad's

        }

        public static string Left(this string self, int tamanho)
        {
            return self.Length > tamanho ? self.Substring(0, tamanho) : self;
        }

        public static string DaString(object s)
        {
            if (s is DBNull)
            {
                return "";
            }
            else
                return Convert.ToString(s);
        }

        public static Int32 DaInt32(object s)
        {
            if (s is DBNull)
            {
                return 0;
            }
            else
                return Convert.ToInt32(s);
        }

        public static Double DaDouble(object s)
        {
            if (s is DBNull)
            {
                return 0;
            }
            else
                return Convert.ToDouble(s);
        }

        public static float DaFloat(object s)
        {
            if (s is DBNull)
            {
                return 0;
            }
            else
                return float.Parse(s.ToString());
        }
    }
}
