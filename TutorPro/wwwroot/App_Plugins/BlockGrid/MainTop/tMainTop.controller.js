angular.module("umbraco").controller("tMainTop", function ($scope, mediaResource) {
    var content = $scope.block.data;
    var titleParts = content.tTitle.split(" ");
    $scope.firstTitlePart = "";
    $scope.secondTitlePart = "";

    $scope.images = [];

    var images = content.tImages;

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

    for (var i = 0; i < Math.min(content.tNumberOfColoredWords, titleParts.length); i++) {
        $scope.firstTitlePart += " " + titleParts[i];
    }

    if (titleParts.length > content.tNumberOfColoredWords) {
        $scope.secondTitlePart = " " + titleParts.slice(content.tNumberOfColoredWords).join(" ");
    }

    $scope.content = content;
    $scope.button = content.tButton ? content.tButton.contentData[0] : null;
    $scope.text = content.tInfoText
});