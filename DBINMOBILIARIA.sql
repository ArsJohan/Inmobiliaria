CREATE DATABASE DBINMOBILIARIA;
GO
USE DBINMOBILIARIA;
GO

CREATE TABLE DEPARTAMENTO (
    Codigo_Departamento INT IDENTITY(1,1) PRIMARY KEY,
    Nombre              VARCHAR(100) NOT NULL
);
GO
INSERT INTO DEPARTAMENTO (Nombre) VALUES 
('Amazonas'),
('Antioquia'),
('Arauca'),
('Atlántico'),
('Bolívar'),
('Boyacá'),
('Caldas'),
('Caquetá'),
('Casanare'),
('Cauca'),
('Cesar'),
('Chocó'),
('Córdoba'),
('Cundinamarca'),
('Guainía'),
('Guaviare'),
('Huila'),
('La Guajira'),
('Magdalena'),
('Meta'),
('Nariño'),
('Norte de Santander'),
('Putumayo'),
('Quindío'),
('Risaralda'),
('San Andrés y Providencia'),
('Santander'),
('Sucre'),
('Tolima'),
('Valle del Cauca'),
('Vaupés'),
('Vichada');
CREATE TABLE CIUDAD (
    Codigo_Ciudad       INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Ciudad              VARCHAR(100) NOT NULL,
    Codigo_Departamento INT NOT NULL,
    FOREIGN KEY (Codigo_Departamento) REFERENCES DEPARTAMENTO(Codigo_Departamento)
);
GO
INSERT INTO CIUDAD (Nombre_Ciudad, Codigo_Departamento) VALUES 
-- Amazonas
('Leticia', 1),

-- Antioquia
('Medellín', 2),
('Envigado', 2),
('Bello', 2),
('Itagüí', 2),
('Rionegro', 2),

-- Arauca
('Arauca', 3),

-- Atlántico
('Barranquilla', 4),
('Soledad', 4),
('Malambo', 4),

-- Bolívar
('Cartagena', 5),
('Turbaco', 5),

-- Boyacá
('Tunja', 6),
('Duitama', 6),
('Sogamoso', 6),

-- Caldas
('Manizales', 7),
('Villamaría', 7),

-- Caquetá
('Florencia', 8),

-- Casanare
('Yopal', 9),

-- Cauca
('Popayán', 10),

-- Cesar
('Valledupar', 11),

-- Chocó
('Quibdó', 12),

-- Córdoba
('Montería', 13),

-- Cundinamarca
('Soacha', 14),
('Zipaquirá', 14),
('Girardot', 14),
('Fusagasugá', 14),

-- Guainía
('Inírida', 15),

-- Guaviare
('San José del Guaviare', 16),

-- Huila
('Neiva', 17),
('Pitalito', 17),

-- La Guajira
('Riohacha', 18),

-- Magdalena
('Santa Marta', 19),

-- Meta
('Villavicencio', 20),

-- Nariño
('Pasto', 21),
('Ipiales', 21),

-- Norte de Santander
('Cúcuta', 22),

-- Putumayo
('Mocoa', 23),

-- Quindío
('Armenia', 24),

-- Risaralda
('Pereira', 25),
('Dosquebradas', 25),

-- San Andrés y Providencia
('San Andrés', 26),

-- Santander
('Bucaramanga', 27),
('Floridablanca', 27),
('Girón', 27),

-- Sucre
('Sincelejo', 28),

-- Tolima
('Ibagué', 29),

-- Valle del Cauca
('Cali', 30),
('Palmira', 30),
('Tuluá', 30),

-- Vaupés
('Mitú', 31),

-- Vichada
('Puerto Carreño', 32);
-- 2. Sedes
CREATE TABLE SEDE (
    Codigo_Sede       INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Sede       VARCHAR(100)   NOT NULL,
    Direccion         VARCHAR(255)   NOT NULL,
    Codigo_Ciudad     INT            NOT NULL,
    FOREIGN KEY (Codigo_Ciudad) REFERENCES CIUDAD(Codigo_Ciudad)
);
GO
INSERT INTO SEDE (Nombre_Sede, Direccion, Codigo_Ciudad) VALUES 
('Sede Centro', 'Cra 45 #54-20', 1),
('Sede Norte', 'Av. 80 #40-10', 2);
GO

