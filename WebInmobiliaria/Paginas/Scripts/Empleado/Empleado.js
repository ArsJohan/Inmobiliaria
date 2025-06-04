// Obtener datos del formulario
let BaseURL = "http://inmobiliaria-ysja.runasp.net";
function obtenerDatosFormulario() {
    const codEmpleado = $("#txtCodEmpleado").val();
    const nombre = $("#txtNombre").val();
    const apellido = $("#txtApellido").val();
    const documento = $("#txtNroDocumento").val();
    const correo = $("#txtEmail").val();
    const telefono = $("#txtTelefono").val();
    const tipoTelefono = $("#cboTipoTelefono").val();
    const genero = $("#cboGenero").val();
    const tipoEmpleado = $("#cboTipoEmpleado").val();
    const sede = $("#cboSede").val();
    const tipoDocumento = $("#cboTipoDocumento").val();
    const activo = $("#chkActivoEmpleado").prop("checked"); 
    const fechaContratacion= $("#txtFechaContratacion").val()
        ? $("#txtFechaContratacion").val() + "T00:00:00"
        : null

    if (!codEmpleado || !nombre || !documento) {
        $("#dvMensaje").html("Por favor complete los campos obligatorios.");
        return null;
    }

    return {
        Codigo_Empleado: parseInt(codEmpleado) || 0, // en caso de que esté vacío, enviamos 0
        Activo: activo,
        Nombre: nombre,
        Apellido: apellido,
        Tipo_Doc: parseInt(tipoDocumento),
        Nro_Documento: documento,
        Tipo_Telefono: parseInt(tipoTelefono),
        Telefono: telefono,
        Email: correo,
        Fecha_Contratacion: fechaContratacion,
        Codigo_TipoEmpleado: parseInt(tipoEmpleado),
        Codigo_Sede: parseInt(sede),
        Codigo_Genero: parseInt(genero)
    };

}


// Insertar empleado
async function ConsultarEmpleado() {
    const documento = $("#txtNroDocumento").val();
    const response = await ConsultarServicio(`${BaseURL}/api/Empleado/Consultar?Documento=${documento}`);
    console.log("Respuesta Consulta:", response, documento);
    if (response == null) {
        $("#dvMensaje").html("Ingrese un documento para consultar la información");
        return;
    } 
        
    
    $("#txtCodEmpleado").val(response.Codigo_Empleado);
    $("#txtNombre").val(response.Nombre);
    $("#txtApellido").val(response.Apellido);
    $("#txtDocumento").val(response.Nro_Documento);
    $("#txtEmail").val(response.Email);
    $("#txtTelefono").val(response.Telefono);
    $("#cboTipoTelefono").val(response.Tipo_Telefono);
    $("#cboGenero").val(response.Codigo_Genero);
    $("#cboTipoEmpleado").val(response.Codigo_TipoEmpleado);
    $("#cboSede").val(response.Codigo_Sede);
    $("#cboTipoDocumento").val(response.Tipo_Doc);
    $("#chkActivoEmpleado").prop("checked", response.Activo);
    // Convertir la fecha al formato YYYY-MM-DD
    let fechaTexto = response.Fecha_Contratacion;
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
    


   
}
async function insertarEmpleado() {
    const empleado = obtenerDatosFormulario();
    if (!empleado) {

        return;
    } 

    const response = await EjecutarComandoServicio("POST", `${BaseURL}/api/Empleado/Insertar`, empleado);
    listarEmpleados(); // Recargar tabla
    limpiarFormulario();
}

// Actualizar empleado
async function actualizarEmpleado() {
    const empleado = obtenerDatosFormulario();
    if (!empleado) return;

    await EjecutarComandoServicio("PUT", `${BaseURL}/api/Empleado/Actualizar`, empleado);
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

    await EjecutarComandoServicio("DELETE", `${BaseURL}/api/Empleado/Eliminar?empleado=${codEmpleado}`, {});
    listarEmpleados();
    limpiarFormulario();
}

// Llenar tabla de empleados
async function listarEmpleados() {
    await LlenarTablaXServicios(`${BaseURL}/api/Empleado/ConsultarTodos`, "#tblEmpleados");
}

// Llenar combos en el load
async function cargarCombos() {
    await LlenarComboXServicios(`${BaseURL}/api/TipoTelefono/Todos`, "#cboTipoTelefono");
    await LlenarComboXServicios(`${BaseURL}/api/Genero/Todos`, "#cboGenero");
    await LlenarComboXServicios(`${BaseURL}/api/TipoEmpleado/Todos`, "#cboTipoEmpleado");
    await LlenarComboXServicios(`${BaseURL}/api/Sedes/Todas`, "#cboSede");
    await LlenarComboXServicios(`${BaseURL}/api/TipoDocumento/Todos`, "#cboTipoDocumento");

}

// Limpiar formulari´
function limpiarFormulario() {
    $("input[type='text']").val("");
    $("select").val("");
}

function Editar(fila_) {
    const fila = $(fila_).find("td");
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
};
// Cargar datos al hacer clic en una fila
$("#tblEmpleados").on("click", "tr", function () {
    Editar(this); // Pasas el elemento clickeado
});



// Al cargar la página
$(document).ready(function () {
    cargarCombos();
    listarEmpleados();
});
