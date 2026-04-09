using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace schack
{
    class Rook : Piece
    {
        public Rook(string color, int row, int col)
            : base(color, row, col)
        {
        }

        public override string Symbol
        {
            get
            {
                // Om färgen är vit så används vita unicode, annars den svarta
                return Color == "White" ? "♖" : "♜";
            }
        }

        public override bool IsValidMove(int newRow, int newCol, Piece[,] board)
        {
            
            if (Row != newRow && Col != newCol)
            {
                return false;
            }

            // Math.Sign ger värdet 1, -1 eller 0, vilket är bra för att inte hoppa över andra pjäser
            int rowDiff = Math.Sign(newRow - Row);
            int colDiff = Math.Sign(newCol - Col);

           
            int currentRow = Row + rowDiff;
            int currentCol = Col + colDiff;

           
            while (currentRow != newRow || currentCol != newCol)
            {
                
                if (board[currentRow, currentCol] != null)
                {
                    return false;
                }

                // Ta nästa steg
                currentRow += rowDiff;
                currentCol += colDiff;
            }

           //förhindrar att ta ut sin egna pjäs
            Piece targetPiece = board[newRow, newCol];
            if (targetPiece != null && targetPiece.Color == this.Color)
            {
                return false;
            }

           
            return true;
        }
    }
}