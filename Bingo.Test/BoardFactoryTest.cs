using System;
using System.Text.RegularExpressions;
using Xunit;

namespace Bingo.Test
{
    public class BoardFactoryTest
    {
        [Fact]
        public void GivenBoard_WhenGettingRows_ThenRowCountMatchExample()
        {
            var board = GetBoard();
            Assert.Equal(5, board.Rows());
        }
        
        [Fact]
        public void GivenBoard_WhenGettingColumns_ThenColumnCountMatchExample()
        {
            var board = GetBoard();
            Assert.Equal(5, board.Columns());
        }

        private static Board GetBoard()
        {
            var board = BoardFactory.FromString(Board5X5);
            return board;
        }

        private const string Board5X5 = @"22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19";
    }

    public class BoardFactory
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
    }
}