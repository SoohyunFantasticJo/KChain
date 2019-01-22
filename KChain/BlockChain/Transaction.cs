using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KChain.BlockChain
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
}
