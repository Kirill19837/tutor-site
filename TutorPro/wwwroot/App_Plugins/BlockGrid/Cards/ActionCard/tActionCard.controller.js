angular.module("umbraco").controller("tActionCard", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;

    if ($scope.content.tImage) {
        var image = $scope.content.tImage.contentData[0].tImage[0];
        if (image) {
            mediaResource.getById(image.mediaKey)
                .then(function (media) {
                    image.src = media.mediaLink;
                })
                .catch(function (error) {
                    console.error("Error loading media:", error);
                });
        }

        $scope.image = image;
    }   
});