using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KChain_Test
{
    class Program
    {
        [Serializable]
        class Node
        {
            public string from = "127.0.0.1";
            public string to = "127.0.0.2";
        }

        static void Main(string[] args)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            Node node = new Node();
            Node node2 = new Node();

            byte[] result = sha.ComputeHash(ToByteArray((object)node));
            byte[] result2 = sha.ComputeHash(ToByteArray((object)node2));

            Console.WriteLine(System.Text.Encoding.Default.GetString(result));
            Console.WriteLine(System.Text.Encoding.Default.GetString(result2));
        }

        private static byte[] ToByteArray(object source)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                return stream.ToArray();
            }
        }
    }
}
