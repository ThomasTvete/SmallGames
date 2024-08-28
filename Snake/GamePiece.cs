using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class GamePiece
    {
       public int X;
        public int Y;
        public char Symbol;

        public GamePiece(int x, int y, char symbol)
        {
            X = x;
            Y = y;
            Symbol = symbol;
        }
       
    }
}
