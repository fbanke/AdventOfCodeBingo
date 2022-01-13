using System.Linq;

namespace Bingo
{
    public static class NumberCallerFactory
    {
        public static NumberCaller From(string numbersInput)
        {
            var numbers = numbersInput.Split(",");
            return new NumberCaller(numbers.Select(int.Parse));
        }
    }
}