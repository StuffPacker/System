function ItemListViewModel() {
    var self = this;
    var Items = [];
    self.Items = ko.observableArray(Items);   
    var editModel=new Object();
    editModel.Name="";
    editModel.Id="";
    editModel.Save = function (){};
    editModel.Cancle = function (){};
    self.EditModel = ko.observable(editModel);
    self.EditItem=ko.observable(false);
    ILVMGetAllItems(self);   
    self.Create = function ()
    {
        ILVMCreateUserItem(self);
    }    
}
function ILVMCreateUserItem(self)
{
    SPApiPost('/api/v1/items/',"", function (obj) {
        if (obj != null) {
            ILVMAddItem(self,obj);            
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
        self.EditItem(true);
        var m = self.EditModel();
        m.Name=dto.Name;
        m.Id = dto.Id;
        m.Weight = dto.Weight;
        m.Save = function ()
        {
            var data= {};
            data.Name=m.Name; 
            data.Weight=m.Weight.toString();
            
            SPApiPut('/api/v1/items/'+m.Id,data, function (obj) {
                if (obj != null) {
                    self.Items.removeAll();
                    ILVMGetAllItems(self);
                    self.EditItem(false);
                }
            });
        };
            m.Cancle=function ()
            {
                m.Name="";
                m.Id = "";
                m.Weight="";
                self.EditItem(false);
            };
        self.EditModel(m);
    }
    self.Items.push(item);
}
