using System;
using System.Collections.Generic;
using System.Text;

namespace schack
{
    class Pawn
    {
        // färg för att inte kunna ta sina egna pjäser, legalMove för att begränsa dragen för olika pjäser och position för att kunna hålla koll på var pjäserna är på brädet
        private string _color;
        private bool _legalMove;
        private string _position;
 



        public string color
        {
            get { return _color; }
            set { _color = value; }
        }
        public bool legalMove
        {
            get { return _legalMove; }
            set { _legalMove = value; }
        }
        public string position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
    