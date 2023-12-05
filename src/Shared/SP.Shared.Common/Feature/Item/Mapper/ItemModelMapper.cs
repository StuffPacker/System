using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace SP.Shared.Common.Feature.PackingList.Mapper;

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
            WeightSufix = model.WeightSufix
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
            WeightSufix = model.WeightSufix
        };
    }
}