angular.module("umbraco").controller("tScreenshots", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;

    var images = $scope.content.tImages;

    if (images) {
        images.forEach(image => {
            var imageUdi = image.mediaKey;

            if (imageUdi) {
                mediaResource.getById(imageUdi)
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

        $scope.images = images;
    }
});
