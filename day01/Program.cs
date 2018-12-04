using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Part One: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        static int PartOne()
        {
            //just need to sum the contents of the file starting from zero
            var input = File.ReadAllLines("input.txt");
            return input.Sum(x => Int32.Parse(x));
        }

        static int PartTwo()
        {
            // need to find the first thing repeated twice
            var input = File.ReadAllLines("input.txt").Select(x => Int32.Parse(x)).ToList();

            //store the frequency in a hash table until we find a dupe
            var seen = new HashSet<int>();
            var sum = 0;

            //may need to loop the input more than once according to the puzzle description
            while (true)
            {

                foreach (var i in input)
                {
                    //add the next value to get the new frequency
                    sum += i;

                    //if we've already seen it, return it
                    if (seen.Contains(sum))
                    {
                        return sum;
                    }

                    //otherwise add it to the hash table
                    seen.Add(sum);
                }
            }
        }
    }
}
