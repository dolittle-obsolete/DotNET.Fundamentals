namespace Dolittle.PropertyBags.Specs
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Dolittle.Concepts;
    using Dolittle.Reflection;
    using Machine.Specifications;
    public static class ShouldExtensions
    {
        private const string NULL = "[NULL]";

        public static void ShouldBeAnAccurateRepresentationOf<T>(this Value<T> source, Value<T> other) where T : Value<T>
        {
            IsAnAccurateRepresentationOf(source,other);
        }
        static void IsAnAccurateRepresentationOf<T>(Value<T> source, Value<T> other) where T : Value<T>
        {
            if (other == null)
                throw new SpecificationException("The comparison object is null");

            var t = source.GetType();
            var otherType = other.GetType();

            if (t != otherType)
                throw new SpecificationException($"Should be of type {t.FullName} but is of type {otherType.FullName}");

            var fields = t.BaseType.GetMethod("GetFields",BindingFlags.Instance | BindingFlags.NonPublic).Invoke(source,new object[0]) as IEnumerable<FieldInfo>;

            foreach (var field in fields)
            {
                var value1 = field.GetValue(source);
                var value2 = field.GetValue(other);

                if (field.FieldType.IsDate())
                {
                    if(field.FieldType.IsNullable())
                    {
                        var first = (DateTime?)value1;
                        var second = (DateTime?)value2;
                        if(first.HasValue != second.HasValue)
                        {
                            var firstValue = first.HasValue ? first.Value.ToString() : NULL;
                            var secondValue = second.HasValue ? second.Value.ToString() : NULL;
                            throw new SpecificationException($"{field.Name} is different: expected { firstValue } but got { secondValue }");
                        }

                        if(first.HasValue &&  second.HasValue)
                            first.Value.ShouldBeCloseTo(second.Value.ToUniversalTime(),TimeSpan.FromMilliseconds(1));
                    } 
                    else 
                    {
                        var first = (DateTime)value1;
                        first.ShouldBeCloseTo(((DateTime)value2).ToUniversalTime(),TimeSpan.FromMilliseconds(1));
                    }
                } 
                else if (field.FieldType.IsDateTimeOffset())
                {
                    if(field.FieldType.IsNullable())
                    {
                        var first = (DateTimeOffset?)value1;
                        var second = (DateTimeOffset?)value2;
                        if(first.HasValue != second.HasValue)
                        {
                            var firstValue = first.HasValue ? first.Value.ToString() : NULL;
                            var secondValue = second.HasValue ? second.Value.ToString() : NULL;
                            throw new SpecificationException($"{field.Name} is different: expected { firstValue } but got { secondValue }");
                        }
                        if(first.HasValue &&  second.HasValue)
                            first.Value.DateTime.ShouldBeCloseTo(second.Value.DateTime.ToUniversalTime(),TimeSpan.FromMilliseconds(1));
                    } 
                    else 
                    {
                        var first = ((DateTimeOffset)value1).ToUniversalTime();
                        first.DateTime.ShouldBeCloseTo(((DateTimeOffset)value2).DateTime.ToUniversalTime(),TimeSpan.FromMilliseconds(1));
                    }
                } 
                else if (field.FieldType.IsValue())
                {
                    if(value1 != null || value2 != null)
                    {
                        var type = field.FieldType.GetValueType();
                        var method = typeof(ShouldExtensions).GetMethod("IsAnAccurateRepresentationOf", BindingFlags.Static | BindingFlags.NonPublic);
                        MethodInfo generic = method.MakeGenericMethod(type);
                        generic.Invoke(null, new object[]{value1,value2});
                    }
                } 
                else if(value1 != null || value2 != null)
                {

                    try
                    {
                        value1.ShouldEqual(value2);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }
                }  
            }
        }
    }
}