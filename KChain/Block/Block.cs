using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KChain.Block
{
    public class Block
    {
        public class Transaction
        {
            private string _from;
            public string From
            {
                get { return _from; }
                set { _from = value; }
            }

            private string _to;
            public string To
            {
                get { return _to; }
                set { _to = value; }
            }

            private string _data;
            public string Data
            {
                get { return _data; }
                set { _data = value; }
            }
        }

        public class Header
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

            private static uint _difficultyTarget = 5;
            public static uint DifficultyTarget
            {
                get { return _difficultyTarget; }
                set { _difficultyTarget = value; }
            }

            private static int _nonce;
            public static int Nonce
            {
                get { return _nonce; }
                set { _nonce = value; }
            }

            public int ProofOfWorkCount()
            {
                using (SHA256Managed hashstring = new SHA256Managed())
                {
                    byte[] bt;
                    string sHash = string.Empty;
                    while (sHash == string.Empty || sHash.Substring(0, (int)_difficultyTarget) != ("").PadLeft((int)_difficultyTarget, '0'))
                    {
                        bt = Encoding.UTF8.GetBytes(_merkleRootHash + _nonce.ToString());
                        sHash = Body.ByteArrayToString(hashstring.ComputeHash(bt));
                        _nonce++;
                    }
                    return _nonce;
                }
            }

            public byte[] toByteArray()
            {
                string tmpStr = "";

                if (_preBlockHash != null)
                {
                    tmpStr += Convert.ToBase64String(_preBlockHash);
                }
                tmpStr += _merkleRootHash.ToString();
                return Encoding.UTF8.GetBytes(tmpStr);
            }

            public Header(byte[] preBlockHash, List<Transaction> transactions)
            {
                _preBlockHash = preBlockHash;
                _merkleRootHash = transactions.GetHashCode();
            }
        }

        public class Body
        {
            private int _size;
            public int Size
            {
                get { return _size; }
                set { _size = value; }
            }

            private Header _header;
            public Header Header
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
                    if(_transactions == null)
                    {
                        _transactions = new List<Transaction>();
                    }
                    return _transactions;
                }
                set { _transactions = value; }
            }

            public string GetBlockHash()
            {
                byte[] bytes = _header.toByteArray();
                using (SHA256Managed hashstring = new SHA256Managed())
                {
                    byte[] hash = hashstring.ComputeHash(bytes);
                    hash = hashstring.ComputeHash(hash);

                    return ByteArrayToString(hash);
                }
            }

            public Body(Header header, List<Transaction> transactions)
            {
                _header = header;
                _transactions = transactions;
            }

            public static string ByteArrayToString(byte[] bts)
            {
                StringBuilder strBld = new StringBuilder();
                foreach (byte bt in bts)
                    strBld.AppendFormat("{0:X2}", bt);

                return strBld.ToString();
            }

            public Body GetBlock()
            {
                return this;
            }
        }
    }
}
