using Minesweeper;


Board board = Messages.SizeChoice();

while (!board.GameOver)
{
    board.Print();
    board.Input();
}