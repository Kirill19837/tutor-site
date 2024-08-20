angular.module("umbraco").controller("tCategoriesFilterSelect", function ($scope) {
    $scope.content = $scope.block.data;   

    console.log($scope.content);
});
