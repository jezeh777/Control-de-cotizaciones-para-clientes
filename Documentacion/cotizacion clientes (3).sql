CREATE TABLE [CLIENTES] (
  [IDCLIENTE] INT PRIMARY KEY,
  [NOMBRE] VARCHAR(100),
  [TELEFONO] VARCHAR(100),
  [CORREO] VARCHAR(100)
)
GO

CREATE TABLE [PRODUCTO] (
  [IDPRODUCTO] INT PRIMARY KEY,
  [NOMBRE] VARCHAR(100),
  [DESCRIPCION] VARCHAR(500),
  [STOCK] INT,
  [PRECIO] DECIMAL(18,2),
  [IDFAMILIA] INT
)
GO

CREATE TABLE [FAMILIA_PRODUCTO] (
  [IDFAMILIA] INT PRIMARY KEY,
  [NOMBRE] VARCHAR(100)
)
GO

CREATE TABLE [COTIZACION] (
  [IDCOTIZACION] INT PRIMARY KEY,
  [FECHA] DATETIME,
  [CANTIDAD] INT,
  [TOTAL] DECIMAL,
  [IDCLIENTE] INT
)
GO

CREATE TABLE [COTIZACION_DET] (
  [IDDETALLE] INT PRIMARY KEY,
  [IDCOTIZACION] INT,
  [IDPRODUCTO] INT,
  [CANTIDAD] INT,
  [PRECIO] DECIMAL,
  [TOTAL] DECIMAL
)
GO

ALTER TABLE [PRODUCTO] ADD FOREIGN KEY ([IDFAMILIA]) REFERENCES [FAMILIA_PRODUCTO] ([IDFAMILIA])
GO

ALTER TABLE [COTIZACION_DET] ADD FOREIGN KEY ([IDPRODUCTO]) REFERENCES [PRODUCTO] ([IDPRODUCTO])
GO

ALTER TABLE [COTIZACION_DET] ADD FOREIGN KEY ([IDCOTIZACION]) REFERENCES [COTIZACION] ([IDCOTIZACION])
GO

ALTER TABLE [COTIZACION] ADD FOREIGN KEY ([IDCLIENTE]) REFERENCES [CLIENTES] ([IDCLIENTE])
GO