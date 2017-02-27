ageWebAPI.controller('IndexController', function ($scope, $routeParams, $http, AgeServices, WaitSpinner) {

	$scope.previousUsers = [];
	$scope.visitorsDisplayList = [];
  $scope.calculationData = [];

  angular.element(document).ready(function() {

  $scope.userFirstName = '';
  $scope.userLastName = '';
	$scope.dobDateTime;

  $scope.resultMessage = '';

  $scope.initialize();

  });

  $scope.initialize = function () {

    //WaitSpinner.startAtPosition('mapPanel', '20%', '48%');
    $scope.fetchPreviousVisitors();

  };

  $scope.fetchPreviousVisitors = function () {

    var clientDateTime = $scope.getClientDateTime();

    var formData = {
      'clientDateTime' : clientDateTime
    };

    $.when(AgeServices.getVisitors(formData)).then(
      function (data) {
        $scope.previousUsers = AgeServices.getPreviousVisitors();
        $scope.populateVisitorsDisplayList($scope.previousUsers);
      }, 
      function (data) {
        alert('Could not retrieve visitors data from server.')
      });
  };

  $scope.getClientDateTime = function () {
    var currentDate = new Date();
    var clientDateTime = currentDate.getFullYear() + "-" + $scope.zeroAdjustedValue((currentDate.getMonth() + 1)) + "-" + $scope.zeroAdjustedValue(currentDate.getDate()) + " " + $scope.zeroAdjustedValue(currentDate.getHours()) + ":" + $scope.zeroAdjustedValue(currentDate.getMinutes()) + ":" + $scope.zeroAdjustedValue(currentDate.getSeconds());
    return clientDateTime;
  };

  $scope.goCalculateAge = function () {

    if(!$scope.isDataValid()){
      return;
    }

    var clientDateTime = $scope.getClientDateTime();

    var formData = {
      'FirstName' : $scope.userFirstName,
      'LastName' : $scope.userLastName,
      'DateOfBirth' : $scope.dobDateTime,
      'clientDateTime' : clientDateTime
    };

    $.when(AgeServices.sendUserData(formData)).then(
      function (data) {
        $scope.calculationData = AgeServices.getCalculationsData();
        $scope.displayAgeData($scope.calculationData);
      }, 
      function (data) {
        alert('Could not retrieve age calculation data from server.')
      });
    
  };

  $scope.isDataValid = function () {
    return true;
  };


    $scope.populateVisitorsDisplayList = function (visitors) {
    	angular.forEach(visitors, function (visitor){

        var displayValue = visitor["FirstName"] + ' ' + visitor["LastName"];
        displayValue += ' - Age : ';
        var age = visitor["Age"];
        displayValue += age["Year"] + ' Year(s), ';
        displayValue += age["Month"] + ' Month(s), ';
        displayValue += age["Day"] + ' Day(s) and ';
        displayValue += age["Hour"] + ' Hour(s). ';

    		$scope.addVisitorForDisplay(visitor["UserId"], displayValue);
    	});

    };

    $scope.addVisitorForDisplay = function (visitor_Id, visitor_name) {
      var found = false;
      angular.forEach($scope.visitorsDisplayList, function (visitor){
        if(visitor.visitorId == visitor_Id) {
          found = true;
        }
      });
      if(found == false) {
        $scope.visitorsDisplayList.push({visitorId: visitor_Id, visitorName: visitor_name});
      }
    };

    $scope.displayAgeData = function (data) {
      $scope.resultMessage = 'You have been on this earth for ';
      $scope.resultMessage += data["Year"] + ' Year(s), ';
      $scope.resultMessage += data["Month"] + ' Month(s), ';
      $scope.resultMessage += data["Day"] + ' Day(s) and ';
      $scope.resultMessage += data["Hour"] + ' Hour(s). ';
      var resultArea = document.getElementById('ageResultArea');
      resultArea.style.visibility='visible';
    };

    $scope.zeroAdjustedValue = function (value) {
    	if(value > 9) {
    		return value;
    	} else {
    		return ("0" + value);
    	}
    };

});