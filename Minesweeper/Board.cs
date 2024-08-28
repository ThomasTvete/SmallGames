using Microsoft.VisualBasic.CompilerServices;

namespace Minesweeper;

public class Board : Gameplay
{
    private int _width;
    private int _height;
    private int _mines;
    protected override Tile[,] Map { get; set; }

    public Board(int size, int mines)
    {
        _width = size;
        _height = size;
        _mines = mines;

        InitBoard();
        SpawnMines();
        SpawnNumbers();

    }



    public Board(int height, int width, int mines)
    {
        _width = width;
        _height = height;
        _mines = mines;

        InitBoard();
        SpawnMines();
        SpawnNumbers();
    }

    private void InitBoard()
    {
        Map = new Tile[_height+2, _width+2];

        for (int i = 1; i <= _height; i++)
        {
            for (int j = 1; j <= _width; j++)
            {
                Map[i, j] = new Tile();

            }
        }
    }

    private void SpawnMines()
    {
        Random rnd = new Random();
        int usedMines = 0;

        while (usedMines < _mines)
        {
            int i = rnd.Next(_height) + 1;
            int j = rnd.Next(_width) + 1;
           Tile tile = Map[i, j];

            if (!tile.Mined)
            {
                tile.Mined = true;
                usedMines++;
            }
            
        }
    }

    private void SpawnNumbers()
    {
        for (int i = 1; i <= _height; i++)
        {
            for (int j = 1; j <= _width; j++)
            {
                Tile tile = Map[i, j];

                if(tile.Mined) continue;

                tile.NearMines = CalcNumber(i, j);



            }
        }
    }

    private int CalcNumber(int i, int j)
    {
        int number = 0;
        List<(int, int)> neighbours = FindNeighbours(i, j);

        foreach (var (x, y) in neighbours)
        {
            if (Map[x, y] is Tile && Map[x, y].Mined)
            {
                number++;
            }
        }

        return number;
    }

    protected override void RevealMap()
    {
        foreach (var tile in Map)
        {
            if (tile is Tile) tile.Revealed = true;
        }

        Print();
    }

    public void Print()
    {
        string number;
        Console.Clear();
        Console.SetCursorPosition(2, 0);
        for (int column = 1; column <= _width; column++)
        {
           
            Console.Write((column).ToString("00").PadLeft(3));
        }
        Console.WriteLine();

        for (int i = 1; i <= _height; i++)
        {

            Console.Write(i.ToString("00"));
            for (int j = 1; j <= _width; j++)
            {
                Tile tile = Map[i, j];
                string print;
                if (tile.Revealed && tile.Mined) print = tile.Mine.ToString();
                else if (tile.Revealed) print = tile.NearMines.ToString();
                else if (tile.Flagged) print = tile.Flag.ToString();
                else print = tile.Default.ToString();
                Console.Write(print.PadLeft(3));
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        Messages.Instructions();
    }

}

