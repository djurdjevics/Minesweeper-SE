using Minesweeper.Common.Enums;

namespace Minesweeper.Entities;

public class Game
{
    public Game(int numberOfMines, int numberOfLives)
    {
        Gamefield = new(numberOfMines, 9, 9);
        Player = new(
            currentPosition: new(0, 0),
            numberOfLives
        );
    }

    public void MovePlayer(Player player, MovementDirection movementDirection)
    {
        PlayerPosition newPlayerPosition = new(player.CurrentPosition.X, player.CurrentPosition.Y);

        DetermineNewPlayerPosition(movementDirection, newPlayerPosition);

        if (!IsNewPlayerPositionValid(Gamefield, newPlayerPosition))
            throw new ArgumentOutOfRangeException();

        player.ChangePosition(new(newPlayerPosition.X, newPlayerPosition.Y));

        if (IsPlayerHitMineField(Gamefield, player))
        {
            player.DecreaseNumberOfLives();
        }

        if (!IsFieldAlreadyExplored(Gamefield, player.CurrentPosition))
        {
            Gamefield.Board[player.CurrentPosition.X, player.CurrentPosition.Y] = true;
            Player.IncreaseNumberOfMoves();
        }
    }

    public bool IsPlayerWon()
    {
        if (Player.CurrentPosition.X == Gamefield.Height - 1)
            return true;

        return false;
    }

    public bool IsPlayerLost()
    {
        if (Player.NumberOfLives == 0)
            return true;

        return false;
    }

    private static void DetermineNewPlayerPosition(MovementDirection movementDirection, PlayerPosition newPlayerPosition)
    {
        if (movementDirection == MovementDirection.Up)
            newPlayerPosition.X--;

        if (movementDirection == MovementDirection.Down)
            newPlayerPosition.X++;

        if (movementDirection == MovementDirection.Left)
            newPlayerPosition.Y--;

        if (movementDirection == MovementDirection.Right)
            newPlayerPosition.Y++;
    }

    private bool IsNewPlayerPositionValid(Gamefield gameField, PlayerPosition playerPosition)
    {
        if (playerPosition.X < 0 ||
            playerPosition.Y < 0 ||
            playerPosition.X >= gameField.Width ||
            playerPosition.Y >= gameField.Height
        )
        {
            return false;
        }
        return true;
    }
    private bool IsPlayerHitMineField(Gamefield gameField, Player player)
    {
        if (IsFieldAlreadyExplored(gameField, player.CurrentPosition))
            return false;

        if (gameField.MineFieldPositions.Any(mineField =>
        mineField.Item1 == player.CurrentPosition.X &&
        mineField.Item2 == player.CurrentPosition.Y)
        )
        {
            return true;
        }

        return false;
    }

    private bool IsFieldAlreadyExplored(Gamefield gameField, PlayerPosition position) =>
        gameField.Board[position.X, position.Y] is true;
    public Gamefield Gamefield { get; private set; }
    public Player Player { get; private set; }
}
