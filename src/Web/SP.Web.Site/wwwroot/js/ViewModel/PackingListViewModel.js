function PackingListViewModel(id) {
    var self = this;
    var existingItems = [];
    self.ExistingItemsList = existingItems;
    PLVMLoadExistingItems(self);
    var Groups = [];
    self.Name = ko.observable("");
    self.Groups = ko.observableArray(Groups);
    self.EditName = ko.observable(false);
    self.IsPublic=ko.observable(false);
    self.PublicLink=ko.observable("");       
    self.PrintLink=ko.observable("/packinglist/"+ id +"/print");
    self.MakePublic=function ()
    {
        var data = {};
        data.MakePublic = true;
        SPApiPatch('/api/v1/packinglist/' + id + '/public', data, function (obj) {
            if (obj != null) {
                self.PublicLink("/packinglist/"+id+"/public");
                self.IsPublic(true);
            }
        });
        
    }
    self.MakePrivate=function ()
    {
        var data = {};
        data.MakePublic = false;
        SPApiPatch('/api/v1/packinglist/' + id + '/public', data, function (obj) {
            if (obj != null) {
                self.IsPublic(false);
                self.PublicLink("");
            }
        });
    }

    PLVMGetPackingList(self, id);
    self.ChangeEditNameState = function () {
        self.EditName(!self.EditName());
    }
    self.SaveName = function () {
        var data = {};
        data.Name = self.Name();       
        SPApiPatch('/api/v1/packinglist/' + id + '/Name', data, function (obj) {
            if (obj != null) {

            }
        });
        self.EditName(!self.EditName());
    }
    self.CancelName = function () {
        self.EditName(!self.EditName());
    }
    self.AddNewGroup = function () {
        SPApiPost('/api/v1/packinglist/'+id+'/group',"", function (obj) {
            if (obj != null) {
                PLVMAddGroup(self,id,obj);
            }
        });       
    }    
} 
function PLVMAddExistingItems  (p1,p2)
{
    alert("group: "+item.Id);
}
function PLVMLoadExistingItems(self)
{
    SPApiGet('/api/v1/items/', function (obj) {
        if (obj != null) {          
            ko.utils.arrayForEach(obj, function (dto) {
                var item = new Object();
                item.Name=dto.Name;
                item.Id=dto.Id;                
                self.ExistingItemsList.push(item);
            });
        }
    });
}
function PLVMGetPackingList(self,id)
{    
    SPApiGet('/api/v1/packinglist/'+id, function (obj) {
        if (obj != null) {
            self.Name(obj.Name);   
            self.IsPublic(obj.IsPublic);
            if(self.IsPublic())
            {
                self.PublicLink("/packinglist/"+id+"/public");
            }
           
            ko.utils.arrayForEach(obj.Groups, function (dto) {
                PLVMAddGroup(self,id,dto);
            });
        }
    });
}
function PLVMAddGroup(self,id,dto)
{   
    var items = [];
    var item = new Object();
    item.Name=dto.Name;
    item.Id=dto.Id;    
    item.EditGroupName = ko.observable(false);
    item.ChangeEditGroupNameState = function (){item.EditGroupName(!item.EditGroupName());}
    item.Save = function (){
        var data = {};
        data.Name = item.Name;
        SPApiPatch('/api/v1/packinglist/' + id + '/Group/'+dto.Id+'/Name', data, function (obj) {
            if (obj != null) {
                self.Groups.removeAll();
                PLVMGetPackingList(self,id);
            }
        });
        
    }
    item.Cancel = function (){item.EditGroupName(!item.EditGroupName());}
    item.Items=ko.observableArray(items); 
     ko.utils.arrayForEach(dto.Items, function (dto2) {
         PLVMAddItem(self,item,id,item.Id,dto2);
         
     });
     item.Delete= function ()
     {
         SPApiDelete('/api/v1/packinglist/'+id+'/Group/'+item.Id,function (obj) {
             if (obj != null) {
                 self.Groups.removeAll();
                 PLVMGetPackingList(self,id);
             }
         });
     }
    var existingItems = [];
    item.ExistingItems = ko.observableArray(existingItems);
    
     ko.utils.arrayForEach(self.ExistingItemsList, function (existingItemsListItem) {
         var eitem = new Object();
         eitem.Name=existingItemsListItem.Name;
         eitem.Id=existingItemsListItem.Id;
         item.AddToList=function ()
         {             
             var addItemdata = {};
             addItemdata.Id = item.ExistingItemsSelectedValue().Id;   
             SPApiPost('/api/v1/packinglist/'+id+'/group/'+item.Id+'/item',addItemdata, function (obj) {
                 if (obj != null) {
                     var list=[];
                     var itemExist=false;
                     ko.utils.arrayForEach(item.Items(), function (i) {  
                        if(i.Id==obj.Id)
                        {
                            itemExist=true; 
                            i.Quantity=obj.Quantity;
                        }    
                        list.push(i);
                     });
                     if(!itemExist)
                     {
                         PLVMAddItem(self,item,id,item.Id,obj);
                        
                     }

                     PLVMReloadItems(self,item,item.Items);
                 }
             });             
         }        
         
         item.ExistingItems.push(eitem);
         item.ExistingItemsSelectedValue=ko.observable("");
    });
   
     
     
    self.Groups.push(item);
}
function PLVMReloadItems(self,group,items)
{
    
    var list=[];
    ko.utils.arrayForEach(items(), function (i) {
        list.push(i);
    });


    group.Items.removeAll();
    ko.utils.arrayForEach(list, function (i) {
        group.Items.push(i);
    });
}
function PLVMAddItem(self,group,id,groupid,dto)
{   
    
    var item = new Object();
    item.Id=dto.Id;
    item.Name=dto.Name;
    item.Weight=dto.Weight;
    item.WeightSufix=dto.WeightSufix;
    item.LinkBack="/item/"+dto.Id;
    item.Quantity=dto.Quantity;
    item.Save=function ()
    {
        var data = {};
        data.Quantity = item.Quantity;
        SPApiPatch('/api/v1/packinglist/' + id + '/group/'+groupid+'/item/'+dto.Id+'/Quantity', data, function (obj) {
            if (obj != null) {
alert("Saved");
            }
        });
    }    
    item.Delete=function ()
    {
        SPApiDelete('/api/v1/packinglist/'+id+'/Group/'+groupid+'/item/'+dto.Id,function (obj) {
            if (obj != null) {
                self.Groups.removeAll();
                PLVMGetPackingList(self,id);
            }
        });
    }
    group.Items.push(item);
}
