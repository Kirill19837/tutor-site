angular.module("umbraco").controller("tTitle", function ($scope, $sce) {
    $scope.content = {
        TTitle: $scope.block.data.tTitle,
        TBold: ($scope.block.data.tBold === "1" || $scope.block.data.tBold === true),
        TColor: $scope.block.data.tColor,
        TCenterAlign: ($scope.block.data.TCenterAlign === "1" || $scope.block.data.TCenterAlign === true),
        TTitleSize: $scope.block.data.tSize
    };

    $scope.title = $scope.content.TTitle;
    $scope.titleSizeClass = $scope.content.TTitleSize;

    $scope.titleStyle = {      
        'font-weight': $scope.content.TBold ? 'bold' : 'normal',
        'text-align': $scope.content.TCenterAlign ? 'center' : 'left',
        'color': $scope.content.TColor ? `#` + $scope.content.TColor.value : 'black'
    };
});