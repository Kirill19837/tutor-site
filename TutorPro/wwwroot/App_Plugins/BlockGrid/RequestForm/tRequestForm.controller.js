angular.module("umbraco").controller("tRequestForm", function ($scope, $sce) {
    $scope.initForm = function (data) {       

        $scope.showPhoneInput = (data.tPhoneInput === "1" || data.tPhoneInput === true);
        $scope.showMessageInput = (data.tMessageInput === "1" || data.tMessageInput === true);
        $scope.centered = (data.tCentered === "1" || data.tCentered === true);

        $scope.actionClass = $scope.centered ? "request-form__actions centered" : "request-form__actions";  
    }

    $scope.initForm($scope.block.data);
});
   