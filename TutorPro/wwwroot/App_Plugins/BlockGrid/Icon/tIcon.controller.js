angular.module("umbraco").controller("tIcon", function ($scope, mediaResource) {
    $scope.initIcon = function (data) {
        $scope.content = data;
        $scope.image = $scope.content.tIconImage[0];

        if ($scope.image) {
            mediaResource.getById($scope.image.mediaKey)
                .then(function (media) {
                    $scope.image = media;
                })
                .catch(function (error) {
                    console.error("Error loading media:", error);
                });
        } else {
            console.error("Image UDI is missing.");
        }
    }   

    $scope.initIcon($scope.block.data)
});