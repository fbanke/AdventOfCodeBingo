using System.Collections.Generic;
using System.Linq;

namespace Bingo
{
    public class Board
    {
        private readonly int[,] _numbers;

        public Board(int[,] numbers)
        {
            _numbers = numbers;
        }

        public int Rows()
        {
            return _numbers.GetLength(0);
        }

        public int Columns()
        {
            return _numbers.GetLength(1);
        }

        public bool Winner(IReadOnlyCollection<int> playedNumbers)
        {
            return WinnerInRows(playedNumbers) || WinnerInColumns(playedNumbers);
        }

        private bool WinnerInColumns(IReadOnlyCollection<int> playedNumbers)
        {
            foreach (var column in Enumerable.Range(0, Columns()))
            {
                if (IsColumnWinner(column, playedNumbers))
                {
                    return true;
                }
            }

            return false;
        }

        private bool WinnerInRows(IReadOnlyCollection<int> playedNumbers)
        {
            foreach (var row in Enumerable.Range(0, Rows()))
            {
                if (IsRowWinner(row, playedNumbers))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsColumnWinner(int column, IReadOnlyCollection<int> playedNumbers)
        {
            for (var i = 0; i < Rows(); i++)
            {
                if ( ! playedNumbers.Contains(_numbers[i, column]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsRowWinner(int row, IReadOnlyCollection<int> playedNumbers)
        {
            for (var i = 0; i < Columns(); i++)
            {
                if ( ! playedNumbers.Contains(_numbers[row, i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int Score(IReadOnlyCollection<int> playedNumbers)
        {
            var unmarkedNumbers = new List<int>();
            foreach (var row in Enumerable.Range(0, Rows()))
            {
                foreach (var column in Enumerable.Range(0, Columns()))
                {
                    if ( ! playedNumbers.Contains(_numbers[row, column]))
                    {
                        unmarkedNumbers.Add(_numbers[row, column]);
                    }
                }
            }

            return unmarkedNumbers.Sum() * playedNumbers.Last();
        }
    }
}