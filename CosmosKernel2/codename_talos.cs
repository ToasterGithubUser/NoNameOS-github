using System;
using System.Text;
using System.IO;
using System.Linq;
//06.01.2025: temporary password security method
namespace CosmosKernel1
{
    public static class TalosSecurity
    {
        

        public static string Encrypt(string input)
        {

            byte xorConstant = 0x53;

            
            byte[] data = Encoding.UTF8.GetBytes(input);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            string output = Convert.ToBase64String(data);
            return output;

        }
        public static string Decrypt(string input) {
            byte xorConstant = 0x53;
            byte[] data = Convert.FromBase64String(input);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            string plainText = Encoding.UTF8.GetString(data);
            return plainText;
        }
    }
}