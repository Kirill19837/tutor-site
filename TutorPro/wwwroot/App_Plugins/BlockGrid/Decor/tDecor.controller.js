angular.module("umbraco").controller("tDecor", function ($scope) {
    $scope.init = function (model = $scope.block.data) {
        $scope.type = model.tType[0];
        $scope.typeNumber = model.tTypeNumber;
        $scope.style = model.tStyle[0];
        $scope.dash = (model.tDash === "1" || model.tDash === true);
        $scope.isAbove = (model.tAbove === "1" || model.tAbove === true);
        $scope.speed = model.tSpeed;
        $scope.direction = (model.tDirection === "1" || model.tDirection === true);

        $scope.typeClass = "decor-element " + $scope.type + ($scope.dash ? "-" : "") + $scope.typeNumber + " parallax-element " + ($scope.isAbove ? "above" : "");

        $scope.src = "/images/icons/decor-elements/decor-" + $scope.type + "-" + $scope.style + ".svg";
    }   

    $scope.init();
});