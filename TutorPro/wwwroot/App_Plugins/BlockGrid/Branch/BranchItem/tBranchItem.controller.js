angular.module("umbraco").controller("tBranchItem", function ($scope, $sce) {
    $scope.item = $scope.block.data;
});