angular.module("umbraco").controller("tHomework", function ($scope, mediaResource) {
    
    $scope.content = $scope.block.data;
    $scope.icon = $scope.content.tIcon.contentData[0];
    $scope.info = $scope.content.tHomeworkItem.contentData[0].tInfo.contentData[0];
    $scope.bottomDesc = $scope.content.tBottomDescription.contentData[0];
    $scope.bottomImage = $scope.bottomDesc.tImage[0];

    var iconImage = $scope.icon.tIconImage[0];
    console.log($scope)
    if (iconImage) {
        mediaResource.getById(iconImage.mediaKey)
            .then(function (media) {
                $scope.icon.src = media.mediaLink;
            })
            .catch(function (error) {
                console.error("Error loading media:", error);
            });
    } else {
        console.error("Image UDI is missing.");
    }

    if ($scope.bottomImage) {
        mediaResource.getById($scope.bottomImage.mediaKey)
            .then(function (media) {
                $scope.bottomImage.src = media.mediaLink;
            })
            .catch(function (error) {
                console.error("Error loading media:", error);
            });
    } else {
        console.error("Image UDI is missing.");
    }
   
});