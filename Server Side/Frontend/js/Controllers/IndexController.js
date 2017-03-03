ageWebAPI.controller('IndexController', function ($scope, $routeParams, $http, AgeServices, WaitSpinner) {

	$scope.previousUsers = [];
	$scope.visitorsDisplayList = [];
  $scope.filteredVisitorsDisplayList = [];
  $scope.calculationData = [];
  $scope.leftPaginator;
  $scope.rightPaginator;
  $scope.paginationCounter;

  angular.element(document).ready(function() {

  $scope.userFirstName = '';
  $scope.userLastName = '';
	$scope.dobDateTime;

  $scope.resultMessage = '';

  $scope.isFormValid = false;
  $scope.pageIndex = 0;
  $scope.totalPages = 0;
  $scope.pageSize = 5;


  $scope.initialize();

  });

  $scope.initialize = function () {

  var goButton = $("#goButton");
  $scope.attachSubmitButtonAnimation(goButton);

  $scope.fetchPreviousVisitors();

  //We are going to attach the pen animation to the First Name.
  var firstNameInputControl = $("#userFirstName");
  var firstNameLabelControl = $("#userFirstNameLabel");

  $scope.attachPenAnimation(firstNameInputControl, firstNameLabelControl);

  //We are now going to attach the pen animation to the Last Name.
  var lastNameInputControl = $("#userLastName");
  var lastNameLabelControl = $("#userLastNameLabel");

  $scope.attachPenAnimation(lastNameInputControl, lastNameLabelControl);

  $scope.leftPaginator = $(".paginate.left");
  $scope.rightPaginator = $(".paginate.right");
  $scope.paginationCounter = $(".counter");

  $scope.attachPaginationAnimation($scope.leftPaginator, $scope.rightPaginator);

  };

  $scope.attachSubmitButtonAnimation = function (goButton) {
      $(goButton).click(function() {
        if ($(goButton).hasClass("loading-start")) {
          if ($(goButton).hasClass("loading-end")) {
            $('#buttonAnchor').text('Go');
            $scope.resetInputControls();
            $('#resetButtonTip').css({"visibility":"hidden"});
            return $(goButton).attr("class", "");
          }
        } else {
          
          //We won't let the button do anything if the user has not provided the information we need.
          if ($scope.isFormValid == false) { return;}

          $scope.goCalculateAge();
          setTimeout((function() {
            $('#buttonAnchor').text('');
            return $(goButton).addClass("loading-start");
          }), 0);
          setTimeout((function() {
            return $(goButton).addClass("loading-progress");
          }), 500);
          return setTimeout((function() {
            $('#resetButtonTip').css({"visibility":"visible"});
            return $(goButton).addClass("loading-end");
          }), 1500);
        }
    });
  };

  $scope.attachPenAnimation = function (inputControl, labelControl) {
  
    // jquery transit is used to handle the animation
    $(inputControl).focusin(function() {
      $(labelControl).transition({x:'80px'},500,'ease').next()
      .transition({x:'5px'},500, 'ease');

      //setTimeout needed for Chrome, for some reson there is no animation from left to right, 
      //the pen is immediately present. Slight delay to adding the animation class fixes it
      setTimeout(function(){
        $(labelControl).next().addClass('move-pen');
      },100);
    
    });

    $(inputControl).focusout(function() {
      $(labelControl).transition({x:'0px'},500,'ease').next()
      .transition({x:'-100px'},500, 'ease').removeClass('move-pen');
    });

  };

  $scope.attachPaginationAnimation = function (leftPaginator, rightPaginator) {

    //var pr = document.querySelector( '.paginate.left' );
    //var pl = document.querySelector( '.paginate.right' );

    //pr.onclick = slide.bind( this, -1 );
    //pl.onclick = slide.bind( this, 1 );

    $(leftPaginator).click(function() {
      $scope.slide(-1);
    });

    $(rightPaginator).click(function() {
      $scope.slide(1);
    });

    $scope.slide(0);
  };

  $scope.slide = function (offset) {
    $scope.pageIndex = Math.min( Math.max( $scope.pageIndex + offset, 0 ), $scope.totalPages - 1 );

    $($scope.paginationCounter).html(( $scope.pageIndex + 1 ) + ' / ' + $scope.totalPages);

    $($scope.leftPaginator).attr( 'data-state', $scope.pageIndex === 0 ? 'disabled' : '' );
    $($scope.rightPaginator).attr( 'data-state', $scope.pageIndex === $scope.totalPages - 1 ? 'disabled' : '' );

    $scope.adjustFilteredVisitorsForDisplay();
  };

  $scope.fetchPreviousVisitors = function () {

    var clientDateTime = $scope.getClientDateTime();

    var formData = {
      'clientDateTime' : clientDateTime
    };

    $.when(AgeServices.getVisitors(formData)).then(
      function (data) {
        $scope.previousUsers = AgeServices.getPreviousVisitors();
        $scope.totalPages = Math.ceil($scope.previousUsers.length/$scope.pageSize);
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

    $scope.removeInputControls();

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

  $scope.removeInputControls = function () {

    var nameQuestionControls = $("#nameQuestions");
    $(nameQuestionControls).addClass('removed');

    var dateOfBrithQuestionControls = $("#dateOfBirthQuestion");
    $(dateOfBrithQuestionControls).addClass('removed');

  };

  $scope.resetInputControls = function () {

    var nameQuestionControls = $("#nameQuestions");
    $(nameQuestionControls).removeClass('removed');

    var dateOfBrithQuestionControls = $("#dateOfBirthQuestion");
    $(dateOfBrithQuestionControls).removeClass('removed');

  };

    $scope.populateVisitorsDisplayList = function (visitors) {
    	angular.forEach(visitors, function (visitor){

        var displayName = visitor["FirstName"] + ' ' + visitor["LastName"];

        var displayValue = 'Age : ';
        
        var age = visitor["Age"];
        displayValue += age["Year"] + ' Year(s), ';
        displayValue += age["Month"] + ' Month(s), ';
        displayValue += age["Day"] + ' Day(s) and ';
        displayValue += age["Hour"] + ' Hour(s). ';

    		$scope.addVisitorForDisplay(visitor["UserId"], displayName, displayValue);
    	});

    };

    $scope.addVisitorForDisplay = function (visitor_Id, visitor_name, visitor_age) {
      var found = false;
      angular.forEach($scope.visitorsDisplayList, function (visitor){
        if(visitor.visitorId == visitor_Id) {
          found = true;
        }
      });
      if(found == false) {
        $scope.visitorsDisplayList.push({visitorId: visitor_Id, visitorName: visitor_name, visitorAge: visitor_age});
      }

      $scope.slide(0);
      $scope.adjustFilteredVisitorsForDisplay();
    };

    $scope.adjustFilteredVisitorsForDisplay = function () {
      var begin = (($scope.pageIndex - 1) * $scope.pageSize);
      if(begin < 0) {
        begin = 0;
      }
      var end = begin + $scope.pageSize;
      
      for (var i = $scope.filteredVisitorsDisplayList.length - 1; i >= 0; i--) {
        $scope.filteredVisitorsDisplayList.pop();
      }
      //$scope.filteredVisitorsDisplayList = [];
      var newList = $scope.visitorsDisplayList.slice(begin, end);

      angular.forEach(newList, function (visitor){
        $scope.filteredVisitorsDisplayList.push(visitor);
      });

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