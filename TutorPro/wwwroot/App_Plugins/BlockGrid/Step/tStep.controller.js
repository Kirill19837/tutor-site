angular.module("umbraco").controller("tStep", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    $scope.arrow = $scope.content.tArrow === "1";
    $scope.bottomDesc = $scope.content.tBottomDescription.contentData[0];
    $scope.topDesc = $scope.content.tTopDescription.contentData[0];
    $scope.items = $scope.content.tStepItems.contentData;

    var icons = [$scope.topDesc.tImage[0], $scope.bottomDesc.tImage[0]];

    if (icons && icons.length > 0) {
        icons.forEach(function (icon) {
            var imageUdi = icon.mediaKey;

            if (imageUdi) {
                mediaResource.getById(imageUdi)
                    .then(function (media) {
                        icon.src = media.mediaLink;
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