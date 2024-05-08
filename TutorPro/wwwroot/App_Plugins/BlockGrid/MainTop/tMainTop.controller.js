angular.module("umbraco").controller("tMainTop", function ($scope) {
    var content = $scope.block.data;
    var titleParts = content.tTitle.split(" ");
    $scope.firstTitlePart = "";
    $scope.secondTitlePart = "";

    //TODO make gif visual

    for (var i = 0; i < Math.min(content.tNumberOfColoredWords, titleParts.length); i++) {
        $scope.firstTitlePart += " " + titleParts[i];
    }

    if (titleParts.length > content.tNumberOfColoredWords) {
        $scope.secondTitlePart = " " + titleParts.slice(content.tNumberOfColoredWords).join(" ");
    }

    $scope.content = content;
    $scope.button = content.tButton ? content.tButton.contentData[0] : null;
    $scope.text = content.tInfoText
});