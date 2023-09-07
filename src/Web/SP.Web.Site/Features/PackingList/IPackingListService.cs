using SP.Web.Site.Features.Packinglist;

namespace SP.Web.Site.Features.PackingList;

public interface IPackingListService
{
    PackingListViewModel GetPackingListById(Guid parse);
}