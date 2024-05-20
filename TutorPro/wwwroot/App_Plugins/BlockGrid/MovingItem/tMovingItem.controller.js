angular.module("umbraco").controller("tMovingItem", function ($scope) {
    $scope.content = $scope.block.data;
    $scope.class = $scope.content.tSize == "small" ? "small-" : "";
});