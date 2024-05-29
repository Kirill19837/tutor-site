angular.module("umbraco").controller("tDecor", function ($scope) {
    $scope.initDecor = function (model = $scope.block.data) {
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

    $scope.initDecors = function (data) {
        if (data.tDecors)
        data.tDecors.contentData.forEach(decor => {
            var type = decor.tType[0];
            var typeNumber = decor.tTypeNumber;
            var style = decor.tStyle[0];
            var dash = (decor.tDash === "1" || decor.tDash === true);
            var isAbove = (decor.tAbove === "1" || decor.tAbove === true);

            decor.typeClass = "decor-element " + type + (dash ? "-" : "") + typeNumber + " parallax-element " + (isAbove ? "above" : "");
            decor.src = "/images/icons/decor-elements/decor-" + type + "-" + style + ".svg";
        })
    }
});