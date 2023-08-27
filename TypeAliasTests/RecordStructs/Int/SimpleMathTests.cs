namespace TypeAliasTests.RecordStructs.Int;

public class SimpleMathTests
{
    [Test]
    public void ShouldSupportAddition()
    {
        var a = new IntRecordStruct(2);
        var b = new IntRecordStruct(3);

        var result = a + b;

        Assert.AreEqual(5, result.i);
    }

    [Test]
    public void ShouldSupportSubtraction()
    {
        var a = new IntRecordStruct(10);
        var b = new IntRecordStruct(3);

        var result = a - b;

        Assert.AreEqual(7, result.i);
    }

    [Test]
    public void ShouldSupportMultiplication()
    {
        var a = new IntRecordStruct(5);
        var b = new IntRecordStruct(4);

        var result = a * b;

        Assert.AreEqual(20, result.i);
    }

    [Test]
    public void ShouldSupportDivision()
    {
        var a = new IntRecordStruct(100);
        var b = new IntRecordStruct(2);

        var result = a / b;

        Assert.AreEqual(50, result.i);
    }
}