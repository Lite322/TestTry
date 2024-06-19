using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTry
{
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        internal string pasForDB;
        public string Password { get => GetPass(pasForDB); set => pasForDB = SetPass(value); }
        public string NameDB { get; set; }

        private string SetPass(string value)
        {
            return CryptoService.CryptokeyText(value, "SecretKey");
        }

        private string GetPass(string pass)
        {
            return CryptoService.DeCryptokeyText(pass, "SecretKey");
        }

        public override string ToString()
        {
            return $"Имя: {Name}, Login: {Login}, Пароль: {Password}";
        }
    }
}
