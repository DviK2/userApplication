var app = angular.module("userApp");

app.component("mainTable",
    {
        transclude: true,
        bindings: {
        },
        templateUrl: "scripts/app/component/mainTable/mainTable.html",
        controller: [
            "$http", function($http) {
                console.log("mainTable init");

                var _this = this;
                _this.enableEdit = {};

                var refresh = function() {
                    var url = "api/Account/GetUsers?search=";
                    if (_this.searchText !== undefined)
                        url += _this.searchText;

                    $http.get(url).then(function(response) {
                        _this.users = response.data.users;
                        console.log(_this.users);
                    });;
                }

                refresh();

                var saveUser = function(user) {
                    $http.post("api/Account/User", user, {})
                        .success(function(data, status, headers, config) {
                            console.log("user saved");
                            user.id = data.id;
                        })
                        .error(function(data, status, header, config) {
                            console.log("user not saved");
                        });
                }

                this.refresh = function() {
                    refresh();  
                };

                this.edit = function (id) {
                    _this.enableEdit[id] = true;
                }

                this.save = function(user) {
                    $http.post("api/Account/User", user, {})
                        .success(function (data, status, headers, config) {
                            console.log("user saved");
                        })
                        .error(function (data, status, header, config) {
                            console.log("user not saved");
                        });
                }

                this.addUser = function() {
                    if (_this.newUser.name) {
                        var user = {
                            "name": _this.newUser.name,
                            "email": _this.newUser.email
                        };
                        saveUser(user);
                        _this.users.push(user);

                        console.log("user added");
                    } else {
                        console.log("user not added");
                    }

                    _this.newUser.name = "";
                    _this.newUser.email = "";
                }

                this.delete = function (id) {
                    var url = "api/Account/DeleteUser/" + id;
                    $http.post(url)
                        .success(function (data, status, headers, config) {
                            console.log("user deleted");
                            refresh();  
                        })
                        .error(function (data, status, header, config) {
                            console.log("user not deleted");
                            console.log(data);
                        });
                }
            }
        ]
    });