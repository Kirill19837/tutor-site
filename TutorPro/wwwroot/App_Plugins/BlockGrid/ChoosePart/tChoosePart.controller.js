angular.module("umbraco").controller("tChoosePart", function ($scope, $sce) {
    $scope.content = $scope.block.data;
    $scope.items = $scope.content.tItems.contentData;
    $scope.items.forEach(item => {
        item.with = $scope.content.tWith;
    })
});