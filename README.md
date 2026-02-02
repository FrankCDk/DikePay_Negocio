Docker command:

docker build -t dikepay-api -f DikePay.Api/Dockerfile .
docker run -d -p 9000:8080 -p 9001:8081 --name dikepay-api dikepay-api-image


```sql

create database DikePay;
use DikePay;

select * from articulos;

CREATE TABLE articulos (
    -- Identificador único (GUID) almacenado como CHAR(36)
    id CHAR(36) NOT NULL,
    
    -- Versión para control de concurrencia y sincronización
    version VARCHAR(20) NOT NULL DEFAULT '1.0.0',
    
    -- Identidad de negocio con índice único para evitar duplicados
    codigo VARCHAR(50) NOT NULL,
    codigo_sku VARCHAR(50) NULL,
    nombre VARCHAR(200) NOT NULL,
    
    -- Finanzas: Usamos DECIMAL(18, 4) para precisión exacta en dinero y stock
    precio DECIMAL(18, 4) NOT NULL DEFAULT 0.0000,
    stock DECIMAL(18, 4) NOT NULL DEFAULT 0.0000,
    stock_minimo DECIMAL(18, 4) NOT NULL DEFAULT 0.0000,
    
    -- Campos SUNAT
    codigo_producto_sunat VARCHAR(20) NULL,
    unidad_medida VARCHAR(10) NOT NULL,
    tipo_articulo VARCHAR(50) NULL,
    tipo_existencia_sunat VARCHAR(10) NULL,
    
    -- Flags: En MySQL 'bool' es un alias de TINYINT(1) (0 o 1)
    acepta_decimales TINYINT(1) NOT NULL DEFAULT 0,
    tiene_serie TINYINT(1) NOT NULL DEFAULT 0,
    tiene_lote TINYINT(1) NOT NULL DEFAULT 0,
    controla_stock TINYINT(1) NOT NULL DEFAULT 1,
    es_precio_libre TINYINT(1) NOT NULL DEFAULT 0,
    
    -- Otros datos de negocio
    moneda VARCHAR(5) NOT NULL DEFAULT 'PEN',
    porcentaje_descuento DECIMAL(5, 2) NOT NULL DEFAULT 0.00,
    tipo_afectacion VARCHAR(10) NOT NULL, -- GR, IN, EX
    estado CHAR(1) NOT NULL DEFAULT 'V', -- V: Vigente, A: Anulado, B: Bloqueado
    
    -- Auditoría
    fecha_creacion DATETIME NOT NULL,
    usuario_creacion VARCHAR(50) NOT NULL,
    fecha_modificacion DATETIME NOT NULL,
    usuario_modificacion VARCHAR(50) NOT NULL,

    -- Constraints (Restricciones)
    PRIMARY KEY (id),
    UNIQUE INDEX idx_articulos_codigo (codigo) -- Evita duplicados a nivel DB
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE promociones (
    id CHAR(36) PRIMARY KEY NOT NULL, -- PK Técnica (GUID)
    codigo_promocion VARCHAR(50) UNIQUE NOT NULL, -- Tu código personalizado
    nombre VARCHAR(100) NOT NULL, -- Ej: "Super Pack Leche"
    descripcion VARCHAR(255),     -- Ej: "Lleva 3 y paga S/ 10"
    
    -- Tipos: 'CANTIDAD_FIJA' (3 por 10), 'PORCENTAJE' (-10% si lleva 5), 'BONIFICACION' (2x1)
    tipo_promocion VARCHAR(30) NOT NULL, 
    
    articulo_id CHAR(36) NOT NULL, -- Relación con tu tabla artículos
    cantidad_minima DECIMAL(18, 4) NOT NULL DEFAULT 1,
    
    -- Valores de la oferta
    nuevo_precio DECIMAL(18, 4),    -- Para 'CANTIDAD_FIJA'
    porcentaje_descuento DECIMAL(5,2), -- Para 'PORCENTAJE'
    
    -- Vigencia
    fecha_inicio DATETIME NOT NULL,
    fecha_fin DATETIME NOT NULL,
    estado CHAR(1) DEFAULT 'V', -- V: Vigente, A: Anulado
    
    FOREIGN KEY (articulo_id) REFERENCES articulos(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE usuarios (
    codigo_usuario VARCHAR(50) PRIMARY KEY,
    nombre_completo VARCHAR(150) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL, -- Nunca guardar texto plano
    rol VARCHAR(50) DEFAULT 'Vendedor',
    estado BOOLEAN DEFAULT TRUE,
    
    -- Auditoría
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ultima_conexion DATETIME,
    
    -- Para JWT (Opcional pero recomendado)
    refresh_token VARCHAR(255),
    token_expiracion DATETIME
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE codigos_autenticacion_movil (
    id CHAR(36) PRIMARY KEY, -- Para almacenar el Guid de .NET
    codigo_usuario VARCHAR(50) NOT NULL,
    codigo_autenticacion VARCHAR(10) NOT NULL,
    es_usado BOOLEAN DEFAULT FALSE,
    fecha_expiracion DATETIME NOT NULL,
    
    -- Relación con la tabla usuarios
    CONSTRAINT FK_Codigos_Usuarios FOREIGN KEY (codigo_usuario) 
    REFERENCES usuarios(codigo_usuario) ON DELETE CASCADE,
    
    -- Índice para que la búsqueda por código sea ultra rápida
    INDEX IX_codigo_autenticacion (codigo_autenticacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabla: AppVersions
CREATE TABLE app_versions (
    id CHAR(36) PRIMARY KEY, -- Almacenaremos el UUID aquí
    platform TINYINT NOT NULL COMMENT '1: Android, 2: iOS, 3: Windows',
    version_number VARCHAR(20) NOT NULL,
    build_number INT NOT NULL,
    is_critical_update TINYINT(1) NOT NULL DEFAULT 0,
    release_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    download_url VARCHAR(255),
    is_active TINYINT(1) NOT NULL DEFAULT 1
);

-- Tabla: AppReleaseNotes
CREATE TABLE app_release_notes (
    id CHAR(36) PRIMARY KEY,
    app_version_id CHAR(36) NOT NULL,
    language_code VARCHAR(5) NOT NULL, -- 'es', 'en'
    notes TEXT NOT NULL,
    CONSTRAINT fk_version FOREIGN KEY (app_version_id) 
        REFERENCES app_versions(id) ON DELETE CASCADE
);

-- Tabla: GlobalSettings
CREATE TABLE global_settings (
    config_key VARCHAR(100) PRIMARY KEY,
    config_value TEXT NOT NULL,
    description VARCHAR(255),
    last_updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);



CREATE TABLE agencias (
    id INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(255),
    ubigeo VARCHAR(6), -- Estándar SUNAT para localización
    codigo_anexo_sunat VARCHAR(4) DEFAULT '0000', -- Código de establecimiento
    esta_activo TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE tipos_documentos (
    id CHAR(2) PRIMARY KEY, -- '01', '03', '07', '08'
    descripcion VARCHAR(50) NOT NULL,
    abreviatura VARCHAR(10),
    es_electronico TINYINT(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE clientes (
    id CHAR(36) PRIMARY KEY, -- UUID para sincronización móvil
    tipo_documento_identidad CHAR(1) NOT NULL, -- 1: DNI, 6: RUC, 4: Carnet Extr.
    numero_documento VARCHAR(15) NOT NULL UNIQUE,
    nombre_razon_social VARCHAR(255) NOT NULL,
    direccion_principal TEXT,
    correo_electronico VARCHAR(100),
    telefono VARCHAR(20),
    esta_activo TINYINT(1) DEFAULT 1,
    fecha_creacion DATETIME DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE series_correlativos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_agencia INT NOT NULL,
    tipo_documento_id CHAR(2) NOT NULL,
    serie VARCHAR(4) NOT NULL, -- Ej: 'F001', 'B001'
    ultimo_numero INT NOT NULL DEFAULT 0, -- El último emitido
    
    FOREIGN KEY (id_agencia) REFERENCES agencias(id),
    FOREIGN KEY (tipo_documento_id) REFERENCES tipos_documentos(id),
    UNIQUE KEY uq_serie_agencia (id_agencia, tipo_documento_id, serie)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE facturas (
    id CHAR(36) PRIMARY KEY, -- UUID para sincronización
    id_agencia INT NOT NULL,
    tipo_documento_id CHAR(2) NOT NULL, -- 01: Factura, 03: Boleta
    serie VARCHAR(4) NOT NULL,
    numero VARCHAR(10) NOT NULL,
    fecha_emision DATETIME NOT NULL,
    
    -- Datos del Cliente
    cliente_id CHAR(36) NOT NULL,
    cliente_nombre VARCHAR(255) NOT NULL,
    cliente_numero_documento VARCHAR(15) NOT NULL, -- RUC o DNI
    cliente_direccion TEXT,
    
    -- Información Financiera
    moneda_id CHAR(3) NOT NULL, -- PEN o USD
    tipo_cambio DECIMAL(10, 3) DEFAULT 1.000,
    monto_total_soles DECIMAL(18, 2) NOT NULL,
    monto_total_dolares DECIMAL(18, 2) NOT NULL,
    total_igv DECIMAL(18, 2) NOT NULL,
    total_descuento DECIMAL(18, 2) DEFAULT 0.00,
    
    -- Estados y Control
	estado_documento_id TINYINT NOT NULL COMMENT '1: Borrador, 2: Emitido, 3: Anulado',
    estado_sunat_id TINYINT NOT NULL DEFAULT 0 COMMENT '0: Pendiente, 1: Aceptado, 2: Rechazado',
	fecha_cdr DATETIME NULL,
    esta_sincronizado TINYINT(1) NOT NULL DEFAULT 0,
    
    -- Auditoría
    usuario_creacion_id CHAR(50),
    fecha_creacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    
	usuario_modificacion_id CHAR(50),
    fecha_modificacion DATETIME NULL,
    
    -- RELACIONES (Nuevas)
    FOREIGN KEY (id_agencia) REFERENCES agencias(id),
    FOREIGN KEY (tipo_documento_id) REFERENCES tipos_documentos(id),
    FOREIGN KEY (cliente_id) REFERENCES clientes(id),
    
    -- Índice para búsqueda rápida de correlativos
    UNIQUE KEY uq_comprobante (tipo_documento_id, serie, numero)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci; 

CREATE TABLE facturas_detalle (
    id CHAR(36) PRIMARY KEY,
    factura_id CHAR(36) NOT NULL, -- Relación con la cabecera
    agencia_id INT NOT NULL,
    tipo_documento_id CHAR(2) NOT NULL,
    serie VARCHAR(4) NOT NULL,
    numero VARCHAR(10) NOT NULL,
    item_secuencia INT NOT NULL, -- Orden de los productos (1, 2, 3...)
    
    -- Producto/Articulo
    articulo_id CHAR(36) NOT NULL,
    articulo_codigo VARCHAR(50) NOT NULL,
    articulo_descripcion VARCHAR(255) NOT NULL,
    unidad_medida_id VARCHAR(5) NOT NULL, -- NIU, ZZ, etc.
    cantidad DECIMAL(18, 4) NOT NULL,
    
    -- Precios e Impuestos
    precio_unitario DECIMAL(18, 2) NOT NULL,
    monto_base_igv DECIMAL(18, 2) NOT NULL, -- Subtotal sin impuestos
    igv_monto DECIMAL(18, 2) NOT NULL,
    igv_tasa DECIMAL(5, 2) DEFAULT 18.00, -- 18% por defecto
    
    -- Totales calculados
    total_soles DECIMAL(18, 2) NOT NULL,
    total_dolares DECIMAL(18, 2) NOT NULL,
    
    -- Descuentos
    porcentaje_descuento DECIMAL(5, 2) DEFAULT 0.00,
    monto_descuento DECIMAL(18, 2) DEFAULT 0.00,
    
    esta_sincronizado TINYINT(1) NOT NULL DEFAULT 0,
    
    FOREIGN KEY (articulo_id) REFERENCES articulos(id),
    FOREIGN KEY (factura_id) REFERENCES facturas(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

use DikePay;
select * from articulos WHERE id = '29415f3b-f5b9-11f0-b3d1-98e74358acac';
select * from usuarios;
select * from codigos_autenticacion_movil;
select * from app_versions;
select * from promociones where articulo_id = '29415f3b-f5b9-11f0-b3d1-98e74358acac';  


INSERT INTO articulos (
    id, version, codigo, codigo_sku, nombre, precio, stock, stock_minimo, 
    codigo_producto_sunat, unidad_medida, tipo_articulo, tipo_existencia_sunat, 
    acepta_decimales, tiene_serie, tiene_lote, controla_stock, es_precio_libre, 
    moneda, porcentaje_descuento, tipo_afectacion, estado, 
    fecha_creacion, usuario_creacion, fecha_modificacion, usuario_modificacion
) VALUES
(UUID(), '1.0.0', 'ART001', 'SKU-789001', 'Laptop ZenBook 14 OLED', 4500.00, 10.00, 2.00, '43211503', 'NIU', 'PRODUCTO', '01', 0, 1, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART002', 'SKU-789002', 'Mouse Logi MX Master 3S', 420.00, 25.00, 5.00, '43211708', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART003', 'SKU-789003', 'Teclado Mecánico Keychron K2', 380.00, 15.00, 3.00, '43211706', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART004', 'SKU-789004', 'Monitor LG 27" 4K IPS', 1250.00, 8.00, 1.00, '43211902', 'NIU', 'PRODUCTO', '01', 0, 1, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART005', 'SKU-789005', 'Disco Externo SSD 1TB Samsung', 550.00, 30.00, 5.00, '43202010', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART006', 'SKU-789006', 'Cámara Web Razer Kiyo Pro', 680.00, 12.00, 2.00, '45121517', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART007', 'SKU-789007', 'Audífonos Sony WH-1000XM5', 1450.00, 5.00, 1.00, '52161505', 'NIU', 'PRODUCTO', '01', 0, 1, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART008', 'SKU-789008', 'Silla Ergonómica Pro-Gamer', 890.00, 10.00, 2.00, '56112102', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART009', 'SKU-100101', 'Aceite Primor Premium 1L', 12.50, 100.00, 10.00, '50151513', 'NIU', 'PRODUCTO', '01', 1, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART010', 'SKU-100102', 'Arroz Costeño Saco 5kg', 24.80, 50.00, 5.00, '50221101', 'NIU', 'PRODUCTO', '01', 1, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART011', 'SKU-100103', 'Azúcar Rubia Dulfina 1kg', 4.20, 200.00, 20.00, '50161509', 'NIU', 'PRODUCTO', '01', 1, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART012', 'SKU-100104', 'Leche Gloria Azul 400g', 3.80, 144.00, 24.00, '50131702', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART013', 'SKU-100105', 'Fideos Lavaggi Tallarín 500g', 3.50, 80.00, 10.00, '50191512', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART014', 'SKU-100106', 'Café Altomayo Instantáneo 180g', 18.90, 40.00, 5.00, '50201706', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART015', 'SKU-100107', 'Detergente Ariel 2kg', 22.50, 35.00, 5.00, '47131811', 'NIU', 'PRODUCTO', '01', 1, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART016', 'SKU-100108', 'Papel Higiénico Elite 12 un', 15.60, 60.00, 10.00, '14111704', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART017', 'SKU-200201', 'Martillo Truper 16oz', 35.00, 20.00, 2.00, '27111602', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART018', 'SKU-200202', 'Taladro Inalámbrico Bosch 18V', 580.00, 6.00, 1.00, '27112703', 'NIU', 'PRODUCTO', '01', 0, 1, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART019', 'SKU-200203', 'Cinta Métrica Stanley 8m', 42.00, 15.00, 3.00, '41111601', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART020', 'SKU-200204', 'Set de Destornilladores 10 pzas', 85.00, 10.00, 2.00, '27111729', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART021', 'SKU-300301', 'Coca Cola 2.5L No Retornable', 10.50, 120.00, 12.00, '50202306', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART022', 'SKU-300302', 'Inka Cola 500ml', 3.00, 200.00, 24.00, '50202306', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART023', 'SKU-300303', 'Cerveza Pilsen Six Pack', 32.00, 48.00, 6.00, '50202203', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART024', 'SKU-300304', 'Agua Cielo 625ml', 1.50, 300.00, 50.00, '50202301', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART025', 'SKU-400401', 'Red Label Johnnie Walker 750ml', 75.00, 24.00, 4.00, '50202206', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART026', 'SKU-400402', 'Vino Tabernero Borgoña', 22.00, 36.00, 6.00, '50202203', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART027', 'SKU-500501', 'Shampoo H&S 375ml', 18.50, 45.00, 5.00, '53131628', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART028', 'SKU-500502', 'Jabón Dove 3 pack', 14.20, 55.00, 10.00, '53131608', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART029', 'SKU-500503', 'Crema Dental Colgate 100g', 6.80, 90.00, 12.00, '53131502', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART030', 'SKU-500504', 'Desodorante Rexona Men', 12.50, 40.00, 5.00, '53131615', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'SERV-001', NULL, 'Consultoría Técnica IT (Hora)', 150.00, 999.00, 0.00, '80101507', 'ZZ', 'SERVICIO', '00', 1, 0, 0, 0, 1, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'SERV-002', NULL, 'Mantenimiento de PC', 80.00, 999.00, 0.00, '81111801', 'ZZ', 'SERVICIO', '00', 0, 0, 0, 0, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'SERV-003', NULL, 'Instalación de Software', 45.00, 999.00, 0.00, '81112204', 'ZZ', 'SERVICIO', '00', 0, 0, 0, 0, 1, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART034', 'SKU-600601', 'Zapatillas Nike Air Max', 480.00, 15.00, 2.00, '53111602', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART035', 'SKU-600602', 'Camiseta Selección Perú', 199.00, 30.00, 5.00, '53103001', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART036', 'SKU-600603', 'Pantalón Jean Levi 511', 250.00, 20.00, 3.00, '53101501', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART037', 'SKU-700701', 'Pan de Molde Bimbo', 9.50, 40.00, 5.00, '50181901', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART038', 'SKU-700702', 'Mantequilla Gloria 200g', 7.20, 60.00, 10.00, '50131701', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART039', 'SKU-700703', 'Mermelada Fanny Fresa 310g', 6.50, 50.00, 8.00, '50192422', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART040', 'SKU-800801', 'Sartén Tramontina 24cm', 55.00, 15.00, 2.00, '52151506', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART041', 'SKU-800802', 'Olla a Presión Record 5L', 145.00, 8.00, 1.00, '52151502', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART042', 'SKU-800803', 'Set de Cubiertos 24 pzas', 78.00, 12.00, 2.00, '52151503', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART043', 'SKU-900901', 'Cuaderno Standford A4', 8.50, 100.00, 20.00, '44111505', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART044', 'SKU-900902', 'Pack de Lapiceros Pilot x3', 14.00, 60.00, 12.00, '44121701', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART045', 'SKU-900903', 'Resaltadores Faber Castell x4', 12.50, 45.00, 10.00, '44121707', 'NIU', 'PRODUCTO', '01', 0, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART046', 'SKU-110110', 'Chocolate Sublime 40g', 2.50, 150.00, 24.00, '50161813', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART047', 'SKU-110111', 'Galleta Oreo Pack x6', 5.50, 80.00, 12.00, '50181902', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART048', 'SKU-110112', 'Papas Lays Clásicas Familiares', 8.90, 40.00, 5.00, '50192109', 'NIU', 'PRODUCTO', '01', 0, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART049', 'SKU-120120', 'Alimento para Perro Ricocan 2kg', 18.00, 25.00, 3.00, '10121001', 'NIU', 'PRODUCTO', '01', 1, 0, 1, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA'),
(UUID(), '1.0.0', 'ART050', 'SKU-120121', 'Arena para Gato Sepicat 5kg', 28.50, 15.00, 2.00, '10121002', 'NIU', 'PRODUCTO', '01', 1, 0, 0, 1, 0, 'PEN', 0.00, 'GR', 'V', NOW(), 'SISTEMA', NOW(), 'SISTEMA');


select * from promociones;
-- 1. Oferta de Precio Fijo: Pack de 2 Laptops por S/ 8500 (Ahorra S/ 500)
INSERT INTO promociones (
    id, codigo_promocion, nombre, descripcion, tipo_promocion, articulo_id, 
    cantidad_minima, nuevo_precio, porcentaje_descuento, 
    fecha_inicio, fecha_fin, estado
) VALUES (
	UUID(),
    'PROM-001-FIXED', 
    'Pack Corporativo (2 unidades)', 
    'Lleva 2 unidades por S/ 8500.00', 
    'CANTIDAD_FIJA', 
    '29415f3b-f5b9-11f0-b3d1-98e74358acac', 
    2, 
    4250.0000, -- Precio unitario resultante (8500 / 2)
    NULL, 
    '2026-01-01 00:00:00', 
    '2026-12-31 23:59:59', 
    'V'
);

-- 2. Oferta por Porcentaje: 10% de descuento si lleva 3 o más
INSERT INTO promociones (
    id, codigo_promocion, nombre, descripcion, tipo_promocion, articulo_id, 
    cantidad_minima, nuevo_precio, porcentaje_descuento, 
    fecha_inicio, fecha_fin, estado
) VALUES (
	UUID(),
    'PROM-002-PERC', 
    'Descuento por Volumen (3+)', 
    '10% de descuento a partir de 3 unidades', 
    'PORCENTAJE', 
    '29415f3b-f5b9-11f0-b3d1-98e74358acac', 
    3, 
    NULL, 
    10.00, 
    '2026-01-01 00:00:00', 
    '2026-12-31 23:59:59', 
    'V'
);


```