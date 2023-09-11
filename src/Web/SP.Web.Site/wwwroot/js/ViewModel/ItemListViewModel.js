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
    GetAllItems(self);   
    self.Create = function ()
    {
        CreateUserItem(self);
    }    
}
function CreateUserItem(self)
{
    SPApiPost('/api/v1/items/',"", function (obj) {
        if (obj != null) {            
                AddItem(self,obj);            
        }
    });    
}
function GetAllItems(self)
{
    SPApiGet('/api/v1/items/', function (obj) {
        if (obj != null) {               
            ko.utils.arrayForEach(obj, function (dto) {
                AddItem(self,dto);
            });
        }
    });
}
function AddItem(self,dto)
{
    var item = new Object();
    item.Name=dto.Name;
    item.Weight=dto.Weight;
    item.WeightSufix=dto.WeightSufix;
    item.Edit = function ()
    {
        self.EditItem(true);
        var m = self.EditModel();
        m.Name=dto.Name;
        m.Id = dto.Id;
        m.Save = function ()
        {
            var data= {};
            data.Name=m.Name;            
            SPApiPut('/api/v1/items/'+m.Id,data, function (obj) {
                if (obj != null) {
                    self.Items.removeAll();
                    GetAllItems(self);
                    self.EditItem(false);
                }
            });
        };
            m.Cancle=function ()
            {
                m.Name="";
                m.Id = "";
                self.EditItem(false);
            };
        self.EditModel(m);
    }
    self.Items.push(item);
}
