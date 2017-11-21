var app = angular.module("userApp");

app.component("userBody",
    {
        transclude: true,
        bindings: {
        },
        templateUrl: "scripts/app/component/userBody/userBody.html",
        controller: [
            "$http", "$scope", function ($http, $scope) {
                console.log("userBody init");
                $scope.currentView = "table";
            }
        ]
    });