-- 3. Tipos de Empleado
CREATE TABLE TIPO_EMPLEADO (
    Codigo_TipoEmpleado  INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion          VARCHAR(100)     NOT NULL
);
GO
INSERT INTO TIPO_EMPLEADO (Descripcion) VALUES 
('Agente de Ventas'), ('Administrador'), ('Captador');
GO
-- 4. Tipos de Documento
CREATE TABLE TIPO_DOCUMENTO (
    Codigo_doc    INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion   VARCHAR(100)     NOT NULL
);
GO
INSERT INTO TIPO_DOCUMENTO (Descripcion) VALUES 
('Cédula de Ciudadanía'), ('Cédula de Extranjería'), ('Pasaporte');
GO

-- 5. Tipos de Teléfono
CREATE TABLE TIPO_TELEFONO (
    Codigo_TipoTelefono INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion         VARCHAR(100)     NOT NULL
);
GO
INSERT INTO TIPO_TELEFONO (Descripcion) VALUES 
('Móvil'), ('Fijo'), ('WhatsApp');
GO
-- 6. Género
CREATE TABLE GENERO (
    Codigo_Genero INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion        VARCHAR(50)     NOT NULL
);
GO
INSERT INTO GENERO (Descripcion) VALUES 
('Masculino'), ('Femenino'), ('Otro');
GO
-- 7. Empleados
CREATE TABLE EMPLEADO (
    Codigo_Empleado      INT IDENTITY(1,1) PRIMARY KEY,
    Activo               BIT              DEFAULT 1,
    Nombre               VARCHAR(50)      NOT NULL,
    Apellido             VARCHAR(50)      NOT NULL,
    Tipo_Doc             INT              NULL,
    Nro_Documento        VARCHAR(100)     NOT NULL UNIQUE,
    Tipo_Telefono        INT              NULL,
    Telefono             VARCHAR(100),
    Email                VARCHAR(100),
    Fecha_Contratacion   DATE             NOT NULL,
    Codigo_TipoEmpleado  INT              NOT NULL,
    Codigo_Sede          INT              NOT NULL,
    Codigo_Genero        INT              NOT NULL,
    FOREIGN KEY (Tipo_Doc) REFERENCES TIPO_DOCUMENTO(Codigo_doc),
    FOREIGN KEY (Tipo_Telefono) REFERENCES TIPO_TELEFONO(Codigo_TipoTelefono),
    FOREIGN KEY (Codigo_TipoEmpleado) REFERENCES TIPO_EMPLEADO(Codigo_TipoEmpleado),
    FOREIGN KEY (Codigo_Sede) REFERENCES SEDE(Codigo_Sede),
    FOREIGN KEY (Codigo_Genero) REFERENCES GENERO(Codigo_Genero)
);
GO
INSERT INTO EMPLEADO (Nombre, Apellido, Tipo_Doc, Nro_Documento, Tipo_Telefono, Telefono, Email, Fecha_Contratacion, Codigo_TipoEmpleado, Codigo_Sede, Codigo_Genero) 
VALUES
('Carlos', 'Pérez', 1, '1000123456', 1, '3001234567', 'carlos@empresa.com', '2023-01-10', 1, 1, 1),
('Laura', 'Gómez', 1, '1000789012', 2, '6041234567', 'laura@empresa.com', '2022-11-15', 3, 2, 2);
GO
-- 8. Clientes
CREATE TABLE CLIENTE (
    Codigo_Cliente       INT IDENTITY(1,1) PRIMARY KEY,
    Activo               BIT              DEFAULT 1,
    Nombre               VARCHAR(50)      NOT NULL,
    Apellido             VARCHAR(50)      NOT NULL,
    Tipo_Doc             INT              NULL,
    Nro_Documento        VARCHAR(100)     NOT NULL UNIQUE,
    Tipo_Telefono        INT              NULL,
    Telefono             VARCHAR(100),
    Email                VARCHAR(100),
    Direccion            VARCHAR(255),
	Fecha_Nacimiento	 DATE,
    Codigo_Ciudad        INT              NULL,
    Codigo_Genero        INT              NOT NULL,
    FOREIGN KEY (Tipo_Doc) REFERENCES TIPO_DOCUMENTO(Codigo_doc),
    FOREIGN KEY (Tipo_Telefono) REFERENCES TIPO_TELEFONO(Codigo_TipoTelefono),
    FOREIGN KEY (Codigo_Ciudad) REFERENCES CIUDAD(Codigo_Ciudad),
    FOREIGN KEY (Codigo_Genero) REFERENCES GENERO(Codigo_Genero)
);
GO
INSERT INTO CLIENTE (Nombre, Apellido, Tipo_Doc, Nro_Documento, Tipo_Telefono, Telefono, Email, Direccion, Codigo_Ciudad, Codigo_Genero, Fecha_Nacimiento)
VALUES 
('Juan', 'Torres', 1, '1010101010', 1, '3012345678', 'juan@mail.com', 'Calle 10 #20-30', 1, 1, '1990-05-15'),
('Ana', 'Martínez', 2, '2020202020', 2, '6047654321', 'ana@mail.com', 'Carrera 50 #30-15', 2, 2, '1992-08-22');
GO
-- 9. Tipos de Inmueble
CREATE TABLE TIPO_INMUEBLE (
    Codigo_TipoInmueble  INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion          VARCHAR(100)     NOT NULL
);
GO
INSERT INTO TIPO_INMUEBLE (Descripcion) VALUES 
('Apartamento'), ('Casa'), ('Local Comercial');
GO

