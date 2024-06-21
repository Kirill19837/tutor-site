angular.module("umbraco").controller("tMaterialBlock", function ($scope, $http) {
    $scope.content = $scope.block.data;   
    $scope.message = "Refresh materials";

    $scope.Refresh = function () {
        $scope.message = "updating materials..."
        $http.post("/Umbraco/Api/Materials/RefreshMaterial")
        .then(function () {
            $scope.message = "successfully refreshed"
        })
        .catch(function (error) {
            console.error("Error fetching waitlist data:", error);
            $scope.message = "error occurred while refreshing"
        });
    }

});
