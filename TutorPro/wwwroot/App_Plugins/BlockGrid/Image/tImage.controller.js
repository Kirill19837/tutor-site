angular.module("umbraco").controller("tImage", function ($scope, mediaResource) {
    $scope.initImage = function (data) {
        var imageUdi = data.tImage;
        $scope.height = data.tHeight && !isNaN(data.tHeight) ? data.tHeight : "";
        $scope.width = data.tWidth && !isNaN(data.tWidth) ? data.tWidth : "";

        $scope.imageStyle = {
            'height': $scope.height + 'px',
            'width': $scope.width + 'px'
        };

        if (imageUdi) {
            mediaResource.getById(imageUdi[0].mediaKey)
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
});