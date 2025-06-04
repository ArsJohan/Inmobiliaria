// Obtener datos del formulario
let BaseURL = "http://inmobiliaria-ysja.runasp.net/";
function obtenerDatosFormulario() {
    const codEmpleado = $("#txtCodEmpleado").val();
    const nombre = $("#txtNombre").val();
    const apellido = $("#txtApellido").val();
    const documento = $("#txtDocumento").val();
    const correo = $("#txtCorreo").val();
    const telefono = $("#txtTelefono").val();
    const tipoTelefono = $("#cboTipoTelefono").val();
    const genero = $("#cboGenero").val();
    const tipoEmpleado = $("#cboTipoEmpleado").val();
    const sede = $("#cboSede").val();
    const tipoDocumento = $("#cboTipoDocumento").val();

    if (!codEmpleado || !nombre || !documento) {
        $("#dvMensaje").html("Por favor complete los campos obligatorios.");
        return null;
    }

    return {
        codEmpleado,
        nombre,
        apellido,
        documento,
        correo,
        telefono,
        tipoTelefono,
        genero,
        tipoEmpleado,
        sede,
        tipoDocumento
    };
}

// Insertar empleado
async function insertarEmpleado() {
    const empleado = obtenerDatosFormulario();
    if (!empleado) return;

    await EjecutarComandoServicio("POST", "/api/Empleado", empleado);
    listarEmpleados(); // Recargar tabla
    limpiarFormulario();
}

// Actualizar empleado
async function actualizarEmpleado() {
    const empleado = obtenerDatosFormulario();
    if (!empleado) return;

    await EjecutarComandoServicio("PUT", `/api/Empleado/${empleado.codEmpleado}`, empleado);
    listarEmpleados();
    limpiarFormulario();
}

// Eliminar empleado
async function eliminarEmpleado() {
    const codEmpleado = $("#txtCodEmpleado").val();
    if (!codEmpleado) {
        $("#dvMensaje").html("Seleccione un empleado para eliminar.");
        return;
    }

    await EjecutarComandoServicio("DELETE", `/api/Empleado/${codEmpleado}`, {});
    listarEmpleados();
    limpiarFormulario();
}

// Llenar tabla de empleados
async function listarEmpleados() {
    await LlenarTablaXServicios("/api/Empleado", "#tblClientes");
}

// Llenar combos en el load
async function cargarCombos() {
    await LlenarComboXServicios("/api/tiposTelefono", "#cboTipoTelefono");
    await LlenarComboXServicios("/api/generos", "#cboGenero");
    await LlenarComboXServicios("/api/tiposEmpleado", "#cboTipoEmpleado");
    await LlenarComboXServicios("/api/sedes", "#cboSede");
    await LlenarComboXServicios("/api/tiposDocumento", "#cboTipoDocumento");
}

// Limpiar formulario
function limpiarFormulario() {
    $("input[type='text']").val("");
    $("select").val("");
}

// Cargar datos al hacer clic en una fila
$("#tblClientes").on("click", "tr", function () {
    const fila = $(this).find("td");
    if (fila.length < 10) return;

    $("#txtCodEmpleado").val(fila.eq(0).text());
    $("#txtNombre").val(fila.eq(1).text());
    $("#txtApellido").val(fila.eq(2).text());
    $("#txtDocumento").val(fila.eq(3).text());
    $("#txtCorreo").val(fila.eq(4).text());
    $("#txtTelefono").val(fila.eq(5).text());
    $("#cboTipoTelefono").val(fila.eq(6).text());
    $("#cboGenero").val(fila.eq(7).text());
    $("#cboTipoEmpleado").val(fila.eq(8).text());
    $("#cboSede").val(fila.eq(9).text());
    $("#cboTipoDocumento").val(fila.eq(10).text());
});

// Al cargar la página
$(document).ready(function () {
    cargarCombos();
    listarEmpleados();
});
