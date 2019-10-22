using System;
using Machine.Specifications;
using Dolittle.Time;

namespace Dolittle.Time.Specs.when_converting
{
    [Subject(typeof(DateTimeExtensions))]
    public class from_milliseconds_to_date_time
    {
		static long source_in_milliseconds;
        static DateTime converted;
        static DateTimeOffset now;
        
        Establish context = () =>
        {
	        now = DateTimeOffset.Now;
			source_in_milliseconds = now.ToUnixTimeMilliseconds(); 
		};

        Because of = () => converted = source_in_milliseconds.ToDateTime();

        It should_convert_the_time = () => converted.LossyEquals(now.ToUniversalTime()).ShouldBeTrue();
        It should_create_the_datetime_as_utc = () => converted.Kind.ShouldEqual(DateTimeKind.Utc);
    }
}