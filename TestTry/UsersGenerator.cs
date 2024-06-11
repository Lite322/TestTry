using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTry
{
    public class UsersGenerator
    {
        public static List<string> Add(string pref, string dbpref, int count, int lenghtpass, string cs)
        {

            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i <= count; i++)
                {
                    string us = pref + i;
                    string pass = GetPass(lenghtpass);
                    string db = dbpref + i;

                    ServiceSQL
                }
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
