
var api_url = "http://localhost:53901/";
let pagina = 1;
let cargando = false;
let sinMasDatos = false;

// Filtros activos
let filtroTipo = "";
let filtroPrecio = "";

window.addEventListener("scroll", async () => {
    if (cargando || sinMasDatos) return;

    if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 100) {
        await cargarInmuebles();
    }
});

function aplicarFiltros() {
    // Reiniciar paginación y filtros
    pagina = 1;
    sinMasDatos = false;
    filtroTipo = document.getElementById("tipoInmueble").value;
    filtroPrecio = document.getElementById("precioMax").value;

    document.getElementById("contenedorInmuebles").innerHTML = '';
    cargarInmuebles();
}

async function cargarInmuebles() {
    cargando = true;
    document.getElementById("loading").style.display = "block";

    const params = new URLSearchParams({
        pagina,
        tipo: filtroTipo,
        precioMax: filtroPrecio
    });

    try {
        const respuesta = await fetch(`${api_url}/api/inmueble/buscar?${params}`);
        const data = await respuesta.json();

        if (data.length === 0) {
            sinMasDatos = true;
            document.getElementById("loading").innerText = "No hay más propiedades.";
            return;
        }

        const contenedor = document.getElementById('contenedorInmuebles');

        data.forEach(inmueble => {
            const precio = inmueble.Precio_Venta !== null && inmueble.Precio_Venta > 0
                ? `$${new Intl.NumberFormat().format(inmueble.Precio_Venta)}`
                : `$${new Intl.NumberFormat().format(inmueble.Arriendo)} / mes`;

            const etiquetaNuevo = inmueble.Es_Nuevo
                ? `<span class="badge bg-warning text-dark position-absolute top-0 start-0 m-2">Nuevo</span>`
                : '';

            let colorEstado = 'bg-success';
            if (inmueble.Estado === 'Vendido' || inmueble.Estado === 'Arrendado') {
                colorEstado = 'bg-danger';
            }

            const etiquetaEstado = `
        <span class="badge ${colorEstado} position-absolute top-0 end-0 m-2">
          ${inmueble.Estado}
        </span>`;

            const card = `
        <div class="col-md-4 mb-4">
          <div class="card card-hover h-100 shadow position-relative">
            ${etiquetaNuevo}
            ${etiquetaEstado}
            <img src="${inmueble.url}" class="card-img-top" style="height: 200px; object-fit: cover;" alt="Imagen Inmueble">
            <div class="card-body">
              <h5 class="card-title">${inmueble.Tipo} en ${inmueble.Departamento}/${inmueble.Ciudad}</h5>
              <p class="card-text">
                <strong>Dirección:</strong> ${inmueble.Direccion}<br>
                <strong>Estrato:</strong> ${inmueble.Estrato}<br>
                <strong>Año Construcción:</strong> ${inmueble.Anio}<br>
                <strong>Precio:</strong> ${precio}
              </p>
              <button class="btn btn-primary w-100" onclick="agendar(${inmueble.Codigo})">Agendar Cita</button>
            </div>
          </div>
        </div>`;

            contenedor.innerHTML += card;
        });

        pagina++;
    } catch (error) {
        console.error('Error al cargar inmuebles:', error);
    } finally {
        cargando = false;
        document.getElementById("loading").style.display = "none";
    }
}

document.addEventListener("DOMContentLoaded", () => {
    cargarInmuebles(); // Cargar todos los inmuebles apenas la página esté lista
});