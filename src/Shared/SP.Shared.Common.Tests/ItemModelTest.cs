using FluentAssertions;
using SP.Shared.Common.Feature.Item.Model;

namespace SP.Shared.Common.Tests;

[TestClass]
public class ItemModelTest
{
    [TestMethod]
    public void TestDecimal()
    {
        var target = new ItemModel();
        target.ChangeWeight("100");
        target.Weight.Should().Be(Convert.ToDecimal(100));
        target.ChangeWeight("10,100");
        target.Weight.Should().Be(Convert.ToDecimal(10.10));
        target.ChangeWeight("20.200");
        target.Weight.Should().Be(Convert.ToDecimal(20.20));
    }
}