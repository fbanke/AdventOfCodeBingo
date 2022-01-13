using System;
using System.Linq;
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

        private static Board GetBoard(string number)
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
}