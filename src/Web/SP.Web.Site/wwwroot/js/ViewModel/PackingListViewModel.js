function PackingListViewModel(id) {
    var self = this;
    var l1 = [];
    self.Items = ko.observableArray(l1);
    self.SelectedGroup = ko.observable("");
    PLVMLoadExistingItems(self);
    var Groups = [];
    self.Name = ko.observable("");
    self.Groups = ko.observableArray(Groups);
    self.EditName = ko.observable(false);
    self.IsPublic = ko.observable(false);
    self.PublicLink = ko.observable("");
    self.PrintLink = ko.observable("/packinglist/" + id + "/print");
    self.SaveItemsFromModalToGroup = function () {
        var obj = {};
        obj.SelectedItems = [];
        ko.utils.arrayForEach(self.Items(), function (i) {
            if (i.Selected == true) {
                obj.SelectedItems.push(i.Id);
            }
        });
        SPApiPatch('/api/v1/packinglist/' + id + '/group/' + self.SelectedGroup() + '/item', obj, function (obj) {
            if (obj != null) {
                PLVMGetPackingList(self, id);
                var modal = $('#AddItemToPackingListModal');
                modal.modal('toggle');
            }
        });
    }
    self.MakePublic = function () {
        var data = {};
        data.MakePublic = true;
        SPApiPatch('/api/v1/packinglist/' + id + '/public', data, function (obj) {
            if (obj != null) {
                self.PublicLink("/packinglist/" + id + "/public");
                self.IsPublic(true);
            }
        });

    }
    self.MakePrivate = function () {
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
        SPApiPost('/api/v1/packinglist/' + id + '/group', "", function (obj) {
            if (obj != null) {
                PLVMAddGroup(self, id, obj);
            }
        });
    }
}

function PLVMLoadExistingItems(self) {
    SPApiGet('/api/v1/items/', function (obj) {
        if (obj != null) {
            ko.utils.arrayForEach(obj, function (dto) {
                var item = {};
                item.Name = dto.Name;
                item.Id = dto.Id;
                item.Selected = false;
                self.Items.push(item);
            });
        }
    });
}

function PLVMGetPackingList(self, id) {
    SPApiGet('/api/v1/packinglist/' + id, function (obj) {
        if (obj != null) {
            self.Name(obj.Name);
            self.IsPublic(obj.IsPublic);
            if (self.IsPublic()) {
                self.PublicLink("/packinglist/" + id + "/public");
            }
self.Groups.removeAll();
            ko.utils.arrayForEach(obj.Groups, function (dto) {
                PLVMAddGroup(self, id, dto);
            });
        }
    });
}

function PLVMAddGroup(self, id, dto) {
    var items = [];
    var item = {};
    item.Name = dto.Name;
    item.Id = dto.Id;
    item.EditGroupName = ko.observable(false);
    item.ChangeEditGroupNameState = function () {
        item.EditGroupName(!item.EditGroupName());
    }
    item.Save = function () {
        var data = {};
        data.Name = item.Name;
        SPApiPatch('/api/v1/packinglist/' + id + '/Group/' + dto.Id + '/Name', data, function (obj) {
            if (obj != null) {
                self.Groups.removeAll();
                PLVMGetPackingList(self, id);
            }
        });

    }
    item.Cancel = function () {
        item.EditGroupName(!item.EditGroupName());
    }
    item.Items = ko.observableArray(items);
    ko.utils.arrayForEach(dto.Items, function (dto2) {
        PLVMAddItem(self, item, id, item.Id, dto2);

    });
    item.Delete = function () {
        SPApiDelete('/api/v1/packinglist/' + id + '/Group/' + item.Id, function (obj) {
            if (obj != null) {
                self.Groups.removeAll();
                PLVMGetPackingList(self, id);
            }
        });
    }
    item.AddToList = function () {
        self.SelectedGroup(item.Id);
        var arr = [];
        ko.utils.arrayForEach(self.Items(), function (i) {
            var obj = {};
            obj.Id = i.Id;
            obj.Name = i.Name;
            var exist = false;
            ko.utils.arrayForEach(dto.Items, function (i1) {
                if (i.Id == i1.Id) {
                    exist = true;
                }
            });
            if (exist == true) {
                obj.Selected = true;
            } else {
                obj.Selected = false;
            }
            arr.push(obj);
        });
        self.Items.removeAll();
        ko.utils.arrayForEach(arr, function (i) {
            self.Items.push(i);
        });
        var modal = $('#AddItemToPackingListModal');
        modal.modal('toggle');
    }
    self.Groups.push(item);
}

function PLVMReloadItems(self, group, items) {

    var list = [];
    ko.utils.arrayForEach(items(), function (i) {
        list.push(i);
    });


    group.Items.removeAll();
    ko.utils.arrayForEach(list, function (i) {
        group.Items.push(i);
    });
}

function PLVMAddItem(self, group, id, groupid, dto) {

    var item = {};
    item.Id = dto.Id;
    item.Name = dto.Name;
    item.Weight = dto.Weight;
    item.WeightSufix = dto.WeightSufix;
    item.LinkBack = "/item/" + dto.Id;
    item.Quantity = dto.Quantity;
    item.Save = function () {
        var data = {};
        data.Quantity = item.Quantity;
        SPApiPatch('/api/v1/packinglist/' + id + '/group/' + groupid + '/item/' + dto.Id + '/Quantity', data, function (obj) {
            if (obj != null) {
                alert("Saved");
            }
        });
    }
    item.Delete = function () {
        SPApiDelete('/api/v1/packinglist/' + id + '/Group/' + groupid + '/item/' + dto.Id, function (obj) {
            if (obj != null) {
                self.Groups.removeAll();
                PLVMGetPackingList(self, id);
            }
        });
    }
    group.Items.push(item);
}
