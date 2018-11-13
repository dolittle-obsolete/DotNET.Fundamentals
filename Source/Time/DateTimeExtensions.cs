using System;

namespace Dolittle.Time
{
    /// <summary>
    /// Extensions for DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Performs a lossy equals algorithm
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool LossyEquals(this DateTime first, DateTimeOffset second) => second.LossyEquals(first);
        /// <summary>
        /// Performs a lossy equals algorithm
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool LossyEquals(this DateTime first, DateTime second) => new DateTimeOffset(first.ToUniversalTime()).LossyEquals((second.ToUniversalTime()));
        /// <summary>
        /// Converts the date to Unix Time in Milliseconds
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUniversalTime().ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Converts the unix milliseconds to a DateTime
        /// </summary>
        /// <param name="milliseconds">The epoch time in milliseconds</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long milliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).ToUniversalTime().DateTime;
        }  

        /// <summary>
        /// Converts the unix milliseconds to a DateTime
        /// </summary>
        /// <param name="milliseconds">The epoch time in milliseconds</param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this long milliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).ToUniversalTime();
        }        
    }
}