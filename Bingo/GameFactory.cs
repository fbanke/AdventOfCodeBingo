using System;
using System.Collections.Generic;
using System.Linq;

namespace Bingo
{
    public static class GameFactory
    {
        public static Game From(string gameInput)
        {
            var gameParts = GetGameParts(gameInput);

            var numberCaller = NumberCallerFactory.From(GetNumberCallerPart(gameParts));
            var boards = GetBoards(gameParts);

            return new Game(numberCaller, boards);
        }

        private static List<Board> GetBoards(IReadOnlyCollection<string> gameParts)
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

        private static string GetNumberCallerPart(IReadOnlyCollection<string> gameParts)
        {
            return gameParts.First();
        }

        private static string[] GetGameParts(string game)
        {
            return game.Split(new [] { "\r\n\r\n" },
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}