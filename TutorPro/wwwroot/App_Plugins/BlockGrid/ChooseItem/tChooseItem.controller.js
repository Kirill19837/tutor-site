angular.module("umbraco").controller("tChooseItem", function ($scope, $sce) {
    $scope.content = $scope.block.data;
});