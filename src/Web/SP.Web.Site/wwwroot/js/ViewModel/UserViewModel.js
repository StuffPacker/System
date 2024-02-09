function UserViewModel(id)
{
    var self = this;
    self.Name = ko.observable("");
    self.IsOwner = ko.observable(false);
    self.EditMode=ko.observable(false);
    UsrVMGetUser(self, id);
    var packingListsPublic = [];
    self.PackingListsPublic=ko.observableArray(packingListsPublic);
    UsrVMGetPublicPackingList(self, id);
    
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
function UsrVMGetUser(self, id) {
    SPApiGet('/api/v1/user/' + id, function (obj) {
        if (obj != null) {
            self.Name(obj.Name);
            self.IsOwner(obj.IsOwner);
        }
    });
}
function UsrVMGetPublicPackingList(self,id)
{
    SPApiGet('/api/v1/user/' + id + "/publicPackingList/", function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                UsrVMAddPublicPackingList(self,dto);
            });
        }
    });
}
function UsrVMAddPublicPackingList(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Link="/packinglist/" + dto.Id+"/public/";
    item.Lang=dto.Language;
    item.FlagUrl="/img/flag/"+item.Lang+".png";
    self.PackingListsPublic.push(item);    
}