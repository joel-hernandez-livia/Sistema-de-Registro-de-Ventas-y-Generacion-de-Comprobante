create database Registro_Ventas_Comprobante

use Registro_Ventas_Comprobante



CREATE TABLE Productos (
    IdProductos INT PRIMARY KEY IDENTITY,
	CodigoProducto varchar(50),
    Nombre NVARCHAR(100),
    Precio DECIMAL(10,2),
    Stock INT
)

CREATE TABLE Ventas (
    IdVentas INT PRIMARY KEY IDENTITY,
    Fecha DATETIME,
    Total DECIMAL(10,2),
	NombreCliente varchar(50),
	documentoCliente varchar(20)
)

CREATE TABLE DetalleVenta (
    IdDetalleVenta INT PRIMARY KEY IDENTITY,
    VentaId INT FOREIGN KEY REFERENCES Ventas(IdVentas),
    ProductoId INT FOREIGN KEY REFERENCES Productos(IdProductos),
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
	NombreProducto NVARCHAR(100)
)
