angular.module("umbraco").controller("tContacts", function ($scope, mediaResource) {
    $scope.content = $scope.block.data;
    var icons = $scope.content.tSocialLinks.contentData
    if (icons) {
        icons.forEach(icon => {
            var imageUdi = icon.tIcon[0]

            if (imageUdi) {
                mediaResource.getById(imageUdi.mediaKey)
                    .then(function (media) {
                        icon.tIcon[0].src = media.mediaLink;
                    })
                    .catch(function (error) {
                        console.error("Error loading media:", error);
                    });
            } else {
                console.error("Image UDI is missing.");
            }
        })
    } 
});