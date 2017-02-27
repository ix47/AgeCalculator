ageWebAPI
	.factory('WaitSpinner', function () {
		'use strict';

		var spinner;

		var opts = {
			lines: 13, // The number of lines to draw
  			length: 15, // The length of each line
  			width: 6, // The line thickness
  			radius: 21, // The radius of the inner circle
			corners: 1, // Corner roundness (0..1)
			rotate: 0, // The rotation offset
			direction: 1, // 1: clockwise, -1: counterclockwise
			color: '#22B14C', // #rgb or #rrggbb or array of colors
			speed: 1, // Rounds per second
			trail: 60, // Afterglow percentage
			shadow: false, // Whether to render a shadow
			hwaccel: false, // Whether to use hardware acceleration
			className: 'spinner', // The CSS class to assign to the spinner
			zIndex: 2e9, // The z-index (defaults to 2000000000)
			top: '49%', // Top position relative to parent in px
			left: '49%' // Left position relative to parent in px
		};

		return {
			start: function (elementId) {
				var target = document.getElementById(elementId);
				spinner = new Spinner(opts).spin(target);
			},
			startAtPosition: function (elementId, top, left) {
				opts["top"] = top;
				opts["left"] = left;
				var target = document.getElementById(elementId);
				spinner = new Spinner(opts).spin(target);
			},
			stop: function () {
				spinner.stop();
			}
		};
});