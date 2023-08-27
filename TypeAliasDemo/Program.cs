// ReSharper disable EqualExpressionComparison

using Generator;

namespace TypeAliasDemo
{
    //TODO: support nested types
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var p1 = new Price(42);
            var p2 = new Price(21);

            p1.Equals(p2);

            Console.WriteLine($"-p: {-p1}");
            Console.WriteLine($"+p: {+p1}");
            Console.WriteLine($"p1 + p2: {p1 + p2}");
            Console.WriteLine($"p1 - p2: {p1 - p2}");
            Console.WriteLine($"p1 * p2: {p1 * p2}");
            Console.WriteLine($"p1 / p2: {p1 / p2}");
            Console.WriteLine($"p1++: {p1++}, {p1}");
            Console.WriteLine($"++p1: {++p1}, {p1}");
            Console.WriteLine($"p1-- {p1--}, {p1}");
            Console.WriteLine($"--p1 {--p1}, {p1}");
            Console.WriteLine($"p1 > p2 {p1 > p2}");
            Console.WriteLine($"p1 >= p2 {p1 >= p2}");
            Console.WriteLine($"p1 >= p1 {p1 >= p1}");
            Console.WriteLine($"p1 < p2 {p1 < p2}");
            Console.WriteLine($"p1 <= p2 {p1 <= p2}");
            Console.WriteLine($"p1 <= p1 {p1 <= p1}");
            Console.WriteLine($"p1 == p2 {p1 == p2}");
            Console.WriteLine($"p1 == p1 {p1 == p1}");
            Console.WriteLine($"p1 != p2 {p1 != p2}");
            Console.WriteLine($"p1 != p1 {p1 != p1}");

            //var v1 = new Price(42);
            //Console.WriteLine(v1--);
            //v1--;
            //Console.WriteLine(v1);

            IntType i1 = new IntType(1);
            Console.WriteLine(i1.value);
            IntType i2 = new IntType(12);
            IntType i3 = i1 + i2;
            Console.WriteLine($"i1 + i2: {i3}");

            IntType i4 = new IntType(1);
            Console.WriteLine($"i = {i4}");
            var i5 = i4;
            Console.WriteLine($"i++ = {i4++}, (i = {i4})");
            Console.WriteLine($"++i = {++i4}, (i = {i4})");
            Console.WriteLine($"--i = {--i4}, (i = {i4})");
            Console.WriteLine($"i-- = {i4--}, (i = {i4})");

            //ReadonlyIntType ri1 = new ReadonlyIntType(1);
            //Console.WriteLine("++ri = " + ++ri1);
            //Console.WriteLine("ri = " + ri1);
            //ri1.value++;

            FloatType f1 = new FloatType(12.0f);
            FloatType f2 = new FloatType(2.0f);
            FloatType f3 = f1 + f2;
            Console.WriteLine($"f1 + f2: {f3}");

            Console.WriteLine("Double type: " + (new DoubleType(13) + new DoubleType(3)));
            Console.WriteLine("Decimal type: " + (new DecimalStruct(13.00m) + new DecimalStruct(3.00m)));


            StringStruct s1 = new StringStruct("hello ");
            StringStruct s2 = new StringStruct("world!");
            var s3 = s1 + s2;

        }
    }

    [TypeAlias]
    partial record struct Price(decimal value)
    {
        //TODO unary operators should probably not work with readonly structs
        //public static Price operator ++(Price price) { price.value++; return price; }

        // public static Price operator --(Price price) => new(price.value - 1);
    };

    partial record struct Price
    {
    }

    // This approach is nice and easy but it doesn't provide a useful level of type-safety as it is easy to mix types with the same base
    //partial record struct Price(decimal value) : IEquatable<Price>
    //{
    //    public static implicit operator decimal(Price price) => price.value;
    //    public static implicit operator Price(decimal v) => new Price(v);
    //};

    [TypeAlias]
    public partial record struct IntType(int value)
    {
    }

    [TypeAlias]
    public readonly partial record struct ReadonlyIntType(int value)
    {

    }

    [TypeAlias]
    public partial record struct FloatType(float x);


    [TypeAlias]
    internal partial record struct DoubleType(double d);


    [TypeAlias]
    partial record struct DecimalStruct(decimal v);

    [TypeAlias]
    public partial record struct StringStruct(string s);
}