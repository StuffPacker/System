namespace SP.Web.Site.Features.Item;

public class ItemViewModel
{
    public ItemViewModel(ItemModel model)
    {
        Name = model.Name;
        Id = model.Id.ToString();
        Weight = model.Weight;
        WeightSufix = model.WeightSufix;
    }

    public string WeightSufix { get; set; }

    public decimal Weight { get; set; }

    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}