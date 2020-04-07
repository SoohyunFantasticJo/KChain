using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KChain.BlockChain
{
    public class Block
    {
        [Serializable]
        public class BlockHeader : ICloneable
        {
            private double _version;
            public double Version
            {
                get { return _version; }
                set { _version = value; }
            }

            private byte[] _preBlockHash;
            public byte[] PreBlockHash
            {
                get { return _preBlockHash; }
                set { _preBlockHash = value; }
            }

            private int _merkleRootHash;
            public int MerkelRootHash
            {
                get { return _merkleRootHash; }
                set { _merkleRootHash = value; }
            }

            private int _timeStamp;
            public int TimeStamp
            {
                get { return _timeStamp; }
                set { _timeStamp = value; }
            }

            private uint _difficulty;
            public uint Difficulty
            {
                get { return _difficulty; }
                set { _difficulty = value; }
            }

            private int _nonce;
            public int Nonce
            {
                get { return _nonce; }
                set { _nonce = value; }
            }

            public int ProofOfWorkCount()
            {
                BlockHeader sampleHdr = (BlockHeader)((BlockHeader)this).Clone();// (BlockHeader)this;
                sampleHdr.Nonce = 0;

                using (SHA256Managed hashstring = new SHA256Managed())
                {
                    while (((BlockHeader)this).GetBlockHash() != sampleHdr.GetBlockHash())
                    {
                        sampleHdr.Nonce += 1;

                        //Console.WriteLine("{0}: {1}", sampleHdr.Nonce, sampleHdr.GetBlockHash());
                    }

                    return sampleHdr.Nonce;
                }
            }

            public string GetBlockHash()
            {
                byte[] bytes = ((BlockHeader)this).ObjectToByteArray();

                SHA256Managed h = new SHA256Managed();
                using (SHA256Managed hashstring = new SHA256Managed())
                {
                    byte[] hash = hashstring.ComputeHash(bytes);

                    return ByteArrayToString(hash);
                }
            }

            public byte[] ObjectToByteArray()
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, (object)this);
                    return ms.ToArray();
                }
            }

            public string ByteArrayToString(byte[] bts)
            {
                StringBuilder strBld = new StringBuilder();
                foreach (byte bt in bts)
                {
                    strBld.AppendFormat("{0:X2}", bt);
                }

                return strBld.ToString();
            }
            public object Clone()
            {
                BlockHeader source = (BlockHeader)this;
                BlockHeader clone = new BlockHeader(source.PreBlockHash, new List<Transaction>());

                clone.Version = source.Version;
                clone.MerkelRootHash = source.MerkelRootHash;
                clone.TimeStamp = source.TimeStamp;
                clone.Difficulty = source.Difficulty;

                return (Object)clone;
            }

            public BlockHeader(byte[] preBlockHash, List<Transaction> transactions)
            {
                _preBlockHash = preBlockHash;
                _merkleRootHash = transactions.GetHashCode();

                Random rnd = new Random(DateTime.Now.Millisecond);
                _nonce = rnd.Next(0, 9999); // 추후에 0~난이도로 조정할 것
            }
        }

        [Serializable]
        public class BlockBody
        {
            private int _size;
            public int Size
            {
                get { return _size; }
                set { _size = value; }
            }

            private BlockHeader _header;
            public BlockHeader Header
            {
                get { return _header; }
                set { _header = value; }
            }

            private int _count;
            public int Count
            {
                get { return _count; }
                set { _count = value; }
            }

            private List<Transaction> _transactions;
            public List<Transaction> Transactions
            {
                get
                {
                    if (_transactions == null)
                    {
                        _transactions = new List<Transaction>();
                    }
                    return _transactions;
                }
                set { _transactions = value; }
            }

            public BlockBody(BlockHeader header, List<Transaction> transactions)
            {
                _header = header;
                _transactions = transactions;
            }

            public BlockBody GetBlock()
            {
                return this;
            }
        }

        private BlockHeader _header;
        public BlockHeader Header
        {
            get
            {
                if (_header == null)
                {
                    _header = new BlockHeader(null, null);
                }
                return _header;
            }
            set
            {
                _header = value;
            }
        }

        private BlockBody _body;
        public BlockBody Body
        {
            get
            {
                if (_body == null)
                {
                    _body = new BlockBody(null, null);
                }
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        public Block(byte[] preBlockHash, List<Transaction> transactions)
        {
            Header = new BlockHeader(preBlockHash, transactions);
            Body = new BlockBody(Header, transactions);
        }

        public string GetBlockHash()
        {
            return Header.GetBlockHash();
        }

        public BlockBody GetBlock()
        {
            return Body.GetBlock();
        }

        public int ProofOfWorkCount()
        {
            return Header.ProofOfWorkCount();
        }
    }
}
