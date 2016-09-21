//$(document).ready(function () {
//    // create DateTimePicker from input HTML element
//    $("#datetimepicker").kendoDateTimePicker({
//        value: new Date()
//    });
//});



var CreateImpactController = function ($scope, $document) {
    $scope.title = $document[0].title;
    $scope.windowTitle = angular.element(window.document)[0].title;
    $scope.models = {
        helloAngular: 'I work!'
    };

    $scope.getType = function (x) {
        return typeof x;
    };
    $scope.isDate = function (x) {
        return x instanceof Date;
    };

    $document.ready(function () {

        var startYearPicker = angular.element(document.querySelector('#startyearpicker'));
        var endYearPicker = angular.element(document.querySelector('#endyearpicker'));

        var myEl = $document.find('#startyearpicker');

        startYearPicker.kendoDatePicker({
            start: "decade",
            depth: "decade",
            format: "yyyy"
        });

        endYearPicker.kendoDatePicker({
            start: "decade",
            depth: "decade",
            format: "yyyy"
        });
    })
}

CreateImpactController.$inject = ['$scope', '$document'];
