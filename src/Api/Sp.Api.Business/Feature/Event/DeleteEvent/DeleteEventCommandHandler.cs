using MediatR;
using SP.Database.Mongo.Feature.Event;

namespace Sp.Api.Business.Feature.Event.DeleteEvent;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, string>
{
    private readonly IEventItemRepository _eventItemRepository;

    public DeleteEventCommandHandler(IEventItemRepository eventItemRepository)
    {
        _eventItemRepository = eventItemRepository;
    }

    public async Task<string> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        await _eventItemRepository.Delete(request.Id);
        return string.Empty;
    }
}