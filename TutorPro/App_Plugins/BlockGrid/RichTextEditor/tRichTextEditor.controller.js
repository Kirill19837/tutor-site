angular.module("umbraco").controller("tRichTextEditor", function ($scope, $sce) {
    $scope.trustAsHtml = function(html) {
        return $sce.trustAsHtml(html);
    };
});