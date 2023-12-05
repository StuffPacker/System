using FluentAssertions;
using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Mapper;

namespace SP.Shared.Common.Tests.Feature.PackingList;

[TestClass]
public class ItemModelMapperTest
{
    [TestMethod]
    public void ShouldMap()
    {
        var userId = Guid.NewGuid();
        var target = GetTarget();
        var model = new ItemModel
        {
            Id = "id",
            Name = "name",
            UserId = userId,
            Weight = 5,
            WeightSufix = "WeightSufix"
        };
        var newDto = target.Map(model);
        newDto.Id.Should().BeEquivalentTo("id");
        newDto.Name.Should().BeEquivalentTo("name");
        newDto.Weight.Should().Be(5);
        newDto.WeightSufix.Should().BeEquivalentTo("WeightSufix");
        newDto.UserId.ToString().Should().BeEquivalentTo(userId.ToString());

        var dto = new ItemDto
        {
            Id = "id",
            Name = "name",
            Weight = 5,
            WeightSufix = "WeightSufix",
            UserId = userId.ToString()
        };
        var newModel = target.Map(dto);
        newModel.Id.Should().BeEquivalentTo("id");
        newModel.Name.Should().BeEquivalentTo("name");
        newModel.Weight.Should().Be(5);
        newModel.WeightSufix.Should().BeEquivalentTo("WeightSufix");
        newModel.UserId.ToString().Should().BeEquivalentTo(userId.ToString());
    }

    private IItemModelMapper GetTarget()
    {
        return new ItemModelMapper();
    }
}