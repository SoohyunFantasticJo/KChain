using KChain.Utils;
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
        private static int _maxNonce = 10000000;
        public static int MaxNonce
        {
            get { return _maxNonce; }
        }

        private static int _defaultDifficulty = 5;
        public static int DefaultDifficulty
        {
            get { return _defaultDifficulty; }
        }

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

            private DateTime _timeStamp;
            public DateTime TimeStamp
            {
                get { return _timeStamp; }
                set { _timeStamp = value; }
            }

            private int _difficulty;
            public int Difficulty
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
                byte[] bytes = Converters.ObjectToByteArray((BlockHeader)this);

                SHA256Managed h = new SHA256Managed();
                using (SHA256Managed hashstring = new SHA256Managed())
                {
                    byte[] hash = hashstring.ComputeHash(bytes);

                    return Converters.ByteArrayToString(hash);
                }
            }

            public static bool isValidNonce(string hash, int difficulty)
            {
                int emptyBitsLength = hash.Substring(0, difficulty).Replace("0", "").Length;

                return emptyBitsLength <= 0 ? true : false;
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

            public int CalculateNonce(BlockHeader blockHeader)
            {
                int nonce = 0;
                while(true)
                {
                    blockHeader.Nonce = nonce;

                    if (BlockHeader.isValidNonce(blockHeader.GetBlockHash(), _difficulty))
                    {
                        break;
                    }

                    nonce++;

                    if(nonce > _maxNonce)
                    {
                        break;
                    }
                }

                return nonce;
            }

            public BlockHeader(byte[] preBlockHash, List<Transaction> transactions)
            {
                _preBlockHash = preBlockHash;
                _merkleRootHash = transactions.GetHashCode();
                _timeStamp = DateTime.Now;
                _difficulty = _defaultDifficulty;

                _nonce = CalculateNonce(this);

                Console.WriteLine(_nonce);
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
        private BlockHeader Header
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
        private BlockBody Body
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
            _header = new BlockHeader(preBlockHash, transactions);
            _body = new BlockBody(_header, transactions);
        }

        public string GetBlockHash()
        {
            return _header.GetBlockHash();
        }

        public bool SetNonce(int nonce)
        {
            _header.Nonce = nonce;

            return true;
        }

        public BlockBody GetBlockBody()
        {
            return _body.GetBlock();
        }

        public int ProofOfWorkCount()
        {
            return _header.ProofOfWorkCount();
        }
    }
}
