using SP.Web.Site.Features.Item;
using SP.Web.Site.Features.Packinglist;

namespace SP.Web.Site.Features.PackingList;

public class PackingListServiceFake : IPackingListService
{
    public PackingListViewModel GetPackingListById(Guid id, Guid userId)
    {
        return GetById(id, userId);
    }

    private PackingListViewModel GetById(Guid id, Guid userId)
    {
        var viewModel = GetPackingListByIdInternal(id);
        if (viewModel == null)
        {
            return null!;
        }

        if (!Access(viewModel, userId))
        {
            return null!;
        }

        return viewModel;
    }

    private bool Access(PackingListViewModel model, Guid userId)
    {
        if (Guid.Parse(model.UserId) == userId)
        {
            return true;
        }

        return false;
    }

    private PackingListViewModel GetPackingListByIdInternal(Guid id)
    {
        var packingListModel = new PackingListModel
        {
            Id = Guid.Parse("646ea801-3788-4424-8e72-3105df10c1c8"),
            Name = "Packing list 1",
            UserId = Guid.Parse("08b220c6-6ee8-4af3-a14b-9dac47bb8609"),
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