//$(document).ready(function () {
//    // create DateTimePicker from input HTML element
//    $("#datetimepicker").kendoDateTimePicker({
//        value: new Date()
//    });
//});



var CreateImpactController = function ($scope, $document, $http, $compile, $event) {
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
        $scope.beneficiaryGroupToRemove = null;
        $scope.beneficiaryGroups = [];
        $scope.selectedBeneficiaryGroups = [];
        //        var parsedData = JSON.parse(data);
        $scope.impactViewModel = [];

        $scope.selectedBeneficiaryGroupSource = new kendo.data.DataSource({});
        $scope.selectedBeneficiaryGroupsTemplate = $("#selectedBeneficiaryGroupsTemplate").html();



        $http({
            method: 'GET',
            url: '/Impacts/GetBlankImpactViewModel/',
        }).success(function (result) {
            var parsedData = JSON.parse(result);
            $scope.impactViewModel = parsedData;

            $scope.selectedBeneficiaryGroupSource = new kendo.data.DataSource({
                data: $scope.impactViewModel.SelectedBeneficiaryGroups,
                pageSize: 21
            });

            //var date = new Date(parseInt(object.MyDate.substr(6)));
            var myDateObject = new Object({
                MyDate: $scope.impactViewModel.StartDate
            });
            var parsedStartDate = new Date(parseInt(myDateObject.MyDate.substr(6)));
            myDateObject.MyDate = $scope.impactViewModel.FinishDate;
            var parsedFinishDate = new Date(parseInt(myDateObject.MyDate.substr(6)));
            $scope.impactViewModel.StartDate = parsedStartDate;
            $scope.impactViewModel.FinishDate = parsedFinishDate;
            var isStartDateDate = $scope.isDate($scope.impactViewModel.StartDate);
            kendo.bind($("startyearpicker"), $scope.impactViewModel);

            startYearPicker.kendoDatePicker({
                start: "decade",
                depth: "decade",
                format: "yyyy",
                value: $scope.impactViewModel.StartDate,
                min: $scope.impactViewModel.StartDate,
            });

            endYearPicker.kendoDatePicker({
                start: "decade",
                depth: "decade",
                format: "yyyy",
                value: $scope.impactViewModel.FinishDate,
                min: $scope.impactViewModel.StartDate,
            });


            //$("#selectedBeneficiaryGroupsList").kendoListView({
            //    dataSource: $scope.selectedBeneficiaryGroupSource,
            //    template: $("#selectedBeneficiaryGroupsTemplate").html(),
            //    selectable: "multiple",
            //    change: onChange,

            //});
        });


        $scope.addBeneficiaryGroup = function (selectedBeneficiaryGroup) {
            if (selectedBeneficiaryGroup != null) {
            $scope.impactViewModel.SelectedBeneficiaryGroups.push(selectedBeneficiaryGroup)
            var indexOfMatchingObject = $.inArray(selectedBeneficiaryGroup, $scope.impactViewModel.BeneficiaryGroups)
            if (indexOfMatchingObject != -1) {
                $scope.impactViewModel.BeneficiaryGroups.splice(indexOfMatchingObject, 1);
            }
            //selectedBeneficiaryGroups = $scope.impactViewModel.SelectedBeneficiaryGroups;

            //$("#selectedBeneficiaryGroupsList").kendoListView({
            //    dataSource: {
            //        data: $scope.impactViewModel.SelectedBeneficiaryGroups
            //    },
            //    template: $("#selectedBeneficiaryGroupsTemplate").html(),
            //    selectable: "multiple",
            //    change: onChange
            //});

            }

            $scope.selectedBeneficiaryGroupSource = new kendo.data.DataSource({
                data: $scope.impactViewModel.SelectedBeneficiaryGroups,
                pageSize: 21,
                selectable: "multiple",
                //change: onChangeAngular
            });


        };

        $scope.removeBeneficiaryGroup = function ($event) {
            var targetId = $event.target.id;
            var selectedBeneficiaryGroupId = $event.target.value;
            var selectedBeneficiaryGroup = null;
            var targetIdParent = $event.target.parentElement;

            targetIdParent.remove();

            var indexOfMatchingObject = indexOfId($scope.impactViewModel.SelectedBeneficiaryGroups, selectedBeneficiaryGroupId);
            let index = $scope.impactViewModel.SelectedBeneficiaryGroups.map((el) => el.BeneficiaryGroupId).indexOf(selectedBeneficiaryGroupId)

            if (indexOfMatchingObject != -1) {
                selectedBeneficiaryGroup = $scope.impactViewModel.SelectedBeneficiaryGroups[indexOfMatchingObject];
                $scope.impactViewModel.SelectedBeneficiaryGroups.splice(indexOfMatchingObject, 1);
                $scope.impactViewModel.BeneficiaryGroups.push(selectedBeneficiaryGroup)
            }
        };


        function indexOfId(array, id) {
            for (var i = 0; i < array.length; i++) {
                if (array[i].BeneficiaryGroupId == id) return i;
            }
            return -1;
        }

        function onChangeAngular() {
            var data = $scope.selectedBeneficiaryGroupSource.view(),
                selected = $.map(this.select(), function (item) {
                    return data[$(item).index()].BeneficiaryGroupDescription;
                });

            console.log("Selected: " + selected.length + " item(s), [" + selected.join(", ") + "]");
        }

        function onClicked($scope) {
            // Set the initial X/Y values.
            $scope.mouseX = "N/A";
            $scope.mouseY = "N/A";
            // When the document is clicked, it will invoke
            // this method, passing-through the jQuery event.
            $scope.handleClick = function( event ){
                $scope.mouseX = event.pageX;
                $scope.mouseY = event.pageY;
            };
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



CreateImpactController.$inject = ['$scope', '$document', '$http', '$compile'];
