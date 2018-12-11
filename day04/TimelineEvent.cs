using System;

namespace day04
{
    internal class TimelineEvent
    {
        public int GuardId { get; set; }

        public TimelineAction ActionType { get; set; }

        public DateTime Timestamp { get; set; }
        public TimelineEvent(int id, DateTime dateTime, TimelineAction action)
        {
            GuardId = id;
            Timestamp = dateTime;
            ActionType = action;
        }

        public TimelineEvent()
        {
            GuardId = 0;
            Timestamp = DateTime.MinValue;
            ActionType = TimelineAction.Unknown;
        }
    }
}