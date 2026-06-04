using System;
using System.Collections.Generic;
using System.Text;

namespace schack
{
    class King : Piece
    {
        public King(string color, int row, int col)
            : base(color, row, col)
        {
        }
        public override string Symbol
        {
            get
            {
                // en enklare if else sats, om färgen är vit så används vita unicode, annars används svarta unicode symbolen
                return Color == "White" ? "♔" : "♚";
            }
        }

        public override bool IsValidMove(int newRow, int newCol, Piece[,] board)
        {
            int rowDiff = Math.Abs(newRow - Row);
            int colDiff = Math.Abs(newCol - Col);


            if (rowDiff > 1 || colDiff > 1)
            {
                return false; 
            }

 
            Piece targetPiece = board[newRow, newCol];
            if (targetPiece != null && targetPiece.Color == this.Color)
            {
                return false;
            }


            return true;
        }
    }
}