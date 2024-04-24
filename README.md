# Laboratiorio: .Net - SQLServer

## Laboratorio 1 - Arquitectura de Software


### Desarrollado por:

- Andres Duarte
- Humberto Rueda
- Juan Aguiar


<details>
<summary>Descripción del Laboratorio</summary>

Este proyecto es el Laboratorio 1 de la asignatura Arquitectura de Software. Consiste en una API desarrollada en .NET 7.0 que permite la gestión de personas. La aplicación está diseñada para conectarse a una base de datos en SQL Server para el almacenamiento y recuperación de datos.

</details>

<details>
<summary>Conexión a Base de Datos</summary>

Para que la aplicación funcione correctamente, debe estar conectada a una base de datos en SQL Server. En los archivos de este repositiorio encontrará un archivo.txt con los datos de creación de tablas y unos datos de ejemplo. Asegúrese de actualizar el archivo de configuración `appsettings.json` con las credenciales y la dirección de su servidor SQL Server.

Ejemplo de cadena de conexión:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=persona_db;Trusted_Connection=True"
  }
```
</details>
<details>
<summary>Puerto</summary>
La aplicación está configurada para exponerse en el puerto 5160. Es necesario que el puerto esté disponible en el entorno de despliegue o que se actualice la configuración del puerto.

</details>
<details>
<summary>Instrucciones de Despliegue</summary>

1. Clone el repositorio o descargue el código fuente.
2. Abra la solución en su entorno de desarrollo.
3. Asegúrese de tener SQL Server ejecutándose y la cadena de conexión configurada correctamente.
4. Compile y ejecute la aplicación. Por defecto, estará accesible en: `http://localhost:5160`

</details>

