angular.module("umbraco").controller("tInnerForPage", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    $scope.class = $scope.content.tFor + "-top inner-top";
    $scope.changes = $scope.content.tFor[0] === "teacher";
    $scope.images = [];

    var images = $scope.content.tImages;

    if (images && images.length > 0) {
        images.forEach(function (image) {
            var imageUdi = image.mediaKey;

            if (imageUdi) {
                mediaResource.getById(imageUdi)
                    .then(function (media) {
                        $scope.images.push(media);
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