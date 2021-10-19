using System;
using System.Security.Cryptography;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public class RandomItemGenerators
    {
        public static string GenerateBorrowerId()
        {
            var milisecndns = string.Format("{0:000}", DateTime.Now.Millisecond);
            var year = DateTime.Now.ToString("yy");
            var month = string.Format("{0:00}", DateTime.Now.Month);
            var day = (RandomNumberGenerator.GetInt32(100, 999)).ToString();
            return $"{year}{month}{milisecndns}{day}";             
        }

    }
}
