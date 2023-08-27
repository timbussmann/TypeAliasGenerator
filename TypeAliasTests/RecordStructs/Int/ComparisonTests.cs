namespace TypeAliasTests.RecordStructs.Int;

public class ComparisonTests
{
    [Test]
    public void ShouldSupportEquality()
    {
        var a = new IntRecordStruct(100);
        var b = new IntRecordStruct(100);
        var c = new IntRecordStruct(99);

        Assert.AreEqual(a, b);
        Assert.IsTrue(a == b);
        Assert.AreNotEqual(a, c);
        Assert.IsFalse(a == c);
        Assert.AreNotEqual(b, c);
        Assert.IsFalse(b == c);
    }

    [Test]
    public void ShouldSupportInequality()
    {
        var a = new IntRecordStruct(100);
        var b = new IntRecordStruct(100);
        var c = new IntRecordStruct(99);

        Assert.IsTrue(a != c);
        Assert.IsFalse(a != b);
    }

    [Test]
    public void ShouldSupportGreaterComparison()
    {
        var a = new IntRecordStruct(100);
        var b = new IntRecordStruct(99);

        Assert.IsTrue(a > b);
        Assert.IsFalse(b > a);
    }

    [Test]
    public void ShouldSupportSmallerComparison()
    {
        var a = new IntRecordStruct(100);
        var b = new IntRecordStruct(99);

        Assert.IsTrue(b < a);
        Assert.IsFalse(a < b);
    }

    [Test]
    public void ShouldSupportGreaterEqualsComparison()
    {
        var a = new IntRecordStruct(100);
        var b = new IntRecordStruct(100);
        var c = new IntRecordStruct(99);

        Assert.IsTrue(a >= b);
        Assert.IsTrue(a >= c);
        Assert.IsFalse(c >= a);
    }

    [Test]
    public void ShouldSupportSmallerEqualsComparison()
    {
        var a = new IntRecordStruct(100);
        var b = new IntRecordStruct(100);
        var c = new IntRecordStruct(99);

        Assert.IsTrue(a <= b);
        Assert.IsFalse(a <= c);
        Assert.IsTrue(c <= a);
    }
}