using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Mapper;

namespace SP.Shared.Common.Feature.Item.Mapper;

public class ItemModelMapper : IItemModelMapper
{
    public ItemDto Map(ItemModel model)
    {
        return new ItemDto
        {
            Name = model.Name,
            Id = model.Id,
            UserId = model.UserId.ToString(),
            Weight = model.Weight,
            WeightSufix = model.WeightSufix,
            Description = model.Description
        };
    }

    public ItemModel Map(ItemDto model)
    {
        return new ItemModel
        {
            Name = model.Name,
            Id = model.Id,
            UserId = Guid.Parse(model.UserId),
            Weight = model.Weight,
            WeightSufix = model.WeightSufix,
            Description = model.Description
        };
    }

    public IEnumerable<ItemModel> Map(IEnumerable<ItemDto> dtos)
    {
        var list = new List<ItemModel>();
        foreach (var item in dtos)
        {
            list.Add(Map(item));
        }

        return list;
    }
}