using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace day03
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

            // #id @ x,y: wxh
            var regex = new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<w>\d+)x(?<h>\d+)");
            string line;
            var fabric = new int[1000, 1000];

            // just fill the fabric matrix with the counts of claims and select anything greater than 1
            using (var file = new StreamReader("input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var parts = regex.Match(line);
                    var claim = new FabricClaim(
                        int.Parse(parts.Groups["id"].Value),
                        int.Parse(parts.Groups["x"].Value),
                        int.Parse(parts.Groups["y"].Value),
                        int.Parse(parts.Groups["w"].Value),
                        int.Parse(parts.Groups["h"].Value));

                    for (var i = claim.StartX; i < claim.EndX; i++)
                    {
                        for (var j = claim.StartY; j < claim.EndY; j++)
                        {
                            fabric[i, j]++;
                        }
                    }
                }
            }

            // the cast here causes the array to get flattened and become enumerable
            var query = from int item in fabric where item > 1 select item;
            return query.Count();
        }

        static int PartTwo()
        {
            var regex = new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<w>\d+)x(?<h>\d+)");
            string line;
            var fabric = new int[1000, 1000];
            var candidates = new HashSet<int>();
            // just fill the fabric matrix with the counts of claims and select anything greater than 1
            using (var file = new StreamReader("input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var parts = regex.Match(line);
                    var claim = new FabricClaim(
                        int.Parse(parts.Groups["id"].Value),
                        int.Parse(parts.Groups["x"].Value),
                        int.Parse(parts.Groups["y"].Value),
                        int.Parse(parts.Groups["w"].Value),
                        int.Parse(parts.Groups["h"].Value));


                    var overlaps = false;
                    for (var i = claim.StartX; i < claim.EndX; i++)
                    {
                        for (var j = claim.StartY; j < claim.EndY; j++)
                        {
                            if (fabric[i, j] == 0)
                            {
                                // square is empty
                                fabric[i, j] = claim.ClaimId;
                            }
                            else
                            {
                                //not empty, remove the existing claim and set overlaps to true
                                candidates.Remove(fabric[i, j]);
                                overlaps = true;
                            }
                        }
                    }
                    // if not overlapping so far add to the list of candidates
                    if (!overlaps)
                    {
                        candidates.Add(claim.ClaimId);
                    }
                }
            }
            //there should only be one left at this point
            return candidates.Single();
        }
    }
}
