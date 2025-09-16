1)
```sql
SELECT COUNT(Venta.CodVenta) AS CantidadVentas
FROM Venta
INNER JOIN Cliente ON Venta.CodCliente = Cliente.CodCliente
WHERE Cliente.CodCiudad = 1
AND MONTH(Venta.Fecha) = MONTH(GETDATE())
AND YEAR(Venta.Fecha) = YEAR(GETDATE());
```

2)
```sql
CREATE TABLE Cliente (
    CodCliente INT PRIMARY KEY,
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    NroDoc VARCHAR(20) UNIQUE,
    CodBarrio INT,
    CodCiudad INT,
    FOREIGN KEY (CodCiudad) REFERENCES Ciudad(CodCiudad),
    FOREIGN KEY (CodBarrio) REFERENCES Barrio(CodBarrio);
);
```

3)
```sql
ALTER TABLE Vendedor
ADD Telefono VARCHAR(20),
    Descripcion VARCHAR(100);
```

4)
```sql
SELECT Barrio.Nombre AS NombreBarrio, Ciudad.Nombre AS NombreCiudad
FROM Barrio
INNER JOIN Ciudad ON Barrio.CodCiudad = Ciudad.CodCiudad
ORDER BY NombreBarrio, NombreCiudad;
```

5)
```sql
SELECT AVG(Venta.Total) AS PromedioVentas
FROM Venta
INNER JOIN Cliente ON Venta.CodCliente = Cliente.CodCliente
WHERE Cliente.CodBarrio = 3;
```

6)
```sql
SELECT
    Cliente.Nombre AS NombreCliente,
    Cliente.Apellido AS ApellidoCliente,
    Ciudad.Nombre AS NombreCiudad,
    Barrio.Nombre AS NombreBarrio
FROM
    Cliente
INNER JOIN Barrio ON Cliente.CodBarrio = Barrio.CodBarrio
INNER JOIN Ciudad ON Cliente.CodCiudad = Ciudad.CodCiudad
ORDER BY
    NombreCiudad,
    NombreBarrio,
    ApellidoCliente,
    NombreCliente;
```

7)
```sql
SELECT
    Producto.Nombre AS NombreProducto,
    Marca.Nombre AS Marca,
    Producto.Precio,
    Producto.Stock
FROM
    Producto
INNER JOIN Marca ON Producto.CodMarca = Marca.CodMarca
WkHERE
    Producto.Stock < 5
ORDER BY
    Marca.Nombre ASC,
    Producto.Nombre ASC;
```

8)
```sql
SELECT
    MAX(Venta.Total) AS MayorVenta,
    MIN(Venta.Total) AS MenorVenta
FROM
    Venta
INNER JOIN Vendedor ON Venta.CodVendedor = Vendedor.CodVendedor
WHERE
    Venta.CodVendedor = 100
    AND YEAR(Venta.Fecha) = YEAR(GETDATE());
```

9)
```sql
DELETE FROM Cliente
WHERE NroDoc = '40.222.333';
```

10)
```sql
ALTER TABLE Cliente
DROP COLUMN Celular;
```

11)
```sql
UPDATE Producto
SET Precio = Precio * 1.20
WHERE CodMarca = 1;
```

12)
```sql
SELECT
    Vendedor.Apellido,
    Vendedor.Nombre,
    Vendedor.Legajo
FROM
    Vendedor
INNER JOIN Barrio ON Vendedor.CodBarrio = Barrio.CodBarrio
INNER JOIN Ciudad ON Barrio.CodCiudad = Ciudad.CodCiudad
WHERE
   Ciudad.Nombre = 'Cordoba'
ORDER BY
    Vendedor.Apellido,
    Vendedor.Nombre;
```

13)
```sql
SELECT
    Producto.Nombre AS NombreProducto,
    DetalleVenta.Subtotal AS ImporteVenta
FROM
    DetalleVenta
INNER JOIN Producto ON DetalleVenta.CodProducto = Producto.CodProducto
INNER JOIN Venta ON DetalleVenta.CodVenta = Venta.CodVenta
INNER JOIN Vendedor ON Venta.CodVendedor = Vendedor.CodVendedor
WHERE
    Vendedor.Legajo = 100
    AND CONVERT(DATE, Venta.Fecha) = CONVERT(DATE, GETDATE());
```

14)
```sql
SELECT SUM(DetalleVenta.Subtotal) AS TotalVentas
FROM
    DetalleVenta
INNER JOIN Venta ON DetalleVenta.CodVenta = Venta.CodVenta
INNER JOIN Cliente ON Venta.CodCliente = Cliente.CodCliente
WHERE
    Cliente.CodCiudad = 2
    AND DetalleVenta.CodProducto = 1
    AND MONTH(Venta.Fecha) = MONTH(GETDATE())
    AND Year(Venta.Fecha) = YEAR(GETDATE());
```

15)
```sql
SELECT COUNT(Venta.CodVenta) AS CantidadVentas
FROM
    Venta
INNER JOIN Cliente ON Venta.CodCliente = Cliente.CodCliente
INNER JOIN DetalleVenta ON Venta.CodVenta = DetalleVenta.CodVenta
WHERE
    Cliente.CodCiudad = 3
    AND DetalleVenta.CodProducto = 1
    AND MONTH(Venta.Fecha) = MONTH(GETDATE())
    AND YEAR(Venta.Fecha) = YEAR(GETDATE());
```

16)
```sql
SELECT SUM(Venta.Total) * 0.30 AS Comision
FROM Venta
INNER JOIN Vendedor ON Venta.CodVendedor = Vendedor.CodVendedor
WHERE Vendedor.Legajo = 1;
```

17)
```sql
SELECT AVG(Venta.Total) AS PromedioVentas
FROM Venta
INNER JOIN Vendedor ON Venta.CodVendedor = Vendedor.CodVendedor
INNER JOIN Barrio ON Vendedor.CodBarrio = Barrio.CodBarrio
INNER JOIN Ciudad ON Barrio.CodCiudad = Ciudad.CodCiudad
WHERE
    Ciudad.CodCiudad = 1
    AND MONTH(Venta.Fecha) = MONTH(GETDATE())
    AND YEAR(Venta.Fecha) = YEAR(GETDATE());
```

18)
```sql
SELECT SUM(DetalleVenta.Subtotal) AS TotalVentas
FROM DetalleVenta
INNER JOIN Producto ON DetalleVenta.CodProducto = Producto.CodProducto
INNER JOIN Venta ON DetalleVenta.CodVenta = Venta.CodVenta
WHERE
    Producto.CodMarca = 1
    AND YEAR(Venta.Fecha) = YEAR(GETDATE());
```

19)
```sql
-- Crear la tabla Marca
CREATE TABLE Marca (
    CodMarca INT PRIMARY KEY,
    Nombre VARCHAR(50)
);

-- Modificar la tabla Marca y agregar la columna Descripcion
ALTER TABLE Marca
ADD Descripcion VARCHAR(150);
```

20)
```sql
SELECT
    Producto.Nombre AS NombreProducto,
    Producto.Precio,
    Producto.Stock,
    Marca.Nombre AS NombreMarca
FROM
    Producto
INNER JOIN Marca ON Producto.CodMarca = Marca.CodMarca
ORDER BY
    Marca.Nombre,
    Producto.Nombre;
```
