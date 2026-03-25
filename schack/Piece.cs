using System;
using System.Collections.Generic;
using System.Text;

namespace schack
{
    abstract class Piece
    {
        public string Color { get; }
        public int Row { get; set; }
        public int Col { get; set; }

        public abstract string Symbol { get; }

        //protected eftersom klassen är abstract och andra klasser ska ärva från den så funkar inte private.
        protected Piece(string color, int row, int col)
        {
            Color = color;
            Row = row;
            Col = col;
        }
        //[,] eftersom det är en 2d array.  
        public abstract bool IsValidMove(int newRow, int newCol, Piece[,] board);
    }
}
