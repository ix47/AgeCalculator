ageWebAPI.directive('datetimepicker', function() {
    return {
        restrict: 'A',
        require : 'ngModel',
        link : function (scope, element, attrs, ngModelCtrl) {
            $(function(){
                $(element).datetimepicker({
                     mask:'dd/MM/yy hh:mm:ss',
                    dateFormat: 'dd/MM/yy',
                    format: 'yyyy-MM-dd hh:mm',
                    // dateFormat: 'dd M yy',
                    onSelect:function (date) {
                            ngModelCtrl.$setViewValue(date);
                            scope.$apply();
                    }
                }).on('changeDate', function(e){
                    var mon = e.localDate.getMonth() + 1;
                    if (mon < 10) {
                        mon = "0" + mon;
                    }
                    var days = e.localDate.getDate();
                    if (days < 10) {
                        days = "0" + days;
                    }
                    var hours = e.localDate.getHours();
                    if (hours < 10) {
                        hours = "0" + hours;
                    }
                    var minutes = e.localDate.getMinutes();
                    if (minutes < 10) {
                        minutes = "0" + minutes;
                    }
                    var date = e.localDate.getFullYear() + "-" + mon + "-" + days + " " + hours + ":" + minutes + ":00";
                    ngModelCtrl.$setViewValue(date);
                    scope.$apply();
                });
            });
        }
    }
});