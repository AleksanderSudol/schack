using System;
using System.Collections.Generic;
using System.Text;

namespace schack
{
    class Knight : Pawn
    {
        private bool _canCastle;

        public bool canCastle
        {
            get { return _canCastle; }
            set { _canCastle = value; }
        }
    }
}
