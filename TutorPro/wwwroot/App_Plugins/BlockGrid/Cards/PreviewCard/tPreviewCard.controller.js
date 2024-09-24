angular.module("umbraco").controller("tPreviewCard", function ($scope) {
    $scope.content = $scope.block.data;
    if (!$scope.content.tButton.contentData[0].tButtonTitle || $scope.content.tButton.contentData[0].tButtonTitle.trim() === '') {
        $scope.titleClass = "block__title big";
    } else {
        $scope.titleClass = "block__title";
    }
});