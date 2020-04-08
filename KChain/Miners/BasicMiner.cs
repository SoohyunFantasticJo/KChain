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

        private static string FindNonce(Block orgBlock, Block newBlock, out int res)
        {
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
    }
}