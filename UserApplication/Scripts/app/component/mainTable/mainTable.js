var app = angular.module("userApp", []);

app.component("mainTable",
    {
        transclude: true,
        bindings: {
        },
        templateUrl: "scripts/app/component/mainTable/mainTable.html",
        controller: [
            "$http", "$scope", function ($http, $scope) {
                console.log("mainTable init");
                

                $scope.editOrCreate = function (user) {
                    $scope.currentUser = user ? user : {};
                    $scope.currentView = "edit";
                }

                $scope.refresh = function () {
                    $http.get("api/Account/GetUsers").then(function (response) {
                        $scope.users = response.data.users;
                        console.log($scope.users);
                    });;

                }

                $scope.refresh();

            }
        ]
    });