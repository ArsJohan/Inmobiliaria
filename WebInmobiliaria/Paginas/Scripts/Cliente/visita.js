const api_url = "http://inmobiliaria-ysja.runasp.net";

async function cargarDetalleInmueble(idInmueble) {
    try {
        const respuesta = await fetch(`${api_url}/api/Inmueble/ConsultarPorCodigo/${idInmueble}`);
        if (!respuesta.ok) throw new Error("No se pudo obtener la información del inmueble.");
        const inmueble = await respuesta.json();
        document.getElementById("txtDescripcion").innerText = inmueble[0].Descripcion || '';
        document.getElementById("txtDireccion").innerText = inmueble[0].Direccion || '';
        document.getElementById("txtEstrato").innerText = inmueble[0].Estrato || '';
        document.getElementById("txtAnio").innerText = inmueble[0].Anio || '';
        document.getElementById("txtPrecioVenta").innerText = inmueble[0].Precio_Venta !== null && inmueble[0].Precio_Venta > 0
            ? `$${new Intl.NumberFormat().format(inmueble[0].Precio_Venta)}`
            : `$${new Intl.NumberFormat().format(inmueble[0].Arriendo)} / mes`;
        document.getElementById("xtDepartamento").innerText = inmueble[0].Departamento || '';
        document.getElementById("txtCiudad").innerText = inmueble[0].Ciudad || '';
        document.getElementById("txtTipo").innerText = inmueble[0].Tipo || '';
        document.getElementById("txtEstado").innerText = inmueble[0].Estado || '';
        document.getElementById("detalleImagen").src = inmueble[0].url || '';
    } catch (error) {
        console.error("Error al cargar el inmueble:", error);
        alert("No se pudo cargar la información del inmueble.");
    }
}

function MostrarFormulario() {
    document.getElementById("formularioIngreso").style.display = "block";
    // Opcional: hacer scroll al formulario
    document.getElementById("formularioIngreso").scrollIntoView({ behavior: "smooth" });
}

async function AsignarVisita() {
    const fechaVisita = document.getElementById("txtFechaVisita").value;
    const hoy = new Date();
    hoy.setHours(0, 0, 0, 0); // Ignorar hora

    if (!fechaVisita) {
        alert("Por favor, seleccione la fecha de la visita.");
        return;
    }

    const fechaSeleccionada = new Date(fechaVisita + "T00:00:00");
    if (fechaSeleccionada < hoy) {
        alert("La fecha de la visita no puede ser anterior a hoy.");
        return;
    }
    // 1. Obtener datos del formulario
    const cliente = {
        Nro_Documento: document.getElementById("txtNroDocumento").value,
        Nombre: document.getElementById("txtNombre").value,
        Apellido: document.getElementById("txtApellido").value,
        Direccion: document.getElementById("txtDireccion").value,
        Telefono: document.getElementById("txtTelefono").value,
        Email: document.getElementById("txtEmail").value,
        Codigo_Genero: 1,
    };
    console.log(cliente.Nombre, cliente.Apellido, cliente.Nro_Documento)
    // Validación básica
    if (!cliente.Nro_Documento || !cliente.Nombre || !cliente.Apellido) {
        alert("Por favor, complete los campos obligatorios.");
        return;
    }

    try {
        // 2. Agregar cliente
        const respCliente = await fetch(`${api_url}/api/Cliente/Insertar`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(cliente)
        });

        if (!respCliente.ok) {
            throw new Error("No se pudo registrar el cliente.");
        }
        // Obtener el código del cliente como string
        const codigoCliente = parseInt(await respCliente.text(), 10);
        
        // 3. Obtener el id del inmueble desde la URL
        const params = new URLSearchParams(window.location.search);
        const idInmueble = params.get("idInmueble");

        // 4. Agregar visita
        const visita = {
            Codigo_Cliente: codigoCliente,
            Codigo_Inmueble: idInmueble,
            Fecha_Visita: document.getElementById("txtFechaVisita").value,
            Comentarios: `Visita Solicitada por ${cliente.Nombre}`
        };

        console.log(visita);
        const respVisita = await fetch(`${api_url}/api/Visitas/Insertar`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(visita)
        });

        if (!respVisita.ok) {
            throw new Error("No se pudo agendar la visita.");
        }

        alert("¡Visita agendada exitosamente!");
        // Opcional: limpiar formulario o redirigir
    } catch (error) {
        alert(error.message);
    }
}
document.addEventListener("DOMContentLoaded", () => {
    const params = new URLSearchParams(window.location.search);
    const idInmueble = params.get("idInmueble");
    if (idInmueble) {
        cargarDetalleInmueble(idInmueble);
    }
    // Establecer fecha mínima en el input de fecha
    const hoy = new Date();
    const yyyy = hoy.getFullYear();
    const mm = String(hoy.getMonth() + 1).padStart(2, '0');
    const dd = String(hoy.getDate()).padStart(2, '0');
    const minDate = `${yyyy}-${mm}-${dd}`;
    const inputFecha = document.getElementById("txtFechaVisita");
    if (inputFecha) {
        inputFecha.setAttribute("min", minDate);
    }
});