-- 10. Inmuebles
CREATE TABLE INMUEBLE (
    Codigo_Inmueble           INT IDENTITY(1,1) PRIMARY KEY,
    Activo                    BIT              DEFAULT 1,
    Direccion                 VARCHAR(255)     NOT NULL,
    Codigo_Ciudad             INT              NOT NULL,
    Codigo_TipoInmueble       INT              NOT NULL,
    Es_Nuevo                  BIT              NOT NULL,
    Fecha_Alta                DATE             NOT NULL,
    Descripcion               TEXT,
    Codigo_Empleado_Captacion INT              NOT NULL,
    Precio_Venta              DECIMAL(18,2)    NULL,
    Canon_Mensual             DECIMAL(18,2)    NULL,
    Metros_Cuadrados          DECIMAL(6,2)     NULL,
    Numero_Habitaciones       INT              NULL,
    Numero_Banos              INT              NULL,
    Estrato                   INT              NULL,
    Tiene_Parqueadero         BIT              DEFAULT 0,
    Numero_Pisos              INT              NULL,
    Estado                    VARCHAR(50)      DEFAULT 'Disponible',
    Anio_Construccion         INT              NULL,
    FOREIGN KEY (Codigo_Ciudad) REFERENCES CIUDAD(Codigo_Ciudad),
    FOREIGN KEY (Codigo_TipoInmueble) REFERENCES TIPO_INMUEBLE(Codigo_TipoInmueble),
    FOREIGN KEY (Codigo_Empleado_Captacion) REFERENCES EMPLEADO(Codigo_Empleado)
);
GO

INSERT INTO INMUEBLE 
(Direccion, Codigo_Ciudad, Codigo_TipoInmueble, Es_Nuevo, Fecha_Alta, Descripcion, Codigo_Empleado_Captacion, 
 Precio_Venta, Canon_Mensual, Metros_Cuadrados, Numero_Habitaciones, Numero_Banos, Estrato, Tiene_Parqueadero, Numero_Pisos, Estado, Anio_Construccion)
VALUES 
('Calle 100 #20-30', 1, 1, 1, '2024-01-15', 'Apartamento nuevo con balcón', 2, 
 250000000, NULL, 80.5, 3, 2, 4, 1, 1, 'Disponible', 2023),

('Cra 50 #40-10', 2, 2, 0, '2023-11-20', 'Casa usada con patio', 1, 
 350000000, NULL, 120.0, 4, 3, 3, 1, 2, 'Disponible', 2010),

('Av. 30 #10-50', 3, 3, 0, '2023-10-05', 'Local comercial en arriendo', 2, 
 NULL, 2000000, 60.0, 0, 1, 5, 0, 1, 'Arrendado', 2015);
