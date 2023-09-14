using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Web.Site.Features.PackingList.PackingList.Mapper;

public static class PackingListGroupItemViewModelMapper
{
    public static PackingListGroupItemViewModel Map(PackingListGroupItemModel item)
    {
        return new PackingListGroupItemViewModel
        {
            Name = item.Name,
            Weight = item.Weight.ToString(),
            WeightSufix = item.WeightSufix,
            Id = item.RefId,
            Quantity = item.Quantity
        };
    }
}