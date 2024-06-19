using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTry
{
    public class CryptoService
    {
        public static string CryptokeyText(string content, string key)
        {
            string str = key + content; //переделать
            return str;
        }

        public static string DeCryptokeyText(string cryptoContent, string key)
        {
            string str = cryptoContent.TrimStart(key.ToCharArray()); //переделать
            return str;
        }
    }
}
