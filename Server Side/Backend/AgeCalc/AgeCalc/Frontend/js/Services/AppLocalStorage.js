ageWebAPI
	.factory('AppLocalStorage', function() {
		'use strict';

		var $ageCalcWebServiceUrl = 'http://localhost:1478/';
		var $secureAgeCalcWebServiceUrl = 'https://wemakegreatsoftware.com/agecalc/WebService/';

		return {
			getServiceUrl: function () {
				return $ageCalcWebServiceUrl;
			},
			getSecureServiceUrl: function () {
				return $secureAgeCalcWebServiceUrl;
			}
		};
	});