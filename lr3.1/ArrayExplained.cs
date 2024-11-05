using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lr3._1
{
    public static class ArrayProcessor
    {
        private static readonly Random _random = new Random();
       
        public static int[] GenerateRandomArray(this int[] array, int min = 0, int max = 10)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = _random.Next(min, max);
            }
            return array;
        }

        public static Task<int[]> GenerateRandomArrayAsync(this int[] array, int min = 0, int max = 10)
        {
            return Task.Run(() => GenerateRandomArray(array, min, max));
        }
    }
}
