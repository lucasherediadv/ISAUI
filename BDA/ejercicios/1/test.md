1) Calcular la cantidad de ventas que realizaron los clientes de la ciudad de Cordoba (CodCiudad igual a 1) durante el corriente mes.
```sql
SELECT
    COUNT(Venta.CodVenta) AS CantidadDeVentas
FROM
    Venta
    INNER JOIN Cliente ON Venta.CodCliente = Cliente.CodCliente
    INNER JOIN Barrio ON Cliente.CodBarrio = Barrio.CodBarrio
    INNER JOIN Ciudad ON Barrio.CodCiudad = Ciudad.CodCiudad
WHERE
    Ciudad.CodCiudad = 1
    AND MONTH(Venta.Fecha) = MONTH(GETDATE())
    AND YEAR(Venta.Fecha) = YEAR(GETDATE());
```

2) Crear la tabla Cliente.
```sql
CREATE TABLE Cliente (
    CodCliente INT PRIMARY KEY,
    Apellido VARCHAR(50),
    Nombre VARCHAR(50),
    NroDoc VARCHAR(20) UNIQUE,
    CodBarrio INT,
    FOREGIN KEY (CodBarrio) REFERENCES Barrio(CodBarrio);
);
```

3) Modificar la tabla vendedor y agregar dos columnas Telefono y Descripcion.
```sql
ALTER TABLE Vendedor
ADD Telefono VARCHAR(20),
    Descripcion VARCHAR(100);
```

4) Realizar un listado con los nombres de los barrios y las ciudades ordenados alfabéticamente.
```sql
SELECT
    Barrio.Nombre AS NombreBarrio,
    Ciudad.Nombre AS NombreCiudad
FROM
    Barrio
    INNER JOIN Ciudad ON Barrio.CodCiudad = Ciudad.CodCiudad
ORDER BY
    Barrio.Nombre ASC,
    Barrio.Nombre ASC;
```

5) Calcular el promedio de ventas que realizan los clientes del barrio Nueva Córdoba (CodBarrio igual a 3).
```sql
SELECT
    AVG(Venta.Total) AS PromedioVentas
FROM
    Venta
    INNER JOIN Cliente ON Venta.CodCliente = Cliente.CodCliente
WHERE
    Cliente.CodBarrio = 3
```

6) Realizar un listado con el Nombre y Apellido de los clientes, nombre de la ciudad y de barrio, ordenado alfabéticamente por nombre de ciudad, barrio y cliente.
```sql
SELECT
    Cliente.Nombre AS NombreCliente
    Cliente.Apellido AS ApellidoCliente
    Barrio.Nombre AS NombreBarrio
    Ciudad.Nombre AS NombreCiudad
FROM
    Cliente
    INNER JOIN Barrio ON Cliente.CodBarrio = Barrio.CodBarrio
    INNER JOIN Ciudad ON Barrio.CodCiudad = Ciudad.CodCiudad
ORDER BY
    NombreCiudad ASC,
    NombreBarrio ASC,
    ApellidoCliente ASC,
    NombreCliente ASC;
```

7) Listar el nombre del producto, marca , precio y stock de aquellos productos que tengan menos de 5 unidades en stock. Ordenado por marca y nombre del producto.
```sql
SELECT
    Producto.Nombre AS NombreProducto,
    Producto.Stock AS Stock,
    Marca.Nombre AS NombreMarca
FROM
   Producto
   INNER JOIN Marca ON Producto.CodMarca = Marca.CodMarca
WHERE
    Stock < 5
ORDER BY
    NombreMarca ASC,
    NombreProducto ASC;
```

8) Calcular la mayor y menor venta que realizó el vendedor con legajo igual a 100 durante el corriente año.
```sql
SELECT
    MAX(Venta.Total) AS MayorVenta,
    MIN(Venta.Total) AS MenorVenta,
FROM
    Venta
    INNER JOIN Vendedor ON Venta.CodVendedor = Vendedor.CodVendedor
WHERE
    Vendedor.Legajo = 100
    AND YEAR(Venta.Fecha) = YEAR(GETDATE());
    -- AND Venta.Fecha >= '01-01-2025
    -- AND Venta.Fecha <= '31-12-2025'
```

9) Borrar el cliente con número de documento igual a 40.222.333.
```sql
DELETE FROM
    Cliente
WHERE
    Cliente.NroDoc = '40.222.333'
```

10) Por error se agrego la columna celular en la tabla Cliente, realizar la sentencia sql que permita eliminar esa columna de la respectiva tabla.
```sql
ALTER TABLE
    Cliente
DROP COLUMN
    Celular;
```

11) Incrementar en un 20 por ciento los precios de los productos con código de marca igual a 1.
```sql
UPDATE
    Producto
    SET Producto.Precio = Producto.Precio * 1.20
WHERE CodMarca = 1;
```

12) Listar el Apellido, Nombre y legajo de los vendedores de la ciudad de Córdoba
ordenados por Apellido y Nombre.
```sql
SELECT
    Vendedor.Apellido
    Vendedor.Nombre
    Vendedor.Legajo
FROM
    Vendedor
    INNER JOIN Barrio ON Vendedor.CodBarrio = Barrio.CodBarrio
    INNER JOIN Ciudad ON Barrio.CodCiudad = Ciudad.CodCiudad
WHERE
    Ciudad.CodCiudad = 1
ORDER BY
    Vendedor.Apellido,
    Vendedor.Nombre,
    Vendedor.Legajo;
```

13) Listar el nombre del producto y el importe de venta que realizo el vendedor con legajo igual a 100 durante el día de hoy.
```sql
SELECT
    Producto.Nombre,
    DetalleVenta.Subtotal
FROM
    DetalleVenta
    INNER JOIN Producto ON DetalleVenta.CodProducto = Producto.CodProducto
    INNER JOIN Venta ON DetalleVenta.CodVenta = Venta.CodVenta
    INNER JOIN Vendedor ON Venta.CodVendedor = Vendedor.CodVendedor
WHERE
    Vendedor.Legajo = 100
    AND CONVERT(DATE, Venta.Fecha) = CONVERT(DATE, GETDATE());
```

14) Calcular el total de ventas que realizaron los clientes de la ciudad de Carlos Paz (Código igual a 2) de aquellos productos con código igual a 1 durante el mes actual.
```sql
SELECT
    SUM(DetalleVenta.Subtotal) AS TotalVentas
FROM
    DetalleVenta
    INNER JOIN Venta ON DetalleVenta.CodVenta = Venta.CodVenta
    INNER JOIN Cliente ON Venta.CodCliente = Cliente.CodCliente
WHERE
    Cliente.CodCiudad = 2
    AND DetalleVenta.CodProducto = 1
    AND MONTH(Venta.Fecha) = MONTH(GETDATE())
    AND YEAR(Venta.Fecha) = YEAR(GETDATE());
```
