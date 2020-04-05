using System;
using System.Text;

namespace ExchangeApiService.Helper
{
    public class Base64Helper
    {
        public static string Encode(string stringValue)
        {
            byte[] data = Encoding.ASCII.GetBytes(stringValue);
            return Convert.ToBase64String(data);
        }

        public static string Decode(string encodedValue)
        {
            byte[] data = Convert.FromBase64String(encodedValue);
            return Encoding.ASCII.GetString(data);
        }
    }
}
