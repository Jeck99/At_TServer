using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AT_T_31_10.Utils
{
    public class Encryption
    {
        public const int SaltSize = 24;
        public const int HashSize = 24;
        public const int PBKDF_Itteration= 24;

        public string CreateHash (string password)
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltSize];
            csprng.GetBytes(salt);

            byte[] hash = PBKDF2(password, salt, PBKDF_Itteration, HashSize);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        private byte[] PBKDF2(string password, byte[] salt, int pBKDF_Itteration, int OutputBytes)
        {

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount=pBKDF_Itteration;

            return pbkdf2.GetBytes(OutputBytes);

        }


        private bool SlowEqual(byte [] dbHash ,byte [] passHash)
        {

            uint diff = (uint)dbHash.Length ^ (uint)passHash.Length;

            for (int i = 0; i < dbHash.Length && i < passHash.Length; i++)
                diff |= (uint)dbHash[i] ^ (uint)passHash[i];

            return diff == 0;
        }

        public bool ValidatePassword(string password , string dbHash)
        {
            char[] delimiter = { ':' };
            string[] split = dbHash.Split(delimiter);
            byte[] salt = Convert.FromBase64String(split[0]);
            byte[] hash = Convert.FromBase64String(split[1]);

            byte[] HashToValidate = PBKDF2(password, salt, PBKDF_Itteration, hash.Length);

            return SlowEqual(hash, HashToValidate);
        }


    }
}