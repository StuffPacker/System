using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Shared.Common.Feature.PackingList.Mapper;

public interface IPackingListMapper
{
    PackingListDto Map(PackingListModel model);

    PackingListModel Map(PackingListDto dto);
}