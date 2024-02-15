using SP.Shared.Common.Feature.Event.Model;

namespace SP.Web.Business.Feature.Event;

public class EventViewModelMapper
{
    public static List<EventViewModel> Map(List<EventModel> list)
    {
        var l1 = new List<EventViewModel>();
        foreach (var item in list)
        {
            l1.Add(Map(item));
        }

        return l1;
    }

    public static EventViewModel Map(EventModel model)
    {
        return new EventViewModel
        {
            Id = model.Id,
            Name = model.Name
        };
    }
}