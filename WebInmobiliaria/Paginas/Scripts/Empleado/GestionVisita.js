let BaseURL = "http://inmobiliaria-ysja.runasp.net";

// Llenar tabla de visitas
async function listarVisitas() {
    await LlenarTablaXServicios(`${BaseURL}/api/Visitas/Todas`, "#tblVisita");
}

// Obtener datos del formulario de visitas
function obtenerDatosFormulario() {
    const idVisita = $("#txtIdVisita").val();
    const idPropiedad = $("#txtIdPropiedad").val();
    const nroDocumento = $("#txtNroDocumento").val();
    const nombre = $("#txtNombre").val();
    const apellido = $("#txtApellido").val();
    const direccion = $("#txtDireccion").val();
    const tipoTelefono = $("#cboTipoTelefono").val();
    const telefono = $("#txtTelefono").val();
    const fechaVisita = $("#txtFechaVisita").val();
    const email = $("#txtEmail").val();
    const descripcion = $("#txtDescripcion").val();

    if (!idVisita || !idPropiedad || !nroDocumento || !fechaVisita) {
        $("#dvMensaje").html("Por favor complete los campos obligatorios.");
        return null;
    }

    return {
        idVisita,
        idPropiedad,
        nroDocumento,
        nombre,
        apellido,
        direccion,
        tipoTelefono,
        telefono,
        fechaVisita,
        email,
        descripcion
    };
}

// Insertar visita
async function insertarVisita() {
    const visita = obtenerDatosFormulario();
    if (!visita) return;

    await EjecutarComandoServicio("POST", `${BaseURL}/api/Visitas/Insertar`, visita);
    listarVisitas();
    limpiarFormulario();
}

// Actualizar visita
async function actualizarVisita() {
    const visita = obtenerDatosFormulario();
    if (!visita) return;

    await EjecutarComandoServicio("PUT", `${BaseURL}/api/Visitas/Actualizar`, visita);
    listarVisitas();
    limpiarFormulario();
}

// Eliminar visita
async function eliminarVisita() {
    const idVisita = $("#txtIdVisita").val();
    if (!idVisita) {
        $("#dvMensaje").html("Seleccione una visita para eliminar.");
        return;
    }

    await EjecutarComandoServicio("DELETE", `${BaseURL}/api/Visitas/Eliminar/${idVisita}`, {});
    listarVisitas();
    limpiarFormulario();
}

// Cargar combos al iniciar
async function cargarCombos() {
    await LlenarComboXServicios(`${BaseURL}/api/TipoTelefono/Todos`, "#cboTipoTelefono");
    // Si agregas combos nuevos aquí puedes añadir más
}

// Limpiar formulario
function limpiarFormulario() {
    $("#frmRegistraVisita")[0].reset();
    $("#dvMensaje").html("");
}

// Cargar datos al hacer clic en la tabla
$("#tblVista").on("click", "tr", function () {
    const fila = $(this).find("td");
    if (fila.length < 12) return;

    $("#txtIdVisita").val(fila.eq(1).text());
    $("#txtIdPropiedad").val(fila.eq(2).text());
    $("#txtNombre").val(fila.eq(3).text());
    $("#txtApellido").val(fila.eq(4).text());
    $("#txtNroDocumento").val(fila.eq(6).text());
    $("#txtDireccion").val(fila.eq(7).text());
    $("#txtTelefono").val(fila.eq(8).text());
    $("#cboTipoTelefono").val(fila.eq(5).text());
    $("#txtFechaVisita").val(formatearFecha(fila.eq(9).text()));
    $("#txtEmail").val(fila.eq(10).text());
    $("#txtDescripcion").val(fila.eq(11).text());
});

// Convertir fechas a formato YYYY-MM-DD
function formatearFecha(fechaTexto) {
    if (fechaTexto.includes("/")) {
        const partes = fechaTexto.split("/");
        if (partes.length === 3) {
            return `${partes[2]}-${partes[1].padStart(2, '0')}-${partes[0].padStart(2, '0')}`;
        }
    } else if (fechaTexto.includes("-")) {
        return fechaTexto.substring(0, 10);
    }
    return "";
}

// Al cargar la página
$(document).ready(function () {
    cargarCombos();
    listarVisitas();

    $("#bntInsertar").on("click", function (e) {
        e.preventDefault();
        insertarVisita();
    });

    $("#btnActualizar").on("click", function (e) {
        e.preventDefault();
        actualizarVisita();
    });

    $("#btnEliminar").on("click", function (e) {
        e.preventDefault();
        eliminarVisita();
    });

    $("#btnConsultar").on("click", function (e) {
        e.preventDefault();
        listarVisitas();
    });
});