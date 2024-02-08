function UserViewModel(id)
{
    var self = this;
    self.Name = ko.observable("");
    self.IsOwner = ko.observable(false);
    self.EditMode=ko.observable(false);
    UsrVMGetPackingList(self, id);
    self.ChangeEditMode= function ()
    {
        console.log("change edit1");
        self.EditMode(!self.EditMode());
    }
    self.Update=function ()
    {
     var obj=new Object();
     obj.Name=self.Name();
        SPApiPatch('/api/v1/user/' + id, obj, function (obj) {
            if (obj != null) { 
                self.EditMode(!self.EditMode());
                alert("saved!");
            }
        });
    }
}
function UsrVMGetPackingList(self, id) {
    SPApiGet('/api/v1/user/' + id, function (obj) {
        if (obj != null) {
            self.Name(obj.Name);
            self.IsOwner(obj.IsOwner);
        }
    });
}