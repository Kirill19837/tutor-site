angular.module("umbraco").controller("tMaterialItemBlock", function ($scope, $sce, contentResource) {  
    $scope.content = $scope.block.data;  
});