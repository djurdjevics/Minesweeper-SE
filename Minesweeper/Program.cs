// See https://aka.ms/new-console-template for more information

using Minesweeper.Common.Enums;
using Minesweeper.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new(
        numberOfMines: 10,
        numberOfLives: 5
        );

        do
        {
            PrintBoard(game);
            var enteredKey = Console.ReadKey();
            MovementDirection direction;
            try
            {
                direction = enteredKey.Key switch
                {
                    ConsoleKey.UpArrow => MovementDirection.Up,
                    ConsoleKey.DownArrow => MovementDirection.Down,
                    ConsoleKey.LeftArrow => MovementDirection.Left,
                    ConsoleKey.RightArrow => MovementDirection.Right,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction))
                };

                game.MovePlayer(game.Player, direction);
                Console.WriteLine(game.Player.NumberOfLives);
                Console.WriteLine($"Current position: {game.Player.CurrentPosition.X}, {game.Player.CurrentPosition.Y}");

                if (game.IsPlayerWon())
                {
                    PrintBoard(game);
                    Console.WriteLine($"You have successfully completed game. Score: {game.Player.NumberOfMoves}");
                    break;
                }

                if (game.IsPlayerLost())
                {
                    PrintBoard(game);
                    Console.WriteLine($"You ran out of your lives. Try again.");
                    break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                continue;
            }
        }
        while (true);

        void PrintBoard(Game game)
        {
            Console.Clear();
            Console.WriteLine($"Number of lives: {game.Player.NumberOfLives}");
            Console.WriteLine($"Number of moves: {game.Player.NumberOfMoves}");
            for (int row = 0; row < game.Gamefield.Width; row++)
            {
                for (int col = 0; col < game.Gamefield.Height; col++)
                {

                    if (row == game.Player.CurrentPosition.X && col == game.Player.CurrentPosition.Y)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(" X ");
                    }
                    else if (game.Gamefield.Board[row, col] is true)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Black;
                        Console.Write("   ");
                    }

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}