GO

-- 11. Inmuebles en Consignación
CREATE TABLE INMUEBLE_CONSIGNACION (
    Codigo_Inmueble      INT PRIMARY KEY,
    Nombre_Propietario   VARCHAR(100) NOT NULL,
    Telefono_Propietario VARCHAR(100),
    Email_Propietario    VARCHAR(100),
    FOREIGN KEY (Codigo_Inmueble) REFERENCES INMUEBLE(Codigo_Inmueble)
);
GO
INSERT INTO INMUEBLE_CONSIGNACION (Codigo_Inmueble, Nombre_Propietario, Telefono_Propietario, Email_Propietario)
VALUES 
(2, 'Mario Gómez', '3023456789', 'mario@mail.com');
GO

-- 12. Proyectos Nuevos
CREATE TABLE PROYECTO_NUEVO (
    Codigo_Proyecto      INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Proyecto      VARCHAR(255)      NOT NULL,
    Direccion            VARCHAR(255)      NOT NULL,
    Codigo_Ciudad        INT               NOT NULL,
    FOREIGN KEY (Codigo_Ciudad) REFERENCES CIUDAD(Codigo_Ciudad)
);
GO
INSERT INTO PROYECTO_NUEVO (Nombre_Proyecto, Direccion, Codigo_Ciudad)
VALUES 
('Conjunto El Bosque', 'Carrera 80 #55-12', 1);
GO
-- 13. Relación Inmueble–Proyecto
CREATE TABLE INMUEBLE_PROYECTO (
    Codigo_Inmueble      INT NOT NULL,
    Codigo_Proyecto      INT NOT NULL,
    PRIMARY KEY (Codigo_Inmueble, Codigo_Proyecto),
    FOREIGN KEY (Codigo_Inmueble) REFERENCES INMUEBLE(Codigo_Inmueble),
    FOREIGN KEY (Codigo_Proyecto) REFERENCES PROYECTO_NUEVO(Codigo_Proyecto)
);
GO
INSERT INTO INMUEBLE_PROYECTO (Codigo_Inmueble, Codigo_Proyecto)
VALUES (1, 1);
GO
-- 14. Visitas
CREATE TABLE VISITA (
    Codigo_Visita        INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Inmueble      INT              NOT NULL,
    Codigo_Cliente       INT              NOT NULL,
    Fecha_Visita         DATETIME         NOT NULL,
    Comentarios          TEXT,
    Codigo_Empleado      INT              NULL,
    FOREIGN KEY (Codigo_Inmueble) REFERENCES INMUEBLE(Codigo_Inmueble),
    FOREIGN KEY (Codigo_Cliente) REFERENCES CLIENTE(Codigo_Cliente),
    FOREIGN KEY (Codigo_Empleado) REFERENCES EMPLEADO(Codigo_Empleado)
);
GO
INSERT INTO VISITA (Codigo_Inmueble, Codigo_Cliente, Fecha_Visita, Comentarios, Codigo_Empleado)
VALUES 
(1, 1, '2025-05-01 10:00', 'El cliente mostró interés.', 1),
(2, 2, '2025-05-02 14:00', 'Solicitó cotización.', NULL);
GO
-- 15. Proveedores
CREATE TABLE PROVEEDOR (
    Codigo_Proveedor     INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Proveedor     VARCHAR(255)      NOT NULL,
    Nombre_Contacto      VARCHAR(100),
    Tipo_Telefono        INT              NULL,
    Telefono             VARCHAR(100),
    Email                VARCHAR(100),
    Direccion            VARCHAR(255),
    FOREIGN KEY (Tipo_Telefono) REFERENCES TIPO_TELEFONO(Codigo_TipoTelefono)
);
GO
INSERT INTO PROVEEDOR (Nombre_Proveedor, Nombre_Contacto, Tipo_Telefono, Telefono, Email, Direccion)
VALUES 
('Muebles del Sur', 'Andrés Peña', 1, '3129876543', 'ventas@muebles.com', 'Calle 90 #50-20');
GO
-- 16. Productos/Servicios de Proveedor
CREATE TABLE PRODUCTO_SERVICIO_PROVEEDOR (
    Codigo_Producto      INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Proveedor     INT               NOT NULL,
    Nombre_Producto      VARCHAR(255)      NOT NULL,
    Unidad_Medida        VARCHAR(50),
    Precio_Unitario      DECIMAL(18,2)     NOT NULL,
    FOREIGN KEY (Codigo_Proveedor) REFERENCES PROVEEDOR(Codigo_Proveedor)
);
GO
INSERT INTO PRODUCTO_SERVICIO_PROVEEDOR (Codigo_Proveedor, Nombre_Producto, Unidad_Medida, Precio_Unitario)
VALUES 
(1, 'Sofá 3 puestos', 'unidad', 1800000),
(1, 'Mesa de comedor', 'unidad', 1200000);
GO
-- 17. Compras a Proveedor (cabecera)
CREATE TABLE COMPRA_PROVEEDOR (
    Codigo_Compra        INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Proveedor     INT               NOT NULL,
    Fecha_Compra         DATE              NOT NULL,
    Monto_Total          DECIMAL(18,2)     NOT NULL,
    Descripcion          TEXT,
    Codigo_Empleado      INT               NOT NULL,
    FOREIGN KEY (Codigo_Proveedor) REFERENCES PROVEEDOR(Codigo_Proveedor),
    FOREIGN KEY (Codigo_Empleado) REFERENCES EMPLEADO(Codigo_Empleado)
);
GO
INSERT INTO COMPRA_PROVEEDOR (Codigo_Proveedor, Fecha_Compra, Monto_Total, Descripcion, Codigo_Empleado)
VALUES 
(1, '2025-05-10', 3000000, 'Compra de mobiliario para modelo', 1);
GO
-- 18. Detalle de Compra a Proveedor
CREATE TABLE DETALLE_COMPRA_PROVEEDOR (
    Detalle_ID           INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Compra        INT               NOT NULL,
    Codigo_Producto      INT               NOT NULL,
    Cantidad             INT               NOT NULL,
    Precio_Unitario      DECIMAL(18,2)     NOT NULL,
    FOREIGN KEY (Codigo_Compra) REFERENCES COMPRA_PROVEEDOR(Codigo_Compra),
    FOREIGN KEY (Codigo_Producto) REFERENCES PRODUCTO_SERVICIO_PROVEEDOR(Codigo_Producto)
);
GO
INSERT INTO DETALLE_COMPRA_PROVEEDOR (Codigo_Compra, Codigo_Producto, Cantidad, Precio_Unitario)
VALUES 
(1, 1, 1, 1800000),
(1, 2, 1, 1200000);
GO
-- 19. Decoración de Viviendas Modelo
CREATE TABLE DECORACION_MODELO (
    Codigo_Decoracion    INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Estilo        VARCHAR(100),
    Descripcion          TEXT,
    Costo                DECIMAL(18,2),
    Fecha_Inicio         DATE,
    Fecha_Fin            DATE,
    Codigo_Empleado      INT              NULL,
    FOREIGN KEY (Codigo_Empleado) REFERENCES EMPLEADO(Codigo_Empleado)
);
GO
INSERT INTO DECORACION_MODELO (Nombre_Estilo, Descripcion, Costo, Fecha_Inicio, Fecha_Fin, Codigo_Empleado)
VALUES 
('Estilo Nórdico', 'Decoración con tonos claros y madera', 2500000, '2025-03-01', '2025-04-01', 2);
GO
-- 20. Vinculación Inmueble–Decoración
CREATE TABLE INMUEBLE_MODELO_DECORACION (
    Codigo_Inmueble      INT NOT NULL,
    Codigo_Decoracion    INT NOT NULL,
    PRIMARY KEY (Codigo_Inmueble, Codigo_Decoracion),
    FOREIGN KEY (Codigo_Inmueble) REFERENCES INMUEBLE(Codigo_Inmueble),
    FOREIGN KEY (Codigo_Decoracion) REFERENCES DECORACION_MODELO(Codigo_Decoracion)
);
GO
INSERT INTO INMUEBLE_MODELO_DECORACION (Codigo_Inmueble, Codigo_Decoracion)
VALUES (1, 1);
GO
-- 21. Tipos de Transacción (con % Comisión)
CREATE TABLE TIPO_TRANSACCION (
    Codigo_TipoTransaccion INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion            VARCHAR(50)     NOT NULL,
    Porcentaje_Comision    DECIMAL(5,2)   NOT NULL
);
GO
INSERT INTO TIPO_TRANSACCION (Descripcion, Porcentaje_Comision)
VALUES ('Venta', 3.00), ('Arriendo', 1.50);
GO
-- 22. Tipos de Pago
CREATE TABLE TIPO_PAGO (
    Codigo_TipoPago   INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion       VARCHAR(50)        NOT NULL
);
GO
INSERT INTO TIPO_PAGO (Descripcion)
VALUES ('Transferencia'), ('Efectivo'), ('Tarjeta de Crédito');
GO
-- 23. Formas de Pago
CREATE TABLE FORMA_PAGO (
    Codigo_Forma      INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_TipoPago   INT              NOT NULL,
    Valor             DECIMAL(18,2)    NOT NULL,
    FOREIGN KEY (Codigo_TipoPago) REFERENCES TIPO_PAGO(Codigo_TipoPago)
);
GO
INSERT INTO FORMA_PAGO (Codigo_TipoPago, Valor)
VALUES (1, 200000000), (2, 50000000);
GO
-- 24. Transacciones (base común)
CREATE TABLE TRANSACCION (
    Codigo_Transaccion     INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Inmueble        INT               NOT NULL,
    Codigo_Cliente         INT               NOT NULL,
    Codigo_Empleado_Cierre INT               NOT NULL,
    Codigo_TipoTransaccion INT               NOT NULL,
    Fecha_Transaccion      DATE              NOT NULL,
    Precio_Acordado        DECIMAL(18,2)     NOT NULL,
    Comision_Agente        DECIMAL(18,2)     NOT NULL,
    Notas_Transaccion      TEXT,
    FOREIGN KEY (Codigo_Inmueble) REFERENCES INMUEBLE(Codigo_Inmueble),
    FOREIGN KEY (Codigo_Cliente) REFERENCES CLIENTE(Codigo_Cliente),
    FOREIGN KEY (Codigo_Empleado_Cierre) REFERENCES EMPLEADO(Codigo_Empleado),
    FOREIGN KEY (Codigo_TipoTransaccion) REFERENCES TIPO_TRANSACCION(Codigo_TipoTransaccion)
);
GO
INSERT INTO TRANSACCION (Codigo_Inmueble, Codigo_Cliente, Codigo_Empleado_Cierre, Codigo_TipoTransaccion, Fecha_Transaccion, Precio_Acordado, Comision_Agente, Notas_Transaccion)
VALUES 
(1, 1, 1, 1, '2025-05-12', 250000000, 7500000, 'Cierre exitoso. Cliente satisfecho.'),
(3, 2, 2, 2, '2025-05-15', 2000000, 30000, 'Contrato de arriendo por 12 meses.');

