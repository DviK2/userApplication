angular.module("userApp", [])
    .config([
        '$controllerProvider', function($controllerProvider) {
            $controllerProvider.allowGlobals();
        }
    ])
    .controller("GlobalController",
        [
            "$scope", "$http",
            function($scope, $http) {
                $scope.currentView = "table";

                $scope.refresh = function() {
                    $http.get("api/Account/GetUsers").then(function(response) {
                        $scope.users = response.data.users;
                        console.log($scope.users);
                    });;
                }
            }
        ]);
