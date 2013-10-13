using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutableCollections.Tests.TestInfrastructure
{
    static class RandomHelper
    {
        public static int[] Sequence(Random random, int count, int min = 0, int max = 1000)
        {
            return Enumerable.Repeat(0, count).Select(i => random.Next(min, max + 1)).ToArray();
        }

        public static int[] UniqueSequence(Random random, int count, int min = 0, int max = 1000)
        {
            var sequence = new List<int>(count);

            while (sequence.Count < count)
            {
                var item = random.Next(min, max + 1);
                
                if (!sequence.Contains(item))
                    sequence.Add(item);
            }

            return sequence.ToArray();
        }

        public static T[] Shuffle<T>(Random random, IEnumerable<T> items)
        {
            var sequence = items.ToArray();
            
            var n = sequence.Length;
            while (n > 1)
            {
                n--;
                var k = random.Next(n + 1);
                var value = sequence[k];
                sequence[k] = sequence[n];
                sequence[n] = value;
            }

            return sequence;
        }
    }
}
