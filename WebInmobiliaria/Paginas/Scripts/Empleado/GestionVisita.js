let BaseURL = "http://inmobiliaria-ysja.runasp.net";

// Obtener datos del formulario de visita
function obtenerDatosVisita() {
    const idVisita = $("#txtIdVisita").val();
    const idPropiedad = $("#txtIdPropiedad").val();
    const codigoCliente = $("#txtCodigoCliente").val();
    const fechaVisita = $("#txtFechaVisita").val() ? $("#txtFechaVisita").val() + "T00:00:00" : null;
    const descripcion = $("#txtDescripcion").val();

    if (!idPropiedad || !nroDocumento || !fechaVisita) {
        $("#dvMensaje").html("Por favor complete todos los campos obligatorios.");
        return null;
    }

    return {
        Id_Visita: parseInt(idVisita) || 0,
        Codigo_Imueble: parseInt(idPropiedad),
        Codigo_Cliente: codigoCliente,
        Fecha_Visita: fechaVisita,
        Comentarios: descripcion
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

    console.log(visita);
    const response = await EjecutarComandoServicio("PUT", `${BaseURL}/api/Visitas/Actualizar`, visita);
    console.log(response);
    listarVisitas();
    limpiarFormulario();
}

// Eliminar visita desde botón externo
async function eliminarVisita() {
    const idVisita = $("#txtIdVisita").val();
    if (!idVisita) {
        $("#dvMensaje").html("Debe seleccionar una visita para eliminar.");
        return;
    }

    if (!confirm("¿Estás seguro de eliminar la visita seleccionada?")) return;

    await EjecutarComandoServicio("DELETE", `${BaseURL}/api/VisitaS/Eliminar/${idVisita}`, {});
    listarVisitas();
    limpiarFormulario();
}

// Consultar cliente por documento
async function consultarClientePorDocumento() {
    const documento = $("#txtNroDocumento").val();
    if (!documento) {
        $("#dvMensaje").html("Debe ingresar el documento del cliente.");
        return;
    }

    const response = await ConsultarServicio(`${BaseURL}/api/Visitas/PorCLiente/${documento}`);
    if (!response) {
        $("#dvMensaje").html("Cliente no encontrado.");
        return;
    }
    $("#txtIdVisita").val(response[0].Codigo_Visita);
    $("#txtIdPropiedad").val(response[0].Codigo_Inmueble);
    $("#txtCodigoCliente").val(response[0].Codigo_Cliente);
    $("#txtNroDocumento").val(response[0].Nro_Documento);
    $("#txtNombre").val(response[0].Nombre);
    $("#txtApellido").val(response[0].Apellido);
    $("#txtDireccion").val(response[0].Direccion);
    $("#txtTipoTelefono").val(response[0].TipoTelefono);
    $("#txtTelefono").val(response[0].Telefono);
    
    $("#txtEmail").val(response[0].Email);
    $("#txtDescripcion").val(response[0].Comentarios);
    // Conversión de fecha (si viene en DD/MM/YYYY o en ISO)
    let fechaTexto = response[0].Fecha_Visita;
    let fechaFormateada = "";

    if (fechaTexto.includes("/")) {
        // Formato DD/MM/YYYY → convertir a YYYY-MM-DD
        const partes = fechaTexto.split("/");
        if (partes.length === 3) {
            fechaFormateada = `${partes[2]}-${partes[1].padStart(2, '0')}-${partes[0].padStart(2, '0')}`;
        }
    } else if (fechaTexto.includes("-")) {
        // Si ya viene en ISO (YYYY-MM-DD…)
        fechaFormateada = fechaTexto.substring(0, 10);
    }

    $("#txtFechaVisita").val(fechaFormateada);
}


// Limpiar formulario
function limpiarFormulario() {
    $("input[type='text'], input[type='date'], textarea").val("");
}

// Llenar tabla con botón eliminar
async function listarVisitas() {
    await LlenarTablaXServicios(`${BaseURL}/api/Visitas/Todas`, "#tblVisita", function (fila, visita) {
        // Agrega al final de cada fila un <td> con un botón de eliminar
        const btnEliminar = `
            <button 
                class='btnEliminar' 
                data-id='${visita.Id_Visita}' 
                data-nombre='${visita.Nombre}' 
                data-apellido='${visita.Apellido}'
            >🗑️</button>`;
        $(fila).append(`<td>${btnEliminar}</td>`);
    });
}

// Editar visita desde tabla (carga valores en el formulario)
function Editar(fila_) {
    const fila = $(fila_).find("td");
    if (fila.length < 13) return; // Asegúrate de que la tabla tenga suficientes columnas

    // Revisa que estos índices coincidan con el orden de las columnas que realmente tienes
    $("#txtCodigoCliente").val(fila.eq(1).text());
    $("#txtNroDocumento").val(fila.eq(2).text());
    $("#txtIdVisita").val(fila.eq(3).text());
    $("#txtIdPropiedad").val(fila.eq(4).text());
    $("#txtNombre").val(fila.eq(5).text());
    $("#txtApellido").val(fila.eq(6).text());
    $("#txtDireccion").val(fila.eq(7).text());
    $("#txtTelefono").val(fila.eq(9).text());
    $("#txtTipoTelefono").val(fila.eq(8).text());
    $("#txtEmail").val(fila.eq(11).text());
    $("#txtDescripcion").val(fila.eq(12).text());

    // Conversión de fecha (si viene en DD/MM/YYYY o en ISO)
    let fechaTexto = fila.eq(10).text().trim();
    let fechaFormateada = "";

    if (fechaTexto.includes("/")) {
        // Formato DD/MM/YYYY → convertir a YYYY-MM-DD
        const partes = fechaTexto.split("/");
        if (partes.length === 3) {
            fechaFormateada = `${partes[2]}-${partes[1].padStart(2, '0')}-${partes[0].padStart(2, '0')}`;
        }
    } else if (fechaTexto.includes("-")) {
        // Si ya viene en ISO (YYYY-MM-DD…)
        fechaFormateada = fechaTexto.substring(0, 10);
    }

    $("#txtFechaVisita").val(fechaFormateada);
}

// Función que elimina una fila cuando se clickea el botón de la misma
function Eliminar(fila_, e) {
    

    const id = $(fila_).data("id");
    const nombre = $(fila_).data("nombre");
    const apellido = $(fila_).data("apellido");

    if (!confirm(`¿Deseas eliminar la visita de ${nombre} ${apellido} (ID: ${id})?`)) {
        return;
    }

    fetch(`${BaseURL}/api/Visita/Eliminar?visita=${id}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(resp => {
            if (resp.ok) {
                alert("Visita eliminada.");
                listarVisitas();
            } else {
                alert("Error al eliminar visita.");
            }
        })
        .catch(err => {
            console.error(err);
            alert("Error de conexión al eliminar.");
        });
} // ← Aquí se cierra la función Eliminar correctamente

// ------------------------------------------------------------------
// Aquí empieza el “binding” de eventos a la tabla y a los botones
// ------------------------------------------------------------------

// Maneja el clic en el botón “🗑️” que se agregó a cada fila
$("#tblVisita").on("click", ".btnEliminar", function (e) {
    Eliminar(this, e);
});

// Selección de fila para cargarla en el formulario (evita disparar selección si el clic fue en ❌)
$("#tblVisita").on("click", "tr", function (e) {
    if (!$(e.target).hasClass("btnEliminar")) {
        $("#tblVisita tr").removeClass("selected");
        $(this).addClass("selected");
        Editar(this);
    }
});

// Al arranque de la página, cargamos la tabla y bind de botones
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
