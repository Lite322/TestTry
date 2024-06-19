using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTry
{
    public class UsersGenerator
    {
        public static List<User> Add(string pref, string dbpref, int count, int lenghtpass, string cs)
        {

            try
            {
                List<User> list = new List<User>();
                for (int i = 1; i <= count; i++)
                {
                    string us = pref + i;
                    string pass = GetPass(lenghtpass);
                    string db = dbpref + i;

                    ServiceSQL.CreateDataBase(db, cs); // создание дб
                    ServiceSQL.CreateUser(us, db, pass, cs); // создание юзеров

                    list.Add(new User
                    {
                        Name = us,
                        Login = us,
                        Password = pass,
                        NameDB = db
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
        public static string GetPass(int lenghtpass)
        {
            int[] arr = new int[lenghtpass];
            Random rand = new Random();
            string pass = "";

            for(int i = 0;i < arr.Length; i++)
            {
                arr[i] = rand.Next(33, 125);
                pass += (char)arr[i];
            }
            return pass;
        }

    }
}
