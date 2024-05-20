angular.module("umbraco").controller("tHelpSchool", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    var images = [$scope.content.tImage[0], $scope.content.tImageSmall[0]];
    images.forEach(image => {
        mediaResource.getById(image.mediaKey)
            .then(function (media) {
                image.src = media.mediaLink;
            })
            .catch(function (error) {
                console.error("Error loading media:", error);
            });
    })

    $scope.images = images;
});