using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day02
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
            //need to find the number of ids that have
            //1 - a letter that repeats twice
            //2 - a letter that repeats three times
            var input = File.ReadAllLines("input.txt");

            var repeatsTwice = 0;
            var repeatsThrice = 0;

            foreach (var boxid in input)
            {

                //use a dictionary to store the Character & number of times it appears
                var characterCount = new Dictionary<char, int>();

                foreach (var c in boxid)
                {
                    var count = characterCount.GetValueOrDefault(c, 0);
                    count++;
                    characterCount[c] = count;
                }

                //increment repeats twice or thrice if appropriate
                if (characterCount.Values.Contains(2))
                {
                    repeatsTwice++;
                }
                if (characterCount.Values.Contains(3))
                {
                    repeatsThrice++;
                }
            }

            //return the multiple of those two values
            return repeatsTwice * repeatsThrice;
        }

        static int PartTwo()
        {
            //need to find the ids that differ by only one character
            var input = File.ReadAllLines("input.txt");
        }
    }
}
