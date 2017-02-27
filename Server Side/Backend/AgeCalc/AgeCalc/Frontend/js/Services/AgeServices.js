ageWebAPI
	.factory('AgeServices', function (AppLocalStorage, $http) {
		'use strict';

		/****Global Variables****/
		var $secureAgeCalcServiceUrl = AppLocalStorage.getSecureServiceUrl();
		var $ageCalcServiceUrl = AppLocalStorage.getServiceUrl();
		var $browserIMEI = 'browser';

		var $deferred_Success = 'success';
		var $deferred_Fail = 'fail';

		//Objects for use.
		var $previousUsers;
		var $previousVisitors;
		var $calculationsData;

		return {
			sendUserData: function (formData) {
				console.log('Sending User Data Calculation request to AgeCalc Server.');
				console.log(formData);
				var deferrenceAgreement = new $.Deferred();

				var serviceUrl = $ageCalcServiceUrl + 'UserAge/Create';
				try 
				{
					var transform = function(data){
						return $.param(data);
					};

					$http.post(serviceUrl, formData, {headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'},transformRequest: transform}).success( function (data, status, headers, config) {
						console.log(data);
						$calculationsData = data;
						deferrenceAgreement.resolve($deferred_Success);
					}).error( function (data, status, headers, config) {
						console.log('Cound not send the request.');
						console.log(data);
						deferrenceAgreement.reject($deferred_Fail);
					});

				}
				catch(exception) 
				{
					deferrenceAgreement.reject($deferred_Fail);
				}

				return deferrenceAgreement.promise();
			},
			getCalculationsData: function () {
				return $calculationsData;
			},
			getUsers: function () {
				console.log('About to fetch previous users from the Server.');
				var deferrenceAgreement = new $.Deferred();
				var serviceUrl = $ageCalcServiceUrl + 'UserAge?callback=JSON_CALLBACK';
				try 
				{
					$http.get(serviceUrl).success( function (data, status, headers, config) {
						console.log(data);
						$previousUsers = data;
						deferrenceAgreement.resolve($deferred_Success);
					}).error( function (data, status, headers, config) {
						console.log(data);
						deferrenceAgreement.reject($deferred_Fail);
					});

				}
				catch(exception) 
				{
					deferrenceAgreement.reject($deferred_Fail);
				}

				return deferrenceAgreement.promise();
			},
			getPreviousUsers: function () {
				return $previousUsers;
			},
			getVisitors: function (formData) {
				console.log('Sending Visitors request to AgeCalc Server.');
				console.log(formData);
				var deferrenceAgreement = new $.Deferred();

				var serviceUrl = $ageCalcServiceUrl + 'UserAge/GetVisitors';
				try 
				{
					var transform = function(data){
						return $.param(data);
					};

					$http.post(serviceUrl, formData, {headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'},transformRequest: transform}).success( function (data, status, headers, config) {
						console.log(data);
						$previousVisitors = data;
						deferrenceAgreement.resolve($deferred_Success);
					}).error( function (data, status, headers, config) {
						console.log('Cound not send the request.');
						console.log(data);
						deferrenceAgreement.reject($deferred_Fail);
					});

				}
				catch(exception) 
				{
					deferrenceAgreement.reject($deferred_Fail);
				}

				return deferrenceAgreement.promise();
			},
			getPreviousVisitors: function () {
				return $previousVisitors;
			}
		};

});