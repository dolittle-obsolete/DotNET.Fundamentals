using System;

namespace Dolittle.Time
{
    /// <summary>
    /// Extensions for DateTime
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Performs a lossy equals algorithm
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool LossyEquals(this DateTimeOffset first, DateTimeOffset second)
        {
            var firstUtc = first.UtcDateTime;
            var secondUtc = second.UtcDateTime;
            return 
                    firstUtc.Millisecond == secondUtc.Millisecond
                &&  firstUtc.Second == secondUtc.Second 
                &&  firstUtc.Day == secondUtc.Day
                &&  firstUtc.Year == secondUtc.Year
                &&  firstUtc.Ticks >> 10 == firstUtc.Ticks >> 10;
        } 
        /// <summary>
        /// Performs a lossy equals algorithm
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool LossyEquals(this DateTimeOffset first, DateTime second) => LossyEquals(first, new DateTimeOffset(second.ToUniversalTime()));
    }
}