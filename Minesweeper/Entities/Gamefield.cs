namespace Minesweeper.Entities;

public class Gamefield
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool[,] Board { get; private set; }
    public IReadOnlyCollection<(int, int)> MineFieldPositions => _mineFieldPositions.AsReadOnly();

    private List<(int, int)> _mineFieldPositions;

    public Gamefield(int numberOfMines, int width, int height)
    {
        Board = new bool[width, height];
        Board[0, 0] = true;

        Width = width;
        Height = height;

        InitializeMinePositions(numberOfMines);
    }

    private void InitializeMinePositions(int numberOfMines)
    {
        Random r = new();
        _mineFieldPositions = new(numberOfMines);

        (int, int) minePositionCoords;

        for (int i = 1; i <= numberOfMines; i++)
        {
            int minePosition = r.Next(0, 99);

            int minePositionX = minePosition / Height;
            int minePositionY = minePosition % Height;

            minePositionCoords = new(minePositionX, minePositionY);

            if (_mineFieldPositions.Any(minePosition => minePosition == minePositionCoords))
            {
                i--;
                continue;
            }

            _mineFieldPositions.Add(minePositionCoords);
        }
    }
}
