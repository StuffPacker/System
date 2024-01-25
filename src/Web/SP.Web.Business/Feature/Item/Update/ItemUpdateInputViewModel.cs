namespace SP.Web.Business.Feature.Item.Update;

public class ItemUpdateInputViewModel
{
    public ItemUpdateInputViewModel()
    {
    }

    public string Name { get; set; } = string.Empty;

    public string Weight { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}