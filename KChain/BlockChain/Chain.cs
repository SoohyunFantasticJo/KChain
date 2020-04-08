using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KChain.BlockChain
{
    public class Chain
    {
        private int _index = 0;
        public int Index
        {
            get { return _index; }
            set { _index = value;  }
        }

        private List<Block> _blocks = new List<Block>();
        public List<Block> Blocks
        {
            get { return _blocks; }
            set { _blocks = value; }
        }

        public bool Add(Block block)
        {
            _blocks.Add(block);

            return true;
        }

        public Block GetFirstBlock()
        {
            if (_blocks.Count <= 0)
            {
                return null;
            }

            return _blocks.First();
        }

        public Block GetLastBlock()
        {
            if(_blocks.Count <= 0)
            {
                return null;
            }

            return _blocks.Last();
        }

        public Block GetCurrentBlock()
        {
            if (_blocks.Count <= 0)
            {
                return null;
            }

            return _blocks[_index];
        }

        public bool Init()
        {
            _index = 0;

            return true;
        }

        public int GetIndex()
        {
            return _index;
        }

        public int GetCount()
        {
            return _blocks.Count();
        }

        public bool Next()
        {
            if (_index < _blocks.Count - 1)
            {
                _index = _index + 1;
                return true;
            }

            return false;
        }

        public bool Prev()
        {
            if(_index > 0)
            {
                _index = _index - 1;
                return true;
            }

            return false;
        }
    }
}
