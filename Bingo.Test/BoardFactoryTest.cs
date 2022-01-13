using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace Bingo.Test
{
    public class BoardFactoryTest
    {
        [Fact]
        public void GivenBoard_WhenGettingRows_ThenRowCountMatchExample()
        {
            var board = GetBoardAdventOfCode();
            Assert.Equal(5, board.Rows());
        }
        
        [Fact]
        public void GivenBoard_WhenGettingColumns_ThenColumnCountMatchExample()
        {
            var board = GetBoardAdventOfCode();
            Assert.Equal(5, board.Columns());
        }

        [Fact]
        public void Given1Board_WhenPlaying1_ThenTheBoardIsAWinner()
        {
            var board = GetBoard("1");
            Assert.True(board.Winner(new []{1}));
        }

        private Board GetBoard(string number)
        {
            var row = string.Concat(Enumerable.Repeat(number + " ", 5)).Trim()+Environment.NewLine;
            var board = string.Concat(Enumerable.Repeat(row, 5)).Trim();

            return BoardFactory.FromString(board);
        }

        private static Board GetBoardAdventOfCode()
        {
            return BoardFactory.FromString(Board5X5);
        }

        private const string Board5X5 = @"22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19";
    }

    public static class BoardFactory
    {
        public static Board FromString(string boardInput)
        {
            var board = new int[5,5];
            var rows = GetRows(boardInput);
            for(var rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                var numbers = GetNumbers(rows[rowIndex]);
                for (var columnIndex = 0; columnIndex < numbers.Length; columnIndex++)
                {
                    board[rowIndex,columnIndex] = ConvertStringToInt(numbers[columnIndex]);
                }
            }
            
            return new Board(board);
        }

        private static int ConvertStringToInt(string number)
        {
            return int.Parse(number.Trim());
        }

        private static string[] GetNumbers(string row)
        {
            return Regex.Split(row.Trim(), "[ ]+");
        }

        private static string[] GetRows(string boardInput)
        {
            return boardInput.Split(Environment.NewLine);
        }
    }

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