angular.module("umbraco").controller("tSocialLinkBlock", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    var icons = $scope.content.tSocialLinks.contentData;

    if (icons && icons.length > 0) {
        icons.forEach(function (icon) {
            var imageUdi = icon.tIcon[0].mediaKey;

            if (imageUdi) {
                mediaResource.getById(imageUdi)
                    .then(function (media) {
                        icon.tIcon[0].src = media.mediaLink;
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