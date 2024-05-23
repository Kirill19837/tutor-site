angular.module("umbraco").controller("tHomeworkItem", function ($scope) {
    $scope.content = $scope.block.data;  
    $scope.info = $scope.content.tInfo.contentData[0];
});