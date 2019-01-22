using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KChainExecuter
{
    class Program
    {
        static void Main(string[] args)
        {
            String a = "1234567890";
            String b = "1234567890";
            SHA256 sha = SHA256Managed.Create();

            Console.WriteLine(Encoding.Default.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(a))));
            Console.WriteLine(Encoding.Default.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(b))));
        }
    }
}
