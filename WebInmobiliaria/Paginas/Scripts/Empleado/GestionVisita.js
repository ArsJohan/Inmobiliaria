let BaseURL = "http://inmobiliaria-ysja.runasp.net";

// Obtener datos del formulario de visita
function obtenerDatosVisita() {
    const idVisita = $("#txtIdVisita").val();
    const idPropiedad = $("#txtIdPropiedad").val();
    const nroDocumento = $("#txtNroDocumento").val();
    const fechaVisita = $("#txtFechaVisita").val() ? $("#txtFechaVisita").val() + "T00:00:00" : null;
    const descripcion = $("#txtDescripcion").val();

    if (!idPropiedad || !nroDocumento || !fechaVisita) {
        $("#dvMensaje").html("Por favor complete todos los campos obligatorios.");
        return null;
    }

    return {
        Id_Visita: parseInt(idVisita) || 0,
        Id_Propiedad: parseInt(idPropiedad),
        Nro_Documento: nroDocumento,
        Fecha_Visita: fechaVisita,
        Descripcion: descripcion
    };
}

// Insertar visita
async function insertarVisita() {
    const visita = obtenerDatosVisita();
    if (!visita) return;

    await EjecutarComandoServicio("POST", `${BaseURL}/api/Visita/Insertar`, visita);
    listarVisitas();
    limpiarFormulario();
}

// Actualizar visita
async function actualizarVisita() {
    const visita = obtenerDatosVisita();
    if (!visita) return;

    await EjecutarComandoServicio("PUT", `${BaseURL}/api/Visita/Actualizar`, visita);
    listarVisitas();
    limpiarFormulario();
}

// Eliminar visita
async function eliminarVisita() {
    const idVisita = $("#txtIdVisita").val();
    if (!idVisita) {
        $("#dvMensaje").html("Debe seleccionar una visita para eliminar.");
        return;
    }

    await EjecutarComandoServicio("DELETE", `${BaseURL}/api/Visita/Eliminar?visita=${idVisita}`, {});
    listarVisitas();
    limpiarFormulario();
}

// Consultar datos del cliente a partir del documento
async function consultarClientePorDocumento() {
    const documento = $("#txtNroDocumento").val();
    if (!documento) {
        $("#dvMensaje").html("Debe ingresar el documento del cliente.");
        return;
    }

    const response = await ConsultarServicio(`${BaseURL}/api/Cliente/Consultar?documento=${documento}`);
    if (!response) {
        $("#dvMensaje").html("Cliente no encontrado.");
        return;
    }

    $("#txtNombre").val(response.Nombre);
    $("#txtApellido").val(response.Apellido);
    $("#txtDireccion").val(response.Direccion);
    $("#txtTipoTelefono").val(response.Tipo_Telefono);
    $("#txtTelefono").val(response.Telefono);
    $("#txtEmail").val(response.Email);
}

// Llenar tabla de visitas
async function listarVisitas() {
    await LlenarTablaXServicios(`${BaseURL}/api/Visitas/Todas`, "#tblVisita");
}

// Limpiar formulario
function limpiarFormulario() {
    $("input[type='text'], input[type='date'], textarea").val("");
}

// Seleccionar fila de la tabla
$("#tblVisita").on("click", "tr", function () {
    const celdas = $(this).find("td");
    if (celdas.length < 13) return;
    $("#txtNroDocumento").val(celdas.eq(1).text());
    $("#txtIdVisita").val(celdas.eq(2).text());
    $("#txtIdPropiedad").val(celdas.eq(3).text());
    $("#txtNombre").val(celdas.eq(4).text());
    $("#txtApellido").val(celdas.eq(5).text());
    $("#txtDireccion").val(celdas.eq(6).text());
    $("#txtDireccion").val(celdas.eq(6).text());
    $("#txtTipoTelefono").val(celdas.eq(7).text());
    $("#txtTelefono").val(celdas.eq(8).text());
    $("#txtFechaVisita").val(celdas.eq(9).text().substring(0, 10));
    $("#txtEmail").val(celdas.eq(10).text());
    $("#txtDescripcion").val(celdas.eq(11).text());

    // Opcional: auto-consulta del cliente
    consultarClientePorDocumento();
});

// Asignar eventos a botones
$(document).ready(function () {
    listarVisitas();

    $("#bntInsertar").click(function (e) {
        e.preventDefault();
        insertarVisita();
    });

    $("#btnActualizar").click(function (e) {
        e.preventDefault();
        actualizarVisita();
    });

    $("#btnEliminar").click(function (e) {
        e.preventDefault();
        eliminarVisita();
    });

    $("#btnConsultar").click(function (e) {
        e.preventDefault();
        consultarClientePorDocumento();
    });
});
