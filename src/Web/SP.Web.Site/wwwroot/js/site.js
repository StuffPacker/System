function ManageResultData(obj) {
    const code = obj.Meta.Code;
    if (code == "200") {
        return obj.ResultData;
    }
    if (code == "400") {
        alert("Error valedating data", obj.ResultData);
        return null;
    }
    if (code == "404") {
        window.location.replace("/404");
    }
    else {
        alert("A error has accured, please try later");
    }
}
function SPApiPost(url,data, callback) {
    fetch(url, {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })

        .then(response => response.json())
        .then(response => {
            var obj = ManageResultData(response);
            callback(obj);
        });
}
function SPApiGet(url, callback) {

    fetch(url)
        .then(response => response.json())
        .then(response => {

            var obj = ManageResultData(response);
            callback(obj);
        });
}
function SPApiPut(url,data, callback) {
    fetch(url, {
        method: 'PUT',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })

        .then(response => response.json())
        .then(response => {
            var obj = ManageResultData(response);
            callback(obj);
        });
}
function SPApiDelete(url, callback) {

    fetch(url,
        {
            method: 'DELETE'
        })
        .then(response => response.json())
        .then(response => {

            var obj = ManageResultData(response);
            callback(obj);
        });
}
