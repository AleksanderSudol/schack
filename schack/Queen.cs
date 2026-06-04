using System;
using System.Collections.Generic;
using System.Text;

namespace schack
{
    class Queen : Piece
    {
        public Queen(string color, int row, int col)
            : base(color, row, col)
        {
        }
        public override string Symbol
        {
            get
            {
                // en enklare if else sats, om färgen är vit så används vita unicode, annars används svarta unicode symbolen
                return Color == "White" ? "♕" : "♛";
            }
        }

        public override bool IsValidMove(int newRow, int newCol, Piece[,] board)
        {

            //abs för att kolla om det är ett diagonalt drag och sign för att kolla riktning
            int rowDiff = Math.Abs(newRow - Row);
            int colDiff = Math.Abs(newCol - Col);

            bool isDiagonal = (rowDiff == colDiff);
            bool isStraight = (Row == newRow || Col == newCol);

            if (!isDiagonal && !isStraight)
            {
                return false;
            }

            int rowStep = Math.Sign(newRow - Row);
            int colStep = Math.Sign(newCol - Col);


            int currentRow = Row + rowStep;
            int currentCol = Col + colStep;


            while (currentRow != newRow || currentCol != newCol)
            {
                // kollar så att den inte hoppar över pjäser
                if (board[currentRow, currentCol] != null)
                {
                    return false;
                }

                //både row och column måste ändras för att gå diagonalt
                currentRow += rowStep;
                currentCol += colStep;
            }

            // Förhindrar att ta ut sin egna pjäs
            Piece targetPiece = board[newRow, newCol];
            if (targetPiece != null && targetPiece.Color == this.Color)
            {
                return false;
            }


            return true;
        }
    }
}

