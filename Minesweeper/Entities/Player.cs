namespace Minesweeper.Entities;

public class Player
{
    public Player((int X, int Y) currentPosition, int numberOfLives)
    {
        CurrentPosition = new(currentPosition.X, currentPosition.Y);
        NumberOfLives = numberOfLives;
        NumberOfMoves = 0;
    }

    public PlayerPosition CurrentPosition { get; private set; }
    public int NumberOfLives { get; private set; }
    public int NumberOfMoves { get; private set; }

    public void ChangePosition((int X, int Y) newPlayerPosition)
    {
        CurrentPosition.X = newPlayerPosition.X;
        CurrentPosition.Y = newPlayerPosition.Y;
    }

    public void DecreaseNumberOfLives() =>
        NumberOfLives--;

    public void IncreaseNumberOfMoves() =>
        NumberOfMoves++;
}

public class PlayerPosition
{
    public int X { get; set; }
    public int Y { get; set; }

    public PlayerPosition(int x, int y)
    {
        X = x;
        Y = y;
    }
}
