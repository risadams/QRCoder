using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QRCoder
{
    public static partial class PayloadGenerator
    {
        private static bool IsValidIban(string iban)
        {
            //Clean IBAN
            var ibanCleared = iban.ToUpper().Replace(" ", "").Replace("-", "");

            //Check for general structure
            var structurallyValid = Regex.IsMatch(ibanCleared, @"^[a-zA-Z]{2}[0-9]{2}([a-zA-Z0-9]?){16,30}$");

            //Check IBAN checksum
            var sum = $"{ibanCleared.Substring(4)}{ibanCleared.Substring(0, 4)}".ToCharArray().Aggregate("", (current, c) => current + (char.IsLetter(c) ? (c - 55).ToString() : c.ToString()));
            if (!decimal.TryParse(sum, out var sumDec))
                return false;
            var checksumValid = sumDec % 97 == 1;

            return structurallyValid && checksumValid;
        }

        private static bool IsValidQRIban(string iban)
        {
            var foundQrIid = false;
            try
            {
                var ibanCleared   = iban.ToUpper().Replace(" ", "").Replace("-", "");
                var possibleQrIid = Convert.ToInt32(ibanCleared.Substring(4, 5));
                foundQrIid = possibleQrIid >= 30000 && possibleQrIid <= 31999;
            }
            catch { }

            return IsValidIban(iban) && foundQrIid;
        }

        private static bool IsValidBic(string bic) => Regex.IsMatch(bic.Replace(" ", ""), @"^([a-zA-Z]{4}[a-zA-Z]{2}[a-zA-Z0-9]{2}([a-zA-Z0-9]{3})?)$");


        private static string ConvertStringToEncoding(string message, string encoding)
        {
            var iso      = Encoding.GetEncoding(encoding);
            var utf8     = Encoding.UTF8;
            var utfBytes = utf8.GetBytes(message);
            var isoBytes = Encoding.Convert(utf8, iso, utfBytes);
#if NET40
            return iso.GetString(isoBytes);
#else
            return iso.GetString(isoBytes, 0, isoBytes.Length);
#endif
        }

        private static string EscapeInput(string inp, bool simple = false)
        {
            char[] forbiddenChars                 = { '\\', ';', ',', ':' };
            if (simple) forbiddenChars            = new char[1] { ':' };
            foreach (var c in forbiddenChars) inp = inp.Replace(c.ToString(), "\\" + c);
            return inp;
        }


        public static bool ChecksumMod10(string digits)
        {
            if (string.IsNullOrEmpty(digits) || digits.Length < 2)
                return false;
            int[] mods = { 0, 9, 4, 6, 8, 2, 7, 1, 3, 5 };

            var remainder = 0;
            for (var i = 0; i < digits.Length - 1; i++)
            {
                var num = Convert.ToInt32(digits[i]) - 48;
                remainder = mods[(num + remainder) % 10];
            }

            var checksum = (10 - remainder) % 10;
            return checksum == Convert.ToInt32(digits[digits.Length - 1]) - 48;
        }

        private static bool isHexStyle(string inp) => Regex.IsMatch(inp, @"\A\b[0-9a-fA-F]+\b\Z") || Regex.IsMatch(inp, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");

        public abstract class Payload
        {
            public virtual int Version => -1;
            public virtual QRCodeGenerator.ECCLevel EccLevel => QRCodeGenerator.ECCLevel.M;
            public virtual QRCodeGenerator.EciMode EciMode => QRCodeGenerator.EciMode.Default;
            public abstract override string ToString();
        }


        public class BitcoinAddress : BitcoinLikeCryptoCurrencyAddress
        {
            public BitcoinAddress(string address, double? amount, string label = null, string message = null)
                : base(BitcoinLikeCryptoCurrencyType.Bitcoin, address, amount, label, message) { }
        }

        public class BitcoinCashAddress : BitcoinLikeCryptoCurrencyAddress
        {
            public BitcoinCashAddress(string address, double? amount, string label = null, string message = null)
                : base(BitcoinLikeCryptoCurrencyType.BitcoinCash, address, amount, label, message) { }
        }

        public class LitecoinAddress : BitcoinLikeCryptoCurrencyAddress
        {
            public LitecoinAddress(string address, double? amount, string label = null, string message = null)
                : base(BitcoinLikeCryptoCurrencyType.Litecoin, address, amount, label, message) { }
        }
    }
}