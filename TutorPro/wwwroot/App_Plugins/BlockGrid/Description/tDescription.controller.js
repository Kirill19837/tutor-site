angular.module("umbraco").controller("tDescription", function ($scope) {
    $scope.content = $scope.block.data;
    $scope.bottomInfo = $scope.content.tBottomDescription.contentData[0];
    $scope.mainInfo = $scope.content.tMainDescription.contentData[0];
    $scope.revertClass = ($scope.content.tRevert === "1" || $scope.content.tRevert === true) ? "revert" : "";
});