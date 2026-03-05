using System;
using System.Collections.Generic;
using System.Text;

namespace schack
{
    class Bishop : Piece
    {
        public Bishop(string color, int row, int col)
            : base(color, row, col)
        {
        }
        public override string Symbol
        {
            get
            {
                // en enklare if else sats, om färgen är vit så används vita unicode, annars används svarta unicode symbolen
                return Color == "White" ? "♙" : "♟";
            }
        }

        public override bool IsValidMove(int newRow, int newCol, Piece[,] board)
        {

            return true;
        }
    }
}
