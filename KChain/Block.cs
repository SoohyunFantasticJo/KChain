using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KChain
{
    public class Transaction
    {
        public DateTime createdTime = DateTime.UtcNow;
        public Byte[] data;
    }

    public class Block
    {
        public Transaction[] transactions;
        public DateTime createdTime;

    }
}
