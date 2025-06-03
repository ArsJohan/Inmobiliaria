$(function () {
    $("#dvMenu").load("../Cliente/Menu-Landpage.html", function (response, status, xhr) {
        if (status == "error") {
            console.error("Error loading Menu-Landpage.html: " + xhr.status + " " + xhr.statusText);
        }
    });
});