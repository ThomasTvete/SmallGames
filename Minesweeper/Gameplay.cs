using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Minesweeper;

public abstract class Gameplay
{
    public bool GameOver;
    protected abstract Tile[,] Map { get; set; }

    protected List<(int, int)> FindNeighbours(int i, int j)
    {
        return new List<(int, int)>
        {
            ( i-1, j-1 ), ( i-1, j ), ( i-1, j+1 ), ( i, j-1 ), ( i, j+1 ), ( i+1, j-1 ), ( i+1, j ), ( i+1, j+1 )
        };
    }

    public void Input()
    {
        //string input = Console.ReadLine();
        string[] input = Console.ReadLine().Trim().Split(' ');
        char action = char.ToLower(Convert.ToChar(input[input.Length - 1]));




            if (int.TryParse(input[0], out int x) && int.TryParse(input[1], out int y))
            {
                if (action == 'r') Reveal(x, y);
                else if (action == 'f') PlaceFlag(x, y);

                if (Victory() && !GameOver)
                {
                    GameOver = true;
                    RevealMap();
                    Messages.Winner();
                }
            }
    }

    protected void Reveal(int x, int y)
    {
        Tile tile;
        if (Map[x, y] is Tile) tile = Map[x, y];
        else return;

        if(!tile.Flagged) tile.Revealed = true;

        if (tile.Mined)
        {
            GameOver = true;
            RevealMap();
            Messages.Loser();
        }

        else if (tile.NearMines == 0)
        {
            List<(int, int)> neighbours = FindNeighbours(x, y);

            foreach (var (i, j) in neighbours)
            {
                if(Map[i, j] is Tile && !Map[i, j].Revealed) Reveal(i, j);
            }
        };

    }

    protected void PlaceFlag(int x, int y)
    {
        if (Map[x, y] is Tile) Map[x, y].Flagged = !Map[x, y].Flagged;
    }

    protected abstract void RevealMap();

    protected bool Victory()
    {
        foreach (var tile in Map)
        {
            if (!(tile is Tile)) continue;

            if (!tile.Mined && !tile.Revealed) return false;

        }
        return true;
    }

}