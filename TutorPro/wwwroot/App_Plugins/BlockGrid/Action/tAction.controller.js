angular.module("umbraco").controller("tAction", function ($scope, $sce) {
    $scope.content = $scope.block.data;
    $scope.form = $scope.content.tRequestBlock.contentData[0];
    $scope.cards = $scope.content.tCards.contentData;

    var actionsClass = "actions ";

    if ($scope.content.tStyle === "Matirials") {
        actionsClass += "matirials-actions ";
    } else if ($scope.content.tStyle === "Blog") {
        actionsClass += "blog-actions ";
    } else if ($scope.content.tStyle === "Article") {
        actionsClass += "article-actions ";
    }

    if ($scope.cards != null && $scope.cards.length === 1) {
        actionsClass += "single-link ";
    }

    $scope.actionClass = actionsClass;
});