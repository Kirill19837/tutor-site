angular.module("umbraco").controller("tAbout", function ($scope) {
    $scope.content = $scope.block.data;
});