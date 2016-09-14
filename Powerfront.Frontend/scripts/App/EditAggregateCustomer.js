//var rawData = document.getElementById('inputModelData').value;
//var data = JSON.parse(decodeURIComponent(div.getAttribute("data-json")))
//console.log(data)

//$.post("/some/url", data, function (returnedData) {
//    // This callback is executed if the post was successful
//})

var aggregateCustomer;
var customerId;



//$.ajax({
//    dataType: "json",
//    contentType: "application/json",
//    type: 'POST',
//    url: '/Maintenance/Edit',
//    data: { 'items': JSON.stringify(lineItems), 'id': documentId }
//});



$(document).ready(function () {
    // when the DOM has fully loaded...

    customerId = document.getElementById('inputModelData').value;
    // get data from server
    $.ajax({
        url: '/Maintenance/GetJsonByCustomerId/' + customerId,
        success: function (data) {
            aggregateCustomer = data;
        }
       
    });


    $("#saveButton").bind("click", function () {
        var onEventLaunchSquirrel = new postSquirrel();
        onEventLaunchSquirrel.launchSquirrel();
    });
});
function postSquirrel() {
    this.launchSquirrel = function () {

        // fetch values from input
        var name = $("Name").val();
        var age = $("Age").val();
        var acorns = $("Acorns").val();
        var gender = $("Gender").val();
        var hobby = $("Hobby").val();

        // build json object
        var squirrel = {
            Name: name,
            Age: age,
            Acorns: acorns,
            Gender: gender,
            Hobby: hobby
        };

        $.ajax({
            type: "POST",
            url: "home/PostSquirrel",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(squirrel),
            success: function (data) { console.log(data) },
            error: function (data) { console.log(data) }
        });

    }
}