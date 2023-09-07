using SP.Web.Site.Features.Item;
using SP.Web.Site.Features.Packinglist;

namespace SP.Web.Site.Features.PackingList;

public class PackingListServiceFake : IPackingListService
{
    public PackingListViewModel GetPackingListById(Guid id)
    {
        return GetById(id);
    }

    private PackingListViewModel GetById(Guid id)
    {
        var packingListModel = new PackingListModel
        {
            Id = Guid.Parse("646ea801-3788-4424-8e72-3105df10c1c8"),
            Name = "Packing list 1",
            Groups = new List<PackListGroupModel>
            {
                new PackListGroupModel
                {
                    Name = "Group 1",
                    Items = new List<PackingListGroupItemModel>()
                    {
                        new PackingListGroupItemModel
                        {
                            Name = "item 1",
                            Weight = Convert.ToDecimal(123.44),
                            WeightSufix = "g"
                        }
                    }
                },
                new PackListGroupModel
                {
                    Name = "Group 2",
                    Items = new List<PackingListGroupItemModel>()
                }
            }
        };
        if (id == Guid.Parse("646ea801-3788-4424-8e72-3105df10c1c8"))
        {
            return new PackingListViewModel(packingListModel);
        }

        return null!;
    }
}