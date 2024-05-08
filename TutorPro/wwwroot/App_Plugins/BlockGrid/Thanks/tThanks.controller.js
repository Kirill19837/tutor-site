angular.module("umbraco").controller("tThanks", function ($scope) {
    var content = $scope.block.data.tThanksReply.contentData[0]
    
    $scope.content = content
    $scope.button = content.tButton.contentData[0]
    $scope.image = content.tImage.contentData[0]
});