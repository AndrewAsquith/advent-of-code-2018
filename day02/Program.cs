using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        static String PartTwo()
        {
            //need to find the ids that differ by only one character
            //and return the characters that are the same between them
            
            //this could probably be more efficient by using the character set as the outer loop as the alphabet is 
            //smaller than the input space, but this is much more readable, even if it is O(n^2)
            var input = File.ReadAllLines("input.txt");

            foreach (var id in input)
            {
                //compare each id to each other
                foreach (var otherId in input)
                {
                    //buffer to store the resulting string
                    var resultingId = new StringBuilder();

                    //number of differences between the two ids being compared
                    var numDifferences = 0;

                    //for every character in the current string (we assume all the same length)
                    for (var i = 0; i < id.Length; i++)
                    {
                        //if they match, add them to the buffer
                        if (id[i] == otherId[i])
                        {
                            resultingId.Append(id[i]);
                        }
                        else
                        {
                            numDifferences++;
                        }
                        //if we have more than one difference short circuit this instance
                        if (numDifferences > 1) { break; }
                    }
                    // if there was only one difference, this is what were we searching for
                    if (numDifferences == 1)
                    {
                        return resultingId.ToString();
                    }
                }
            }
            //something went terribly wrong
            return "No Candidates Found";
        }
    }
}
