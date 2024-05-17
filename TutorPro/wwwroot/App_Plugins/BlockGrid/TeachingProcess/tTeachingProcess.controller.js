angular.module("umbraco").controller("tTeachingProcess", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    var icons = $scope.content.tIconItems.contentData;

    if (icons && icons.length > 0) {
        icons.forEach(function (icon) {
            var imageUdi = icon.tIconImage[0].mediaKey;

            if (imageUdi) {
                mediaResource.getById(imageUdi)
                    .then(function (media) {
                        icon.tIconImage[0].src = media.mediaLink;
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