function ItemListViewModel() {
    var self = this;
    var Items = [];
    self.Items = ko.observableArray(Items);
    self.EditItem=ko.observable(false);
    ILVMGetAllItems(self);   
    self.Create = function ()
    {
        ILVMCreateUserItem();
    }    
}
function ILVMCreateUserItem()
{
    SPApiPost('/api/v1/items/',"", function (obj) {
        if (obj != null) {           

            window.location.href = "/item/" + obj.Id + "?edit=true";
        }
    });    
}
function ILVMGetAllItems(self)
{
    SPApiGet('/api/v1/items/', function (obj) {
        if (obj != null) {               
            ko.utils.arrayForEach(obj, function (dto) {
                ILVMAddItem(self,dto);
            });
        }
    });
}
function ILVMAddItem(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Weight=dto.Weight;
    item.WeightSufix=dto.WeightSufix;
    item.Link="/item/"+dto.Id;
    item.Description=dto.Description;
    item.Delete = function ()
    {
        SPApiDelete('/api/v1/items/'+dto.Id,function (obj) {
            if (obj != null) {
                self.Items.removeAll();
                ILVMGetAllItems(self);                
            }
        });
    }
    item.Edit = function ()
    {
        window.location.href = "/item/" + dto.Id + "?edit=true";        
    }
    self.Items.push(item);
}
