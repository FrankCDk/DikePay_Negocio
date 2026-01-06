Docker command:

docker build -t dikepay-api -f DikePay.Api/Dockerfile .
docker run -d -p 9000:8080 -p 9001:8081 --name dikepay-api dikepay-api-image


```sql
create database DikePay;
use DikePay;

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

select * from articulos;
select * from usuarios;
