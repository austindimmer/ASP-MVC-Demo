//$(document).ready(function () {
//    // create DateTimePicker from input HTML element
//    $("#datetimepicker").kendoDateTimePicker({
//        value: new Date()
//    });
//});



var CreateImpactController = function ($scope, $document, $http) {
    $scope.title = $document[0].title;
    $scope.windowTitle = angular.element(window.document)[0].title;

    $scope.getType = function (x) {
        return typeof x;
    };
    $scope.isDate = function (x) {
        return x instanceof Date;
    };

    $document.ready(function () {

        var startYearPicker = angular.element(document.querySelector('#startyearpicker'));
        var endYearPicker = angular.element(document.querySelector('#endyearpicker'));
        var selectedBeneficiaryGroups = angular.element(document.querySelector('#selectedBeneficiaryGroups'));

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

      

        //$http.get('/GetBlankImpactViewModel').then(successCallback, errorCallback);

        // get data from server
        //$.ajax({
        //    url: '/Impacts/GetBlankImpactViewModel/',
        //    success: function (data) {
        //        var parsedData = JSON.parse(data);
        //        $scope.impactsViewModel = parsedData;
        //        $scope.beneficiaryGroups = parsed.BeneficiaryGroups
        //    }
        //});

        $scope.selectedBeneficiaryGroup = null;
        $scope.beneficiaryGroups = [];
        $scope.impact = [];
        //        var parsedData = JSON.parse(data);
        $scope.impactViewModel = [];

        $http({
            method: 'GET',
            url: '/Impacts/GetBlankImpactViewModel/',
        }).success(function (result) {
            var parsedData = JSON.parse(result);
            $scope.impactViewModel = parsedData;
            $scope.impact = result;

            selectedBeneficiaryGroups.kendoListView({
                dataSource: $scope.impactViewModel.SelectedBeneficiaryGroups,
                template: kendo.template($("#template").html()),
                selectable: "multiple",
                change: onChange,

            });
        });


        $scope.addBeneficiaryGroup = function (selectedBeneficiaryGroup) {
            //$scope.items.splice(index, 1);
            $scope.impactViewModel.SelectedBeneficiaryGroups.push(selectedBeneficiaryGroup)
            var indexOfMatchingObject = $.inArray(selectedBeneficiaryGroup, $scope.impactViewModel.BeneficiaryGroups)
            if (indexOfMatchingObject != -1) {
                $scope.impactViewModel.BeneficiaryGroups.splice(indexOfMatchingObject, 1);
            }
        };

        function onChange() {
            var data = dataSource.view(),
                selected = $.map(this.select(), function (item) {
                    return data[$(item).index()].ProductName;
                });

            kendoConsole.log("Selected: " + selected.length + " item(s), [" + selected.join(", ") + "]");
        }

    }
    )
}


    //var req = {
    //    method: 'GET',
    //    url: '/GetBlankImpactViewModel',
    //    headers: {
    //        'Content-Type': application/json
    //    },
    //}

    //$http(req).then(function () { }, function () { });



CreateImpactController.$inject = ['$scope', '$document', '$http'];
