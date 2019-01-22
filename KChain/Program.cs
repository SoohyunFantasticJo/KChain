using KChain.BlockChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KChain
{
    class Program
    {
        static void Main(string[] args)
        {
            Transaction transaction = new Transaction();
            transaction.From = "Bob";
            transaction.To = "SJ";
            transaction.Data = "sample.txt";

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(transaction);

            Block firstBlock = new Block(null, transactions);
            Console.WriteLine("Block Hash : {0}", firstBlock.GetBlockHash());
            Console.WriteLine("Count of Proof of Work of 1st Block : {0}", firstBlock.ProofOfWorkCount());

            transactions = new List<Transaction>();
            transaction.From = "SJ";
            transaction.To = "Jhon";
            transaction.Data = "resume.doc";
            transactions.Add(transaction);

            Block preBlock = firstBlock;
            Block nextBlock = new Block(Encoding.UTF8.GetBytes(preBlock.GetBlockHash()), transactions);

            Console.WriteLine("2nd Block Hash : {0}", nextBlock.GetBlockHash());
            Console.WriteLine("Count of Proof of Work of 2nd Block : {0}", nextBlock.ProofOfWorkCount());

            Console.ReadKey();
        }
    }
}
