
using Snake;

Console.CursorVisible = false;
Board board = new Board(30, 15);


while (!board.GameOver)
{
    board.Draw();
    board.Input();
    board.Update();
    Thread.Sleep(200);
}

