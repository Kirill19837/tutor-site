angular.module("umbraco").controller("tSchoolHero", function ($scope, $sce) {
    $scope.content = $scope.block.data;
    $scope.movingItems = $scope.content.tMovingItems.contentData;
    $scope.info = $scope.content.tInfo.contentData[0];
});