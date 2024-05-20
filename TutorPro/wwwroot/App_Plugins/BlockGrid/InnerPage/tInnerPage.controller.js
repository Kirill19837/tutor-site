angular.module("umbraco").controller("tInnerPage", function ($scope) {
    $scope.innerClass = "inner-top ";
    $scope.content = $scope.block.data;

    if ($scope.content.TStyle === "About")
        $scope.innerClass += "radial-bg";
    else if ($scope.content.TStyle === "Matirials")
        $scope.innerClass += "matirials-top radial-bg";
    else if ($scope.content.TStyle === "Blog")
        $scope.innerClass += "blog-top radial-bg";
});