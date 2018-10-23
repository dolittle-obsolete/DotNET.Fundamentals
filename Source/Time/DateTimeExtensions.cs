using System;

namespace Dolittle.Time
{
    /// <summary>
    /// Extensions for DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
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