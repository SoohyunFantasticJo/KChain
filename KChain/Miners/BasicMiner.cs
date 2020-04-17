using KChain.BlockChain;
using KChain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KChain.Miners
{
    public class BasicMiner
    {
        public static bool Run(Chain chain)
        {
            chain.Init();
            Block prevBlock = chain.GetFirstBlock();

            Block newBlock = new Block(chain.GetIndex() <= 0 ? null : Encoding.UTF8.GetBytes(prevBlock.GetBlockHash()), chain.GetCurrentBlock().GetBlockBody().Transactions);



            //while (true)
            //{
            //prevBlock
            //}

            return true;
        }

        public static bool FindNonce(Block newBlock)
        {
            int nonce = 0;
            string hashResult = string.Empty;

            for(int difficultyBits = 0; difficultyBits < 32; difficultyBits++)
            {
                double difficulty = Math.Pow(2, difficultyBits);

                Console.WriteLine("Difficulty: {0}({1} bits)", difficulty, difficultyBits);
                Console.WriteLine("Starting search...");

                DateTime start = DateTime.Now;

                //Block newBlock = new Block(chain.GetIndex() <= 0 ? null : Encoding.UTF8.GetBytes(newBlock.GetBlockHash()), chain.GetCurrentBlock().GetBlockBody().Transactions);
            }

            return true;
        }

        public static int ProofOfWork(Block header, int difficultyBits)
        {
            return 0;
        }

        /*
        public static bool Run(Chain chain)
        {
            chain.Init();
            Block newBlock = chain.GetFirstBlock();

            while (true)
            {
                newBlock = new Block(chain.GetIndex() <= 0 ? null : Encoding.UTF8.GetBytes(newBlock.GetBlockHash()), chain.GetCurrentBlock().GetBlockBody().Transactions);
                int nonce = 0;
                string hashResult = BasicMiner.FindNonce(chain.GetCurrentBlock(), newBlock, out nonce);

                if (hashResult != null)
                {
                    Console.WriteLine("Success with nonce {0}", nonce);
                    Console.WriteLine("Hash is {0}", hashResult);
                }
                else
                {
                    Console.WriteLine("Failed after {0} (max_nonce) tries", nonce);
                }

                Console.WriteLine();

                if(!chain.Next())
                {
                    break;
                }
            }

            return true;
        }

        private static string FindNonce(Block orgBlock, Block newBlock, out int res)        {
            byte[] orgHash = Converters.StringToByteArray(orgBlock.GetBlockHash());
            string hashResult = null;
            int nonce = 0;

            for (nonce = 0; nonce < Block.MaxNonce; nonce++)
            {
                newBlock.SetNonce(nonce);

                byte[] newHash = Converters.StringToByteArray(newBlock.GetBlockHash());

                if (Converters.ByteArrayToInt64(newHash) < Converters.ByteArrayToInt64(orgHash))
                {
                    hashResult = newBlock.GetBlockHash();

                    break;
                }
            }

            res = nonce;
            return hashResult;
        }
        */
    }
}