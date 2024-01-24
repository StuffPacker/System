using System.Text.Json;
using FluentAssertions;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Shared.Common.Tests.Feature.PackingList;

[TestClass]
public class PackingListMapperTest
{
    [TestMethod]
    public void ShouldMapPackingListDto()
    {
        var dto1 = GetNewDto();
        var dto2 = GetNewDto();

        var dtosIn = new List<PackingListDto>();
        dtosIn.Add(dto1);
        dtosIn.Add(dto2);
        var target = GetTarget();
        var result = target.Map(dtosIn);
        result.Count().Should().Be(2);
        result.First().Id.Should().NotBeNullOrEmpty();
        result.First().Id.Should().BeEquivalentTo(dto1.Id);
    }

    [TestMethod]
    public void ShouldMapPackingListDtoString()
    {
         var dtoString =
            "[{\"userId\": \"8e9db8a2-c54e-41af-a1d9-2bef2e89b38c\",\"name\": \"new packing list\",\"groups\": [{\"items\": [],\"name\": \"New Group\",\"id\": \"1bd7efce-431e-416a-89ed-40621e3b096a\"}],\"id\": \"6586cfdfb43594ac1a0f2953\",\"isPublic\": false},{\"userId\": \"8e9db8a2-c54e-41af-a1d9-2bef2e89b38c\",\"name\": \"new packing list\",\"groups\": [{\"items\": [],\"name\": \"New Group\",\"id\": \"07bdd23c-66e4-4d85-b09f-c805d7cac340\"}], \"id\": \"6586d11fb43594ac1a0f2954\",\"isPublic\": false}]";

         var result = JsonHandler.Deserialize<List<PackingListDto2>>(dtoString);

         result!.Count.Should().Be(2);
         result.First().Id.Should().NotBeNullOrEmpty();
    }

    private PackingListDto GetNewDto()
    {
        var packingList = new PackingListModel
        {
            Id = DateTime.UtcNow.Ticks.ToString(),
            Name = DateTime.UtcNow.Ticks.ToString(),
            Groups = new List<PackingListGroupModel>
            {
                new()
                {
                    Name = DateTime.UtcNow.Ticks.ToString(),
                    Id = DateTime.UtcNow.Ticks.ToString(),
                    Items = new List<PackingListGroupItemModel>
                    {
                        new()
                        {
                            Name = DateTime.UtcNow.Ticks.ToString(),
                            Quantity = 1,
                            RefId = Guid.NewGuid().ToString(),
                            Weight = 100,
                            WeightSufix = "sufix"
                        }
                    }
                }
            },
            IsPublic = true,
            UserId = Guid.NewGuid()
        };

        var packingListMapper = new PackingListMapper();

        return packingListMapper.Map(packingList);
    }

    private PackingListMapper GetTarget()
    {
        return new PackingListMapper();
    }

    public class PackingListDto2
    {
        public string Id { get; set; } = string.Empty;
    }
}