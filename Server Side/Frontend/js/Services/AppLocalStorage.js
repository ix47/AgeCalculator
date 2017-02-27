ageWebAPI
	.factory('AppLocalStorage', function() {
		'use strict';

		var $ageCalcWebServiceUrl = 'http://wemakegreatsoftware.com/agecalc/WebService/';
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