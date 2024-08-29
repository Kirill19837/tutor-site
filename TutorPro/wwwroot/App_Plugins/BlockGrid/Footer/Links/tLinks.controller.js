angular.module("umbraco").controller("tLinks", function ($scope) {
    $scope.content = $scope.block.data;
    console.log($scope.content)
});