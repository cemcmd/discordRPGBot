using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace textAdventure
{
    class Admin
    {
        private string HashedKey;

        public void CreateKey()
        {
            string a = GenAuthKey();
            Console.WriteLine(a);
            HashedKey = Hash(a);
            a = "";
        }

        public bool IsAdmin(string key)
        {
            if (Hash(key) != HashedKey)
                return false;
            else
                return true;
        }

        private static string GenAuthKey()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());

            int e;

            string key = "";

            for (int i = 0; i < 32; i++)
            {
                e = r.Next(1, 4);
                switch (e)
                {
                    case 1:
                        key += (char)r.Next(48, 57);
                        break;
                    case 2:
                        key += (char)r.Next(65, 90);
                        break;
                    case 3:
                        key += (char)r.Next(97, 122);
                        break;
                }
            }

            return key;
        }

        private static string Hash(string authkey)
        {
            HashAlgorithm algorithm = SHA256.Create();
            authkey += Environment.UserName; // Salted
            var a = algorithm.ComputeHash(Encoding.UTF8.GetBytes(authkey));

            string sb = "";

            foreach (byte b in a)
            {
                sb += b.ToString("X2");
            }

            return sb;
        }
    }
}
