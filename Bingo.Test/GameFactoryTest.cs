using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bingo.Test
{
    public class GameFactoryTest
    {
        [Fact]
        public void GivenExampleGame_WhenCreating_ThenGameSizeMatchesExample()
        {
            var game = GameFactory.From(Game);
            
            Assert.Equal(27, game.NumberCaller.Count);
            Assert.Equal(3, game.Boards.Count);
        }
        
        [Fact]
        public void GivenGame_WhenPlayingFirstNumber_ThenGame()
        {
            var game = GameFactory.From(Game);
            PlayRounds(game, 11);
            
            // 12'th round makes it a winner
            game.PlayRound();

            Assert.True(game.HasWinner());
        }

        private static void PlayRounds(Game game, int rounds)
        {
            for (var round = 0; round < rounds; round++)
            {
                game.PlayRound();
            }
        }

        // Example game from Advent of code
        private const string Game = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";
    }

    public class GameFactory
    {
        public static Game From(string gameInput)
        {
            var gameParts = GetGameParts(gameInput);

            var numberCaller = NumberCallerFactory.From(GetNumberCallerPart(gameParts));
            var boards = GetBoards(gameParts);

            return new Game(numberCaller, boards);
        }

        private static List<Board> GetBoards(IReadOnlyList<string> gameParts)
        {
            var boards = new List<Board>();
            foreach(var board in GetBoardParts(gameParts))
            {
                boards.Add(BoardFactory.FromString(board));
            }

            return boards;
        }

        private static IEnumerable<string> GetBoardParts(IReadOnlyCollection<string> gameParts)
        {
            foreach (var gamePart in gameParts.Skip(GamePartNumberCaller))
            {
                yield return gamePart;
            }
        }

        private const int GamePartNumberCaller = 1;

        private static string GetNumberCallerPart(string[] gameParts)
        {
            return gameParts[0];
        }

        private static string[] GetGameParts(string game)
        {
            return game.Split(new [] { "\r\n\r\n" },
                StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public class Game
    {
        public Game(NumberCaller numberCaller, IReadOnlyCollection<Board> boards)
        {
            NumberCaller = numberCaller;
            _numbers = numberCaller.GetEnumerator();
            Boards = boards;
        }

        private readonly IEnumerator<int> _numbers;
        private readonly List<int> _playedNumbers = new();

        public NumberCaller NumberCaller { get; }
        public IReadOnlyCollection<Board> Boards { get; }

        public void PlayRound()
        {
            _numbers.MoveNext();
            var number = _numbers.Current;
            _playedNumbers.Add(number);
        }

        public bool HasWinner()
        {
            return Boards.Any(board => board.Winner(_playedNumbers));
        }
    }
}