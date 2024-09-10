using Minesweeper.Common.Enums;
using Minesweeper.Entities;
using System.Reflection;

namespace Minesweeper.Tests
{
    public class GameTests
    {
        [Fact]
        public void Constructor_ShouldInitializeGameWithGivenMinesAndLives()
        {
            // Arrange
            int numberOfMines = 10;
            int numberOfLives = 3;

            // Act
            var game = new Game(numberOfMines, numberOfLives);

            // Assert
            Assert.Equal(numberOfMines, game.Gamefield.MineFieldPositions.Count);
            Assert.Equal(3, game.Player.NumberOfLives);
            Assert.Equal(0, game.Player.CurrentPosition.X);
            Assert.Equal(0, game.Player.CurrentPosition.Y);
        }

        [Fact]
        public void MovePlayer_ValidMove_ShouldUpdatePlayerPosition()
        {
            // Arrange
            var game = new Game(0, 3);
            var initialPosition = new PlayerPosition(game.Player.CurrentPosition.X, game.Player.CurrentPosition.Y);

            // Act
            game.MovePlayer(game.Player, MovementDirection.Right);

            // Assert
            Assert.NotEqual(initialPosition, game.Player.CurrentPosition);
            Assert.Equal(1, game.Player.CurrentPosition.Y);
        }

        [Fact]
        public void MovePlayer_InvalidMove_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var game = new Game(0, 3);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                game.MovePlayer(game.Player, MovementDirection.Left)
            );
        }

        [Fact]
        public void MovePlayer_HitMine_ShouldDecreasePlayerLives()
        {
            // Arrange
            var game = new Game(1, 3);

            var mineFieldPositionsField = typeof(Gamefield)
                .GetField("_mineFieldPositions", BindingFlags.NonPublic | BindingFlags.Instance);

            if (mineFieldPositionsField != null)
            {
                var mineFieldPositions = mineFieldPositionsField.GetValue(game.Gamefield) as List<(int, int)>;
                mineFieldPositions?.Add((1, 0)); 
            }
            // Act
            game.MovePlayer(game.Player, MovementDirection.Down);

            // Assert
            Assert.Equal(2, game.Player.NumberOfLives);
        }

        [Fact]
        public void MovePlayer_ExploreNewField_ShouldMarkFieldAsExploredAndIncreaseMoves()
        {
            // Arrange
            var game = new Game(0, 3);

            // Act
            game.MovePlayer(game.Player, MovementDirection.Down);

            // Assert
            Assert.True(game.Gamefield.Board[1, 0]);
            Assert.Equal(1, game.Player.NumberOfMoves);
        }

        [Fact]
        public void IsPlayerWon_PlayerReachesLastRow_ShouldReturnTrue()
        {
            // Arrange
            var game = new Game(0, 3);
            game.Player.ChangePosition(new(game.Gamefield.Height - 1, 0));

            // Act
            var result = game.IsPlayerWon();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPlayerWon_PlayerNotInLastRow_ShouldReturnFalse()
        {
            // Arrange
            var game = new Game(0, 3);

            // Act
            var result = game.IsPlayerWon();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsPlayerLost_NoLivesLeft_ShouldReturnTrue()
        {
            // Arrange
            var game = new Game(0, 1); 
            game.Player.DecreaseNumberOfLives();

            // Act
            var result = game.IsPlayerLost();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPlayerLost_LivesRemaining_ShouldReturnFalse()
        {
            // Arrange
            var game = new Game(0, 3);

            // Act
            var result = game.IsPlayerLost();

            // Assert
            Assert.False(result);
        }
    }
}