GO

-- 25. Venta (subtabla específica)
CREATE TABLE VENTA (
    Codigo_Transaccion INT PRIMARY KEY,
    Fecha_Pago         DATE               NOT NULL,
    Monto_Pagado       DECIMAL(18,2)      NOT NULL,
    Estado_Pago        VARCHAR(50)        NOT NULL DEFAULT 'Pendiente',
    Notas_Venta        TEXT               NULL,
    FOREIGN KEY (Codigo_Transaccion) REFERENCES TRANSACCION(Codigo_Transaccion)
);
GO
INSERT INTO VENTA (Codigo_Transaccion, Fecha_Pago, Monto_Pagado, Estado_Pago, Notas_Venta)
VALUES (1, '2025-05-13', 250000000, 'Pagado', 'Pago completo de la propiedad.');
GO
-- 26. Arriendo (subtabla específica)
CREATE TABLE ARRIENDO (
    Codigo_Transaccion      INT PRIMARY KEY,
    Fecha_Inicio_Contrato   DATE              NOT NULL,
    Fecha_Fin_Contrato      DATE              NOT NULL,
    Duracion_Contrato_Meses INT               NOT NULL,
    FOREIGN KEY (Codigo_Transaccion) REFERENCES TRANSACCION(Codigo_Transaccion)
);
GO
INSERT INTO ARRIENDO (Codigo_Transaccion, Fecha_Inicio_Contrato, Fecha_Fin_Contrato, Duracion_Contrato_Meses)
VALUES (2, '2025-06-01', '2026-05-31', 12);
GO
-- 27. Facturas
CREATE TABLE FACTURA (
    Codigo_Factura         INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Transaccion     INT               NOT NULL,
    Codigo_Cliente         INT               NOT NULL,
    Codigo_Inmueble        INT               NOT NULL,
    Codigo_Empleado_Genera INT               NULL,
    Codigo_Forma           INT               NOT NULL,
    Fecha_Emision          DATE              NOT NULL,
    Fecha_Vencimiento      DATE              NULL,
    Monto_Total            DECIMAL(18,2)     NOT NULL,
    Concepto               VARCHAR(255)      NOT NULL,
    Estado                 VARCHAR(50)       DEFAULT 'Pendiente',
    Fecha_Pago             DATE              NULL,
    Notas_Factura          TEXT,
    FOREIGN KEY (Codigo_Transaccion) REFERENCES TRANSACCION(Codigo_Transaccion),
    FOREIGN KEY (Codigo_Cliente) REFERENCES CLIENTE(Codigo_Cliente),
    FOREIGN KEY (Codigo_Inmueble) REFERENCES INMUEBLE(Codigo_Inmueble),
    FOREIGN KEY (Codigo_Empleado_Genera) REFERENCES EMPLEADO(Codigo_Empleado),
    FOREIGN KEY (Codigo_Forma) REFERENCES FORMA_PAGO(Codigo_Forma)
);
GO
INSERT INTO FACTURA (
    Codigo_Transaccion, Codigo_Cliente, Codigo_Inmueble, Codigo_Empleado_Genera, Codigo_Forma,
    Fecha_Emision, Fecha_Vencimiento, Monto_Total, Concepto, Estado, Fecha_Pago, Notas_Factura
)
VALUES (
    2, 2, 3, 2, 2,  -- forma de pago = efectivo
    '2025-05-16', '2025-06-01', 2000000, 'Canon de arriendo mensual', 'Completado', '2025-06-01', 'Pago en proceso'
);
GO
-- 28. Usuarios
CREATE TABLE USUARIO (
    Codigo_Usuario       INT IDENTITY(1,1) PRIMARY KEY,
    Documento_Empleado   INT               NULL,
    Username             VARCHAR(100)      NOT NULL,
    Clave                NVARCHAR(2000)    NOT NULL,
    Salt                 NVARCHAR(2000)    NULL,
    FOREIGN KEY (Documento_Empleado) REFERENCES EMPLEADO(Codigo_Empleado)
);
GO

