using System.Collections.Generic;
using System.Linq;

namespace Bingo
{
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

        public int Score()
        {
            var board = Boards.First(board => board.Winner(_playedNumbers));

            return board.Score(_playedNumbers);
        }
    }
}