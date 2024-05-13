angular.module("umbraco").controller("tHeroMoving", function ($scope, mediaResource) {
    
    $scope.content = $scope.block.data;
    
    $scope.bottomInfo = $scope.content.tBottomInfo ? $scope.content.tBottomInfo.contentData[0] : null;
    $scope.mainClass = $scope.content.tStyle === 'teacher' ? 'teacher-info ' : '';
    $scope.mainClass += 'school-info';

    $scope.bottomInfoClass = $scope.bottomInfo.tReverse ? "reverse" : "" 

    var imageUdi = $scope.bottomInfo.tImage;

    if (imageUdi) {
        mediaResource.getById(imageUdi[0].mediaKey)
            .then(function (media) {
                $scope.image = media;
            })
            .catch(function (error) {
                console.error("Error loading media:", error);
            });
    } else {
        console.error("Image UDI is missing.");
    }
});