angular.module("umbraco").controller("tTitle", function ($scope, $sce) {
   
    $scope.content = {
        TTitle: $scope.block.data.tTitle,
        TColor: $scope.block.data.tColor,
        TCenterAlign: ($scope.block.data.TCenterAlign === "1" || $scope.block.data.TCenterAlign === true),
        TTitleSize: $scope.block.data.tSize + "px",
        TFont: $scope.block.data.tFont,
        TWeight: $scope.block.data.tWeight,
    };

    $scope.title = $scope.content.TTitle;
    $scope.titleSizeClass = $scope.content.TTitleSize;
    $scope.class = $scope.content.TSubTitle ? "sub-title" : "title";
    $scope.titleStyle = {      
        'font-weight': $scope.content.TBold ? 'bold' : 'normal',
        'text-align': $scope.content.TCenterAlign ? 'center' : 'left',
        'color': $scope.content.TColor ? $scope.content.TColor : 'black',
        'font-size': $scope.content.TTitleSize,
        'font-family': $scope.content.TFont[0],
        'font-weight' : $scope.content.TWeight,
    };
});