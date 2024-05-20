angular.module("umbraco").controller("tTutorLesson", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    var images = [];
    $scope.content.tItems.contentData.forEach(item => {
        images = images.concat(item.tImages)
    })

    if (images && images.length > 0) {
        images.forEach(function (image) {
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
        });
    }
});