using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList.Mapper;

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

    public static PackingListGroupItemModel Map(PackingListGroupItemViewModel item)
    {
        return new PackingListGroupItemModel
        {
            Name = item.Name,
            Weight = Convert.ToDecimal(item.Weight),
            WeightSufix = item.WeightSufix,
            RefId = item.Id,
            Quantity = item.Quantity
        };
    }
}