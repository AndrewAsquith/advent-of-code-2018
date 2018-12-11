using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var sleepSchedule = Initialize();
            Console.WriteLine($"Part One: {PartOne(sleepSchedule)}");
            Console.WriteLine($"Part Two: {PartTwo(sleepSchedule)}");
        }

        static int PartOne(Dictionary<int, Dictionary<int, int>> sleepSchedule)
        {


            var mostSleepyGuard = sleepSchedule
                                .ToDictionary(x => x.Key, v => v.Value.Sum(y => y.Value))
                                .OrderByDescending(x => x.Value)
                                .First().Key;

            var mostSleepyMinute = sleepSchedule[mostSleepyGuard]
                                    .OrderByDescending(x => x.Value)
                                    .First().Key;

            return mostSleepyGuard * mostSleepyMinute;
        }

        static int PartTwo(Dictionary<int, Dictionary<int, int>> sleepSchedule)
        {
            return 0;
        }

        private static Dictionary<int, Dictionary<int, int>> Initialize()
        {
            // guard -> [minute of hour, how long slept]
            var sleepSchedule = new Dictionary<int, Dictionary<int, int>>();

            //read the input and sort it alphabetically which is also chronologically
            var input = File.ReadAllLines("input.txt").OrderBy(x => x);


            var regex = new Regex(@"\[(?<datetime>[\d\s-:]+)\] (?<action>Guard|wakes|falls)( #(?<guard>\d+))?");

            var currentGuard = 0;

            TimelineEvent currentEvent;
            TimelineEvent previousEvent = null;

            foreach (var line in input)
            {
                var parts = regex.Match(line);

                switch (parts.Groups["action"].Value)
                {
                    case "Guard":
                        currentGuard = int.Parse(parts.Groups["guard"].Value);

                        currentEvent = new TimelineEvent(
                            currentGuard,
                            DateTime.Parse(parts.Groups["datetime"].Value),
                            TimelineAction.BeginShift);
                        break;

                    case "wakes":
                        currentEvent = new TimelineEvent(
                            currentGuard,
                            DateTime.Parse(parts.Groups["datetime"].Value),
                            TimelineAction.WakeUp);

                        break;

                    case "falls":
                        currentEvent = new TimelineEvent(
                            currentGuard,
                            DateTime.Parse(parts.Groups["datetime"].Value),
                            TimelineAction.FallAsleep);
                        break;
                    default:
                        currentEvent = new TimelineEvent();
                        break;
                }
                //sanity check
                if (previousEvent != null && currentEvent.ActionType != TimelineAction.Unknown && previousEvent.GuardId == currentEvent.GuardId)
                {
                    //if guard fell asleep, we need to record it
                    if (previousEvent.ActionType == TimelineAction.FallAsleep)
                    {
                        var minutesInState = currentEvent.Timestamp.Subtract(previousEvent.Timestamp).Minutes;
                        var sleepLog = sleepSchedule.GetValueOrDefault(currentEvent.GuardId, new Dictionary<int, int>());


                        if (!sleepSchedule.ContainsKey(currentEvent.GuardId))
                        {
                            sleepSchedule.Add(currentEvent.GuardId, new Dictionary<int, int>());
                        }
                        for (int i = 0; i < minutesInState; i++)
                        {
                            var currentMinute = previousEvent.Timestamp.AddMinutes(i).Minute;
                            if (!sleepLog.ContainsKey(currentMinute))
                            {
                                sleepLog.Add(currentMinute, 1);
                            }
                            else
                            {
                                sleepLog[currentMinute]++;
                            }


                        }

                    }
                }
                previousEvent = currentEvent;
            }

            return sleepSchedule;
        }


    }
}
