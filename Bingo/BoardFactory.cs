using System;
using System.Text.RegularExpressions;

namespace Bingo
{
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
}