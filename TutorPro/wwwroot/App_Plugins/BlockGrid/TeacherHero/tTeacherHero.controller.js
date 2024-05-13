angular.module("umbraco").controller("tTeacherHero", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    $scope.info = $scope.content.tBlockInfo.contentData[0];
    var icons = $scope.content.tIcons.contentData;

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