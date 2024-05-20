angular.module("umbraco").controller("tSocialLink", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    var imageUdi = $scope.content.tIcon;
    $scope.wrapClass = "socials-item"
    $scope.class = "socials-link"
    $scope.iconClass = "social-icon"

    if (imageUdi) {
        mediaResource.getById(imageUdi[0].mediaKey)
            .then(function (media) {
                $scope.image = media;

                if (media.mediaLink.indexOf("white") !== -1) {
                    $scope.iconClass += "-white"
                    $scope.class = "footer__" + $scope.class;
                    $scope.wrapClass = "footer__" + $scope.wrapClass;
                }
                else {
                    $scope.class = "info-block__" + $scope.class;
                    $scope.wrapClass = "info-block__" + $scope.wrapClass;
                }
            })
            .catch(function (error) {
                console.error("Error loading media:", error);
            });
    } else {
        console.error("Image UDI is missing.");
    }
});