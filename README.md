# ExamenTecnicoFlavio

## Versión: 1.0.0

- __Author:__ Flavio Urdiales
- __Fecha:__ 27/10/2026
- __Descripción:__
    - feat: se implementa dashboard con los 5 productos más vendidos, mostrando cantidad y monto total.
    - feat: se agrega consulta de órdenes por producto con paginación.
    - feat: se implementa visualización del detalle de venta.
    - feat: se permite editar y guardar CarrierTrackingNumber en SQL Server.

--------

## Configuración técnica
- __IDE:__ Visual Studio
- __Lenguaje:__ C# 14 / ASP.NET Core MVC .NET 10
- __Base de datos:__ SQL Server - AdventureWorks2016
- __ORM:__ Entity Framework Core 10

## Librerías
### Internas

### Externas
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

--------

## Documentación
1. Restaurar `AdventureWorks2016.bak` en SQL Server.
2. Configurar la conexión `AdventureWorks` en `appsettings.json`.
3. Abrir `ExamenTecnicoFlavio.slnx` en Visual Studio.
4. Restaurar paquetes NuGet, compilar y ejecutar con `F5`.

**Nota:** No se modifica la estructura original de AdventureWorks2016 ni se requieren scripts SQL adicionales.