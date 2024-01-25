using SP.Shared.Common.Feature.Item.Model;

namespace SP.Web.Business.Feature.Item;

public class ItemViewModel
{
    public ItemViewModel(ItemModel model)
    {
        Name = model.Name;
        Id = model.Id.ToString();
        Weight = model.Weight;
        WeightSufix = model.WeightSufix;
        Description = model.Description;
    }

    public string Description { get; set; }

    public string WeightSufix { get; set; }

    public decimal Weight { get; set; }

    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}