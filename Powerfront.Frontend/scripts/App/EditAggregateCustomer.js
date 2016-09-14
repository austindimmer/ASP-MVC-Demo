//var rawData = document.getElementById('inputModelData').value;
//var data = JSON.parse(decodeURIComponent(div.getAttribute("data-json")))
//console.log(data)

//$.post("/some/url", data, function (returnedData) {
//    // This callback is executed if the post was successful
//})

var aggregateCustomer;
var customerId;





$(document).ready(function () {
    // when the DOM has fully loaded...

    customerId = document.getElementById('inputModelData').value;
    // get data from server
    $.ajax({
        url: '/Maintenance/GetJsonByCustomerId/' + customerId,
        success: function (data) {
            var aggregateCustomerString = Cereal.stringify(data);
            aggregateCustomer = Cereal.parse(aggregateCustomerString);
            ko.applyBindings(
     aggregateCustomer
 );
        }

    });




    $("#saveJsonButton").bind("click", function () {
        var onEventPostJsonData = new postJsonData();
        onEventPostJsonData.launchPostJsonData();
    });
});

//options.OnBegin = "onBeginSave";
//options.OnComplete = "onCompleteSave";
//options.OnFailure = "onFailureSave";
//options.OnSuccess = "onSuccessSave";

function onBeginSave() {
    console.log("onBeginSave");
    postJsonData();
}
function onCompleteSave() {
    console.log("onCompleteSave");
}
function onFailureSave() {
    console.log("onFailureSave");
}
function onSuccessSave() {
    console.log("onSuccessSave");
}


function postJsonData() {
    this.launchPostJsonData = function () {

        var updatedData = JSON.stringify(aggregateCustomer);

        $.ajax({
            dataType: 'json', // expected format for response
            contentType: 'application/json; charset=utf-8', // send as JSON
            beforeSend: function (xhr) {
                xhr.setRequestHeader('content-type', 'application/json');
            },
            type: 'POST',
            url: '/Maintenance/EditPostedJson/',
            data: updatedData
        })
            .done(function (data) {
                console.log("Response " + JSON.stringify(data));
            })
        .success(function (result) {
            console.log("Response " + JSON.stringify(result));
            new PNotify({
                title: 'Customer Details Saved',
                text: 'The customer record was updated!',
                type: 'success',
                animate_speed: 'fast'
            });
        })
        .error(function (request, textStatus, errorThrown) {
            console.log('textStatus ' + textStatus);
            console.log('errorThrown ' + errorThrown);
            new PNotify({
                title: 'Customer Details Not Saved',
                text: 'There appears to have been an error. Please try again later.',
                type: 'error',
                animate_speed: 'fast'
            });
        });



    }
}