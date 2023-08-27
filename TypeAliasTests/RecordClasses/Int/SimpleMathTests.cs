namespace TypeAliasTests.RecordClasses.Int;

public class SimpleMathTests
{
    [Test]
    public void ShouldSupportAddition()
    {
        var a = new IntRecordClass(2);
        var b = new IntRecordClass(3);

        var result = a + b;

        Assert.AreEqual(5, result.value);
    }

    [Test]
    public void ShouldSupportSubtraction()
    {
        var a = new IntRecordClass(10);
        var b = new IntRecordClass(3);

        var result = a - b;

        Assert.AreEqual(7, result.value);
    }

    [Test]
    public void ShouldSupportMultiplication()
    {
        var a = new IntRecordClass(5);
        var b = new IntRecordClass(4);

        var result = a * b;

        Assert.AreEqual(20, result.value);
    }

    [Test]
    public void ShouldSupportDivision()
    {
        var a = new IntRecordClass(100);
        var b = new IntRecordClass(2);

        var result = a / b;

        Assert.AreEqual(50, result.value);
    }
}