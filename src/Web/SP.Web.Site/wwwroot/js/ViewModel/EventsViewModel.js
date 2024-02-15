function EventsViewModel() {
    var self = this;
    var events = [];
    self.Events = ko.observableArray(events);
    EventListVMGetAllItems(self);
    self.Create = function ()
    {
        EventListVMCreateUserItem();
    }
}
function EventListVMCreateUserItem()
{
    SPApiPost('/api/v1/event/',"", function (obj) {
        if (obj != null) {
            // window.location.href = "/event/" + obj.Id + "?edit=true";
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
    obj.Link="/event/"+dto.Id
    self.Events.push(obj);
}
