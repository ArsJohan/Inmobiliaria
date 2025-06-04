$(function () {
    $("#dvMenu").load("../Empleado/MenuEmpleado.html", function (response, status, xhr) {
        if (status == "error") {
            console.error("Error loading ../../Empleado/MenuEmpleado.html: " + xhr.status + " " + xhr.statusText);
        }
    });
});