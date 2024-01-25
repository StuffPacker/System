function ItemViewModel(id) {
    var self = this;
    self.Name=ko.observable("")
    self.Weight=ko.observable("")
    self.WeightSufix=ko.observable("")
    self.Description=ko.observable("")
    self.EditMode=ko.observable(false);
    IVMGetItem(self,id);
    self.ChangeEditMode = function ()
    {        
        self.EditMode(!self.EditMode());
    }
    self.Cancel = function ()
    {      
        self.EditMode(!self.EditMode());        
    }
    self.Save = function ()
    {
      IVMSaveItem(self,id);
        self.EditMode(!self.EditMode());
    }
}
function IVMGetItem(self,id)
{  
    SPApiGet('/api/v1/items/'+id, function (obj) {
        if (obj != null) {         
              
                self.Name(obj.Name);
            self.Weight(obj.Weight);
            self.WeightSufix(obj.WeightSufix);
            self.Description(obj.Description);                 
        }
    });
}
function IVMSaveItem(self,id)
{
    var item = new Object();
    item.Name = self.Name();
    item.Weight=self.Weight().toString();
    item.Description=self.Description();   
    console.log(item);
    SPApiPut('/api/V1/items/'+id,item,function (obj){
       if(obj==null)
       {
           alert("Saved");
       }
    });    
}