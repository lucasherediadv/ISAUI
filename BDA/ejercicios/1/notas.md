DDL (Lenguaje de Definicion de Datos): Se utiliza para definir la estructura de la base de datos y sus objetos.
```sql
-- Para crear una nueva base de datos
CREATE DATABASE <NombreBaseDeDatos>

-- Para seleccionar una base de datos y comenzar a trabajar con ella
USE <NombreBaseDeDatos>

-- Para crear una nueva tabla, definiendo sus columnas, tipos de datos y restricciones
CREATE TABLE <NombreTabla> (
    <NombreColumna1> INT PRIMARY KEY,
    <NombreColumna2> VARCHAR(50),
    <NombreColumna3> VARCHAR(20) UNIQUE,
    FOREIGN KEY (<NombreColumna) REFERENCES <NombreTabla>(<NombreColumna>);
);

-- Para modificar una tabla existente
ALTER TABLE <NombreTabla>
ADD <NombreColumna1> INT,
    <Nombrecolumna2> INT,
    <Nombrecolumna3> VARCHAR(100);
```

DML (Lenguaje de Manipulacion de Datos): Se utiliza para manipular los datos dentro de las tablas.
```sql
-- Para agregar nuevos registros a una tabla.
INSERT INTO <NombreTabla> (<NombreColumna1>, <Nombrecolumna2>) VALUES ('<ValorColumna1>', '<ValorColumna2>');

-- Para consultar y recuperar datos.
SELECT <Columna1> <Columna2> ...

-- Para modificar los datos de los registros existentes.
UPDATE <NombreTabla>
SET <NombreColumna> = <NombreColumna> * 1.20

-- Para eliminar registros de una tabla.
DELETE FROM <NombreTabla>
WHERE <NombreColumna> = <Condicion>
```
