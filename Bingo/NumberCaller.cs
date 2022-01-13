using System.Collections;
using System.Collections.Generic;

namespace Bingo
{
    public class NumberCaller : IReadOnlyCollection<int>
    {
        private readonly List<int> _numbers;

        public NumberCaller(IEnumerable<int> numbers)
        {
            _numbers = new List<int>(numbers);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return _numbers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _numbers.Count;
    }
}