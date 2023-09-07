namespace SP.Web.Site.Features.Item;

public class ItemServiceFake : IItemService
{
    private List<ItemModel> itemModels;

    public ItemServiceFake()
    {
        itemModels = new List<ItemModel>();
        itemModels.Add(new ItemModel
        {
            Id = Guid.Parse("c43e1e80-29b1-4ba3-aa55-d104c1639d0f"),
            Name = "test item 1",
            WeightSufix = "g",
            Weight = Convert.ToDecimal(123.44)
        });
        itemModels.Add(new ItemModel
        {
            Id = Guid.Parse("b186d737-912c-4483-9ad2-ea454ab1e024"),
            Name = "test item 2",
            WeightSufix = "g",
            Weight = Convert.ToDecimal(456.55)
        });
    }

    public List<ItemViewModel> GetItems()
    {
        var result = new List<ItemViewModel>();
        foreach (var item in itemModels)
        {
            result.Add(new ItemViewModel(item));
        }

        return result;
    }
}