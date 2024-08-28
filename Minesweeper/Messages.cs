namespace Minesweeper;

public static class Messages
{
    public static void Instructions()
    {
        Console.WriteLine("Velg en rute via å skrive rad, så kolonne, og skriv F for å legge flagg eller R for å avsløre ruten." +
                          "\nHusk mellomrom! (f.eks 2 3 F, eller 13 25 r)");
    }

    public static void Winner()
    {
        Console.WriteLine("Grattis, du vant");
    }

    public static void Loser()
    {
        Console.WriteLine("Lol du tapte, din idiot");
    }
    static void ShowFlagCount()
    {

    }

    public static Board SizeChoice()
    {
        Board board = null;
        Console.Clear();
        Console.WriteLine("Velg vanskelighetsgrad");
        Console.WriteLine("1: Lett (10 miner)");
        Console.WriteLine("2: Normal (40 miner)");
        Console.WriteLine("3: Vanskelig (99 miner)");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                board = new Board(9, 10);
                break;
            case 2:
                board = new Board(16, 40);
                break;
            case 3:
                board = new Board(16, 30, 99);
                break;
            default: Console.WriteLine("Velg ett gyldig tall");
                SizeChoice();
                break;
        }

        return board;

    }

    
}