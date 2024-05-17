angular.module("umbraco").controller("tWhyChooseUs", function ($scope) {
    $scope.content = $scope.block.data;
    $scope.card = $scope.content.tCard.contentData[0];
    $scope.sections = $scope.content.tSections.contentData;
});