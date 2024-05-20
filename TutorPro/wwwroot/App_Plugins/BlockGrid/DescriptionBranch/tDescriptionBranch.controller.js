angular.module("umbraco").controller("tDescriptionBranch", function ($scope, $sce) {
    $scope.content = $scope.block.data;
    $scope.bottomInfo = $scope.content.tBottomDescription.contentData[0];
});
