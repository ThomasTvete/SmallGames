namespace Snake;

public class Board
{
    private int _width;
    private int _height;
    
    public char[,] _board;
    private int _moveX;
    private int _moveY;
    public Random Rnd = new Random();
    private Head _head;
    private List<GamePiece> _snake;
    private Snack _snack;
    private bool _yummy;
    private int _score;
    public bool GameOver;

    public Board(int width, int height)
    {
        _width = width;
        _height = height;
        InitWall();
        InitSnake();
        SpawnSnack();
    }

    private void InitWall()
    {
        _board = new char[_height, _width];


        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (x == 0 || x == _width - 1 || y == 0 || y == _height - 1)
                {
                    _board[y, x] = '|';
                }
                else _board[y, x] = ' ';
            }
        }
    }

    private void InitSnake()
    {
        _snake = new List<GamePiece>();
        _head = new Head(_width / 2, 
            _height / 2);
        _score = 0;
        _moveX = 1;
        _moveY = 0;
        _snake.Add(_head);

        UpdatePiece(_head);
    }


    public void UpdatePiece(GamePiece piece)
    {
        if (piece.X >= 0 && piece.X < _width && piece.Y >= 0 && piece.Y < _height)
        {
            _board[piece.Y, piece.X] = piece.Symbol;
        }
    }

    public void SpawnSnack()
    {
        int x;
        int y;

        do
        {
            x = Rnd.Next(1, _width - 1);
            y = Rnd.Next(1, _height - 1);
        } while (_board[y, x] != ' ');

        _snack = new Snack(x, y);

        UpdatePiece(_snack);
    }

    public void Draw()
    {
        for (int y = 0; y < _height; y++)
        {
            Console.SetCursorPosition(10, y + 10);
            for (int x = 0; x < _width; x++)
            {
                Console.Write(_board[y, x]);
                
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Poeng: {_score}");
    }

    public void Input()
    {
        if (!Console.KeyAvailable) return;

        ConsoleKey input = Console.ReadKey().Key;

        switch (input)
        {
            case ConsoleKey.W:
                if (_moveY != 1) { _moveX = 0; _moveY = -1; }
                break;
            case ConsoleKey.S:
                if (_moveY != -1) { _moveX = 0; _moveY = 1; }
                break;
            case ConsoleKey.A:
                if (_moveX != 1) { _moveX = -1; _moveY = 0; }
                break;
            case ConsoleKey.D:
                if (_moveX != -1) { _moveX = 1; _moveY = 0; }
                break;

        }
    }

    public void Update()
    {
        Head newHead = new Head(_head.X + _moveX, _head.Y + _moveY);

        GameOverCheck(newHead);
        SnackCheck(newHead);
        MoveCheck(newHead);
    }

    private void GameOverCheck(Head head)
    {
        if (head.X == 0
            || head.X == _width - 1
            || head.Y == 0
            || head.Y == _height - 1)
        {
            GameOver = true;
        }

        else 

        {
            foreach (GamePiece segment in _snake)
            {
                if (segment.X == head.X && segment.Y == head.Y)
                {
                    GameOver = true;
                }
            }
        }
    }

    private void SnackCheck(Head head)
    {
        

        if (head.X == _snack.X && head.Y == _snack.Y)
        {
            _score++;
            //new Body(lastSegment.)
            _yummy = true;
            SpawnSnack();
        }

    }

    private void MoveCheck(GamePiece newHead)
    {
        int newX = newHead.X;
        int newY = newHead.Y;


        


        foreach (GamePiece segment in _snake)
        {
          
                int tempX = segment.X;
                int tempY = segment.Y;

                    segment.X = newX;
                    segment.Y = newY;
                    UpdatePiece(segment);
                
                    newX = tempX;
                    newY = tempY;
            
            

           

        }
        if (_yummy)
        {
            Body newBody = new Body(newX, newY);
            _snake.Add(newBody);
            UpdatePiece(newBody);
            
            _yummy = false;
        }
        else
        {
            EmptySpace space = new EmptySpace(newX, newY);
            UpdatePiece(space);
        }
    }
}