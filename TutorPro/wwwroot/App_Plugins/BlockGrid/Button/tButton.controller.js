angular.module("umbraco").controller("tButton", function ($scope, $sce) {
    $scope.isFill = ($scope.block.data.tFill === "1" || $scope.block.data.tFill === true);
    $scope.toggleClass = $scope.isFill ? 'link link-main' : 'link link-inverted';

    $scope.initButton = function (buttonData) {
        $scope.isFill = (buttonData.tFill === "1" || buttonData.tFill === true);
        $scope.toggleClass = $scope.isFill ? 'link link-main' : 'link link-inverted';
    }
});