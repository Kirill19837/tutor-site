angular.module("umbraco").controller("SmtpConfigurationDashboardController", 
    function ($scope, $http, umbRequestHelper, notificationsService) {
        $scope.smtpConfig = {
            username: "",
            password: "",
            from: "",
            host: "",
            port: "",
            clientId: "",
            clientSecret: "",
            refreshToken: ""
        };

        $http.get("/umbraco/api/smtpConfiguration/get").then(function(response) {
            $scope.smtpConfig = response.data;
        });

        $scope.validateConfig = function () {
            return ($scope.smtpConfig.username && $scope.smtpConfig.username.length !== 0 &&
                $scope.smtpConfig.password && $scope.smtpConfig.password.length !== 0 &&
                $scope.smtpConfig.from && $scope.smtpConfig.from.length !== 0 &&
                $scope.smtpConfig.host && $scope.smtpConfig.host.length !== 0);
        }

        $scope.submit = function() {
            if ($scope.validateConfig()) {
                umbRequestHelper.resourcePromise(
                $http.put("/umbraco/api/smtpConfiguration/update", $scope.smtpConfig),
                "Something went wrong, try sending again").then(function () {
                notificationsService.success("Successfully updated.");
                }, function () {
                    notificationsService.error("An error occurred while updating of SMTP configuration. See logs for details.");
                });
            }
        };

        $scope.testing = false;

        $scope.test = function() {
            $scope.testing = true;
            umbRequestHelper.resourcePromise(
                $http.get("/umbraco/api/smtpConfiguration/test"),
                "Something went wrong, try sending again").then(function () {
                notificationsService.success("Test succeeded.");
                $scope.testing = false;
            }, function () {
                notificationsService.error("An error occurred while test. See logs for details.");
                $scope.testing = false;
            });
        }
});
