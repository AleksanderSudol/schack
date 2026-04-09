using System;
using System.Collections.Generic;
using System.Text;

namespace schack
{
    class Knight : Piece
    {
        public Knight(string color, int row, int col)
            : base(color, row, col)
        {
        }

        public override string Symbol
        {
            get
            {
                return Color == "White" ? "♘" : "♞";
            }
        }

        public override bool IsValidMove(int newRow, int newCol, Piece[,] board)
        {
            int rowDiff = Math.Abs(newRow - Row);
            int colDiff = Math.Abs(newCol - Col);

            bool isValidLShape = (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);

            if (!isValidLShape)
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