-- 29. Perfiles
CREATE TABLE PERFIL (
    Codigo_Perfil   INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion     VARCHAR(100)     NOT NULL,
    PaginaNavegar   NVARCHAR(200)    NOT NULL
);
GO
INSERT INTO PERFIL (Descripcion, PaginaNavegar) VALUES
('Administrador','Panel.html'), ('Empleado','Index.html');
GO

-- 30. Asociación Perfil–Usuario
CREATE TABLE PERFIL_USUARIO (
    Codigo              INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Usuario      INT               NOT NULL,
    Codigo_Perfil       INT               NOT NULL,
    Activo              BIT               NOT NULL,
    FOREIGN KEY (Codigo_Usuario) REFERENCES USUARIO(Codigo_Usuario),
    FOREIGN KEY (Codigo_Perfil) REFERENCES PERFIL(Codigo_Perfil)
);
GO
-- 31. IMAGEN_INMUEBLE
CREATE TABLE IMAGEN_INMUEBLE (
    Codigo_Imagen       INT IDENTITY(1,1) PRIMARY KEY,
    Codigo_Inmueble     INT NOT NULL,
    Url_Imagen          VARCHAR(500) NOT NULL,
    Descripcion         VARCHAR(255) NULL,
    Es_Principal        BIT DEFAULT 0, -- 1 si es la imagen destacada
    Fecha_Subida        DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (Codigo_Inmueble) REFERENCES INMUEBLE(Codigo_Inmueble)
);