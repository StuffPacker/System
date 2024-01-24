using SP.Shared.Common.Feature.Item.Model;
using SP.Shared.Common.Feature.PackingList.Dto;

namespace SP.Shared.Common.Feature.Item.Mapper;

public interface IItemModelMapper
{
    ItemDto Map(ItemModel model);

    ItemModel Map(ItemDto model);

    IEnumerable<ItemModel> Map(IEnumerable<ItemDto> model);
}