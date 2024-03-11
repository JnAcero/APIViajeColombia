# Solución de Conexión de Viajes - API .NET

La solución de Conexión de Viajes es una aplicación Web API desarrollada en .NET que permite a la empresa "Viaje por Colombia" conectar viajes a través de Colombia.

## Características

- **Conexión de Viajes:** Permite a los usuarios especificar origen y destino para encontrar vuelos asociados.
  
- **Consulta de Vuelos Disponibles:** Consulta y muestra todos los vuelos disponibles entre el origen y destino especificados.
  
- **Cálculo de Ruta de Viaje:** Calcula la ruta de viaje basada en los vuelos disponibles, si es posible.
  
- **Persistencia de Rutas:** Guarda rutas calculadas en un sistema de persistencia para acceso futuro.
  
- **Recuperación de Rutas Previas:** Recupera rutas previamente calculadas si se ingresan los mismos parámetros de origen y destino.

## Ejecutar Localmente

1. **Clone el repositorio**

    ```bash
    git clone https://github.com/JnAcero/APIViajeColombia.git
    ```

2. **Diríjase al directorio del proyecto**

    ```bash
    cd APIViajeColombia
    ```

3. **Abre el proyecto en tu IDE preferido (Visual Studio o Visual Studio Code).**

4. **Restaure las dependencias**

    ```bash
    dotnet restore
    ```

5. **Ejecutar las migraciones para crear la base de datos**

    ```bash
    dotnet ef database update
    ```

6. **Compile el proyecto**

    ```bash
    dotnet build
    ```

7. **Configura la cadena de conexión a la base de datos en el archivo de configuración**

    Editar el archivo `appsettings.Development.json` dentro de la carpeta `/ViajeColombia.API`:

    ```json
    "ConnectionStrings": {
        "postgresCnx": "Server=myServerAddress;Database=myDatabase;Username=myUsername;Password=myPassword;"
    }
    ```

    Reemplazar los parámetros de la cadena de conexión (`Server`, `Database`, `Username`, `Password`) de PostgreSQL por los propios del servidor.

8. **Instalar herramientas de desarrollo de Entity Framework Core**

    ```bash
    dotnet tool install --global dotnet-ef
    ```

9. **Ejecutar las migraciones de la base de datos**

    Diríjase al directorio `/ViajeColombia.API` y desde allí ejecute el siguiente comando:

    ```bash
    dotnet ef database update
    ```

    Esto creará las tablas en la base de datos y estará listo para probar la solución.

10. **Ejecutar la aplicación**

    Ejecute el siguiente comando dentro del directorio `/ViajeColombia.API`:

    ```bash
    dotnet run
    ```

    La aplicación tiene configurado Swagger como interfaz para llamar a los endpoints del API. Ingrese a la URL donde está escuchando la aplicación y agregue `/swagger` a la ruta. Ejemplo:

    ```bash
    http://localhost:5141/swagger
    ```
