using CustomTokenBasedAuthentication.Business.Enums;
using CustomTokenBasedAuthentication.Business.Models;
using System;
using System.Security.Cryptography;

namespace CustomTokenBasedAuthentication.Business.Helpers
{
    public static class StaticHelpers
    {
        public static ResponseDetails SetResponSeDetails(bool success, object data, MessageType messageType,
            string message = "Exception Encountered") => new ResponseDetails
            {
                Success = success,
                Data = data,
                Message = message,
                MessageType = messageType
            };

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != passwordHash[i]) return false;
            }
            return true;
        }

        public static string EncryptToken(string token)
        {
            char[] ans = new char[token.Length];

            for (int i = 0; i < token.Length; i++)
            {
                int temp = GetASCIIValue(token[i]);
                temp += i;

                ans[i] = GetCharFromASCII(temp);
            }
            return new string(ans);
        }

        public static string DecryptToken(string token)
        {
            char[] ans = new char[token.Length];

            for (int i = 0; i < token.Length; i++)
            {
                int temp = GetASCIIValue(token[i]);
                temp -= i;
                if (temp < 300 && temp > 0)
                    ans[i] = GetCharFromASCII(temp);
            }

            return new string(ans);
        }

        private static char GetCharFromASCII(int ch) => Convert.ToChar(ch);

        private static int GetASCIIValue(char ch) => Convert.ToInt32(ch);
    }
}
