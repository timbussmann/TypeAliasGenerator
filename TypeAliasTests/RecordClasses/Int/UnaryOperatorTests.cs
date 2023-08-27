namespace TypeAliasTests.RecordClasses.Int;

public class UnaryOperatorTests
{
    [Test]
    public void ShouldSupportUnaryPlus()
    {
        var a = new IntRecordClass(4);
        var b = new IntRecordClass(-4);

        Assert.AreEqual(4, (+a).value);
        // This one is confusing but that's how it is supposed to work
        Assert.AreEqual(-4, (+b).value, "unary plus should not change the value");
        Assert.AreEqual(-4, +-4);
    }

    [Test]
    public void ShouldSupportUnaryMinus()
    {
        var a = new IntRecordClass(4);
        var b = new IntRecordClass(-4);

        Assert.AreEqual(-4, (-a).value);
        Assert.AreEqual(4, (-b).value, "unary minus should negate the value");
        Assert.AreEqual(-4, (-(-b)).value);
    }

    [Test]
    public void ShouldSupportUnaryIncrement()
    {
        var a = new IntRecordClass(4);
        var b = new IntRecordClass(4);
        var c = new IntRecordClass(-4);
        var d = new IntRecordClass(-4);

        Assert.AreEqual(5, (++a).value);
        Assert.AreEqual(5, a.value);

        Assert.AreEqual(4, b++.value);
        Assert.AreEqual(5, b.value);

        Assert.AreEqual(-3, (++c).value);
        Assert.AreEqual(-3, c.value);

        Assert.AreEqual(-4, d++.value);
        Assert.AreEqual(-3, d.value);
    }

    [Test]
    public void ShouldSupportUnaryDecrement()
    {
        var a = new IntRecordClass(4);
        var b = new IntRecordClass(4);
        var c = new IntRecordClass(-4);
        var d = new IntRecordClass(-4);

        Assert.AreEqual(3, (--a).value);
        Assert.AreEqual(3, a.value);

        Assert.AreEqual(4, b--.value);
        Assert.AreEqual(3, b.value);

        Assert.AreEqual(-5, (--c).value);
        Assert.AreEqual(-5, c.value);

        Assert.AreEqual(-4, d--.value);
        Assert.AreEqual(-5, d.value);
    }
}