angular.module("umbraco").controller("tContactInfo", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    $scope.linkCards = $scope.content.tLinkCard.contentData;
    $scope.infoCards = $scope.content.tInfoCard.contentData;
    $scope.socialLinks = $scope.content.tSocialLinks.contentData;

    var icons = $scope.socialLinks

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