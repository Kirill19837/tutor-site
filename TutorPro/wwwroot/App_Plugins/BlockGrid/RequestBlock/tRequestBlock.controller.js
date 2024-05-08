angular.module("umbraco").controller("tRequestBlock", function ($scope, $sce) {
    $scope.content = $scope.block.data;
    $scope.form = $scope.content.tRequestForm.contentData[0];   
});
