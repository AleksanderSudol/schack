using System;
using System.Collections.Generic;
using System.Text;

namespace schack
{
    class Pawn : Piece
    {
        public Pawn(string color, int row, int col)
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
            int rowDiff = newRow - Row;
            // math.Abs eftersom bonden ska kunna ta ut pjäser diagonalt både åt vänster och höger
            int colDiff = Math.Abs(newCol - Col);
            int direction = (Color == "White") ? -1 : 1;
            int startRow = (Color == "White") ? 6 : 1;

            // kollar om det är en tom ruta
            if (colDiff == 0 && board[newRow, newCol] == null)
            {
                
                if (rowDiff == direction)
                {
                    return true;
                }
                // kan gå två steg fram om det är första draget
                if (Row == startRow && rowDiff == 2 * direction && board[Row + direction, Col] == null)
                {
                    return true;
                }
            }

            // går diagonalt om det finns en fiende där
            if (colDiff == 1 && rowDiff == direction)
            {
                // kollar om det finns en fiende där, om det inte är null och har en annan färg än den som flyttar
                if (board[newRow, newCol] != null && board[newRow, newCol].Color != this.Color)
                {
                    return true;
                }
            }

            return false; 
        }
    }
}



 




    