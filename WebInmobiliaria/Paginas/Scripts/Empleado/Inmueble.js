// Obtener datos del formulario
let BaseURL = "http://inmobiliaria-ysja.runasp.net";
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
    const activo = $("#chkActivoEmpleado").prop("checked");
    const fechaContratacion = $("#txtFechaContratacion").val()
        ? $("#txtFechaContratacion").val() + "T00:00:00"
        : null

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
        tipoDocumento,
        activo,
        fechaContratacion
    };
}


// Insertar empleado
async function insertarEmpleado() {
    const empleado = obtenerDatosFormulario();
    if (!empleado) return;

    const response = await EjecutarComandoServicio("POST", `${BaseURL}/api/Inmueble//Insertar`, empleado);
    console.log(response);
    listarEmpleados(); // Recargar tabla
    limpiarFormulario();
}

// Actualizar empleado
async function actualizarEmpleado() {
    const empleado = obtenerDatosFormulario();
    if (!empleado) return;

    await EjecutarComandoServicio("PUT", `${BaseURL}/api/Inmueble/${empleado.codEmpleado}`, empleado);
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

    await EjecutarComandoServicio("DELETE", `${BaseURL}/api/Inmueble/${codEmpleado}`, {});
    listarEmpleados();
    limpiarFormulario();
}

// Llenar tabla de empleados
async function listarEmpleados() {
    await LlenarTablaXServicios(`${BaseURL}/api/Inmueble/ConsultarTodos`, "#tblInmueble");
}

// Llenar combos en el load
const BaseURL = "https://tuservidor.com"; // Ajusta esto según tu entorno

async function cargarCombos() {
    await LlenarComboXServicios(`${BaseURL}/api/TipoInmueble/Todos`, "#cboTipoInmueble");
    await LlenarComboXServicios(`${BaseURL}/api/Ubicacion/Departamentos`, "#cboDepartamentos");
    // Las ciudades se cargan dinámicamente cuando se selecciona el departamento
}

// Evento para cargar ciudades según el departamento seleccionado
$("#cboDepartamentos").change(async function () {
    const idDepto = $(this).val();
    if (idDepto && idDepto !== "0") {
        await LlenarComboXServicios(`${BaseURL}/api/Ubicacion/CiudadesPorDepartamento?id=${idDepto}`, "#cboCiudad");
    } else {
        $("#cboCiudad").empty().append('<option value="0">Seleccione una ciudad</option>');
    }
});

// Función genérica para llenar combos
async function LlenarComboXServicios(url, idCombo) {
    try {
        const response = await fetch(url);
        if (!response.ok) throw new Error("Error al cargar el combo.");

        const data = await response.json();
        const $combo = $(idCombo);
        $combo.empty().append('<option value="0">Seleccione una opción</option>');

        data.forEach(item => {
            $combo.append(`<option value="${item.id}">${item.nombre}</option>`);
        });
    } catch (error) {
        console.error(`Error en ${idCombo}:`, error);
        $(idCombo).empty().append('<option value="0">Error al cargar</option>');
    }
}


// Limpiar formulari´
function limpiarFormulario() {
    $("input[type='text']").val("");
    $("select").val("");
}

// Cargar datos al hacer clic en una fila
$("#tblVista").on("click", "tr", function () {
    const fila = $(this).find("td");
    if (fila.length < 13) return; // Asegúrate de que haya suficientes columnas

    $("#txtCodEmpleado").val(fila.eq(1).text());
    $("#txtNombre").val(fila.eq(6).text());
    $("#txtApellido").val(fila.eq(7).text());
    $("#txtNroDocumento").val(fila.eq(3).text());
    $("#txtEmail").val(fila.eq(9).text());
    $("#txtTelefono").val(fila.eq(11).text());
    $("#cboTipoTelefono").val(fila.eq(10).text());
    $("#cboGenero").val(fila.eq(8).text());
    $("#cboTipoEmpleado").val(fila.eq(13).text());
    $("#cboSede").val(fila.eq(5).text());
    $("#cboTipoDocumento").val(fila.eq(2).text());

    // Convertir la fecha al formato YYYY-MM-DD
    let fechaTexto = fila.eq(12).text().trim(); // Ej: "03/06/2025"
    let fechaFormateada = "";

    if (fechaTexto.includes("/")) {
        // Si viene en formato DD/MM/YYYY
        const partes = fechaTexto.split("/");
        if (partes.length === 3) {
            fechaFormateada = `${partes[2]}-${partes[1].padStart(2, '0')}-${partes[0].padStart(2, '0')}`;
        }
    } else if (fechaTexto.includes("-")) {
        // Si ya viene en formato ISO, tomar solo la fecha sin la hora
        fechaFormateada = fechaTexto.substring(0, 10);
    }

    $("#txtFechaContratacion").val(fechaFormateada);

    $("#chkActivoEmpleado").prop("checked", fila.eq(4).text().toLowerCase() === 'true');
});



// Al cargar la página
$(document).ready(function () {
    cargarCombos();
    listarEmpleados();
});
