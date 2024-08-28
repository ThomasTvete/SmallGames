namespace Minesweeper;

public class Tile
{
    public bool Mined;
    public bool Revealed;
    public bool Flagged;
    public int NearMines;
    public char Default = '#';
    public char Flag = '?';
    public char Mine = '*';

    public Tile()
    {
        Mined = false;
        Revealed = false;
        Flagged = false;
        NearMines = 0;

    }
}