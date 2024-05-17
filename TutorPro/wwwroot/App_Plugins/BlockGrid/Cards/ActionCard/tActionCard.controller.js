angular.module("umbraco").controller("tActionCard", function ($scope, $sce) {
    $scope.content = $scope.block.data;
});