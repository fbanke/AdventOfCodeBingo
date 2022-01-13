using Xunit;
using System.Linq;

namespace Bingo.Test
{
    public class NumberCallerTest
    {
        [Fact]
        public void GivenNumbers_WhenGettingFirst_ThenItMatchesWithExample()
        {
            var caller = NumberCallerFactory.From(Numbers);
            Assert.Equal(7, caller.First());
        }

        [Fact]
        public void GivenNumbers_WhenCountingTotal_ThenItMatchesWithExample()
        {
            var caller = NumberCallerFactory.From(Numbers);
            Assert.Equal(27, caller.Count);
        }
        
        // 27 elements
        private const string Numbers = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1";
    }
}