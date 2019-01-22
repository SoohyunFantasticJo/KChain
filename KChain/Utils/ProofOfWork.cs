using KChain.BlockChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KChain.Utils
{
    public class ProofOfWork
    {
        public static bool RunProofOfWork(Block firstBlock)
        {
            using (SHA256Managed hashstring = new SHA256Managed())
            {
                byte[] bt;
                string sHash = firstBlock.GetBlockHash();
                int nonce = 0;
                while (true)
                {
                    Block block = firstBlock;
                    block.Header.Nonce = nonce;

                    if(sHash == block.GetBlockHash())
                    {
                        return true;
                    }

                    nonce++;
                }

                return false;
            }
        }
    }
}
