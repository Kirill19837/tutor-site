angular.module("umbraco").controller("tImageWithText", function ($scope, mediaResource) {
    $scope.item = $scope.block.data;

    $scope.item.tImages.forEach(image => {
        var imageUdi = image;

        if (imageUdi) {
            mediaResource.getById(imageUdi.mediaKey)
                .then(function (media) {
                    image.src = media.mediaLink;
                })
                .catch(function (error) {
                    console.error("Error loading media:", error);
                });
        } else {
            console.error("Image UDI is missing.");
        }
    })  
});