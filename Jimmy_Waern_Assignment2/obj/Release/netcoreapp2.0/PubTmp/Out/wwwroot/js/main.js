$(document).ready(function () {
    var userDropdown = $("#userDropdown");
    var userSelection = $("#userSelection");
    var consoleWindow = $("#consoleWindow");

    getUsers();

    function getUsers() {
        $.ajax({
            url: "api/user",
            type: "GET"
        }).done(function (result) {
            userDropdown.empty();
            userSelection.empty();
            console.log(result);
            $.each(result, function (index, user) {
                userSelection.append("<option>" + user.userName + "</option>");
                userSelection.appendTo(userDropdown);
            });
        }).fail(function (xhr, status, error) {
            console.log(error);
            consoleWindow.text(error);
        });
    }

    $(document).on("click", "#getUsers", function () {
        $.ajax({
            url: "/api/user/seed",
            type: "GET"
        }).done(function (result) {
            getUsers();
            console.log(result);
            consoleWindow.text(result);
        }).fail(function (xhr, status, error) {
            console.log(error);
            consoleWindow.text(error);
        });
    });

    $(document).on("click", "#getClaims", function () {
        $.ajax({
            url: "/api/user/claims",
            type: "GET"
        }).done(function (result) {
            console.log(result);
            var users = [result.length];
            $.each(result, function (index, user) {
                users[index] = user.userName;
            });
            consoleWindow.text("Claims: " + users.toString());
        }).fail(function (xhr, status, error) {
            console.log(error);
            consoleWindow.text(error);
        });
    });

    $(document).on('click', '#logIn', function () {
        $.ajax({
            url: '/api/user/login',
            method: 'POST',
            data: {
                "Email": userSelection.val()
            }
        })
            .done(function (result) {
                console.log(result);
                consoleWindow.text(result);

            })
            .fail(function (xhr, status, error) {
                console.log(error);
                consoleWindow.text(error);
            })
    });

    $(document).on("click", "#openNews", function () {
        $.ajax({
            url: "/api/news/open",
            type: "GET"
        }).done(function (result) {
            console.log(result);
            consoleWindow.text(result);
        }).fail(function (xhr, status, error) {
            console.log(error);
            consoleWindow.text(error);
        });
    });

    $(document).on("click", "#hiddenNews", function () {
        $.ajax({
            url: "/api/news/hidden",
            type: "GET"
        }).done(function (result) {
            console.log(result);
            consoleWindow.text(result);
        }).fail(function (xhr, status, error) {
            console.log(error);
            consoleWindow.text(error);
        });
    });

    $(document).on("click", "#ageCheck", function () {
        $.ajax({
            url: "/api/news/adult",
            type: "GET"
        }).done(function (result) {
            console.log(result);
            consoleWindow.text(result);
        }).fail(function (xhr, status, error) {
            console.log(error);
            consoleWindow.text(error);
        });
    });

    $(document).on("click", "#publishSport", function () {
        $.ajax({
            url: "/api/news/sport",
            type: "GET"
        }).done(function (result) {
            console.log(result);
            consoleWindow.text(result);
        }).fail(function (xhr, status, error) {
            console.log(error);
            consoleWindow.text(error);
        });
    });

    $(document).on("click", "#publishCulture", function () {
        $.ajax({
            url: "/api/news/culture",
            type: "GET"
        }).done(function (result) {
            console.log(result);
            consoleWindow.text(result);
        }).fail(function (xhr, status, error) {
            console.log(error);
            consoleWindow.text(error);
        });
    });
});