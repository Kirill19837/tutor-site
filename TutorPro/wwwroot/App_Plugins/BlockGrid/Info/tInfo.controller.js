angular.module("umbraco").controller("tInfo", function ($scope, mediaResource) {
    console.log($scope);
    $scope.content = $scope.block.data;
    $scope.items = $scope.content.tItems.contentData;
    
    var images = $scope.items;

    if (images && images.length > 0) {
        images.forEach(function (image) {
            var imageUdi = image.tImage[0].mediaKey;

            if (imageUdi) {
                mediaResource.getById(imageUdi)
                    .then(function (media) {
                        image.tImage[0].src = media.mediaLink;
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