function EventsViewModel() {
    var self = this;
    var events = [];
    self.Events = ko.observableArray(events);
    EventListVMGetAllItems(self);
    self.Create = function ()
    {
        EventListVMCreateEvent();
    }   
}
function EventListVMDeleteEvent(self,id)
{
    SPApiDelete('/api/v1/event/'+id, function (obj) {
        if (obj != null) {
            self.Events.removeAll();
            EventListVMGetAllItems(self);
        }
    });
}
function EventListVMCreateEvent()
{
    SPApiPost('/api/v1/event/',"", function (obj) {
        if (obj != null) {
             window.location.href = "/event/" + obj.Id + "?edit=true";
        }
    });
}
function EventListVMGetAllItems(self)
{
    SPApiGet('/api/v1/event/', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                EventListVMAddEvent(self,dto);
            });
        }
    });    
}
function EventListVMAddEvent(self,dto)
{
    var obj=new Object();
    obj.Name=dto.Name;
    obj.Id=dto.Id;
    obj.Link="/event/"+dto.Id;
    obj.IsOwner=dto.IsOwner;
    obj.Delete = function ()
    {
        EventListVMDeleteEvent(self,dto.Id);
    }
    self.Events.push(obj);
}
