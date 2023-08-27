namespace TypeAliasTests.RecordStructs.Int;

public class UnaryOperatorTests
{
    [Test]
    public void ShouldSupportUnaryPlus()
    {
        var a = new IntRecordStruct(4);
        var b = new IntRecordStruct(-4);

        Assert.AreEqual(4, (+a).i);
        // This one is confusing but that's how it is supposed to work
        Assert.AreEqual(-4, (+b).i, "unary plus should not change the value");
        Assert.AreEqual(-4, +(-4));
    }

    [Test]
    public void ShouldSupportUnaryMinus()
    {
        var a = new IntRecordStruct(4);
        var b = new IntRecordStruct(-4);

        Assert.AreEqual(-4, (-a).i);
        Assert.AreEqual(4, (-b).i, "unary minus should negate the value");
        Assert.AreEqual(-4, (-(-b)).i);
    }

    [Test]
    public void ShouldSupportUnaryIncrement()
    {
        var a = new IntRecordStruct(4);
        var b = new IntRecordStruct(4);
        var c = new IntRecordStruct(-4);
        var d = new IntRecordStruct(-4);

        Assert.AreEqual(5, (++a).i);
        Assert.AreEqual(5, a.i);

        Assert.AreEqual(4, (b++).i);
        Assert.AreEqual(5, b.i);

        Assert.AreEqual(-3, (++c).i);
        Assert.AreEqual(-3, c.i);

        Assert.AreEqual(-4, (d++).i);
        Assert.AreEqual(-3, d.i);
    }

    [Test]
    public void ShouldSupportUnaryDecrement()
    {
        var a = new IntRecordStruct(4);
        var b = new IntRecordStruct(4);
        var c = new IntRecordStruct(-4);
        var d = new IntRecordStruct(-4);

        Assert.AreEqual(3, (--a).i);
        Assert.AreEqual(3, a.i);

        Assert.AreEqual(4, (b--).i);
        Assert.AreEqual(3, b.i);

        Assert.AreEqual(-5, (--c).i);
        Assert.AreEqual(-5, c.i);

        Assert.AreEqual(-4, (d--).i);
        Assert.AreEqual(-5, d.i);
    }
}