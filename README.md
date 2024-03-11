# Solución de Conexión de Viajes - API .NET

La solución de Conexión de Viajes es una aplicación Web API desarrollada en .NET que permite a la empresa "Viaje por Colombia" conectar viajes a través de Colombia.

## Características

- **Conexión de Viajes:** Permite a los usuarios especificar origen y destino para encontrar vuelos asociados.
  
- **Consulta de Vuelos Disponibles:** Consulta y muestra todos los vuelos disponibles entre el origen y destino especificados.
  
- **Cálculo de Ruta de Viaje:** Calcula la ruta de viaje basada en los vuelos disponibles, si es posible.
  
- **Persistencia de Rutas:** Guarda rutas calculadas en un sistema de persistencia para acceso futuro.
  
- **Recuperación de Rutas Previas:** Recupera rutas previamente calculadas si se ingresan los mismos parámetros de origen y destino.

## Información Relevante

Esta sección proporciona información adicional sobre la solución y detalles sobre cualquier tema que no haya sido implementado correctamente.

### Repositorio de Código

La solución está alojada en el siguiente repositorio de código:

[Enlace al Repositorio](https://github.com/JnAcero/APIViajeColombia)

### Configuración de la Solución

A continuación, se detallan algunos aspectos relevantes sobre la configuración de la solución:

- **Cadena de Conexión a la Base de Datos:** Se debe configurar la cadena de conexión a la base de datos en el archivo de configuración `appsettings.Development.json` para que la aplicación pueda conectarse correctamente a la base de datos.

- **Migraciones de la Base de Datos:** Antes de ejecutar la aplicación, asegúrese de haber aplicado todas las migraciones de la base de datos utilizando el comando `dotnet ef database update`.

### Uso de Entity Framework como ORM

En esta solución, se ha utilizado Entity Framework como nuestro Object-Relational Mapper (ORM) para interactuar con la base de datos.

### Enfoque Code First para la Creación de la Base de Datos
Se ha adoptado el enfoque Code First para la creación de la base de datos en nuestra aplicación. Con este enfoque, se nuestras clases de entidad en el código y luego Entity Framework genera automáticamente el esquema de la base de datos a partir de estas clases. Esto proporciona una forma intuitiva y orientada a objetos de diseñar nuestra base de datos, lo que facilita la comprensión y mantenimiento del código.

### Uso de Llaves Primarias y Llaves Foráneas con Data Annotations
Para establecer relaciones entre las tablas en nuestra base de datos, se ha utilizado llaves primarias y llaves foráneas mediante el uso de Data Annotations en nuestras clases de entidad.

### Temas No Implementados

En esta sección, se detallan los temas que no han sido implementados correctamente y posibles razones:

- **[Unit Test]**: Tanto el controlador como los servicios poseen varias dependencias por lo que no pude abarcar los test con tiempo.

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

5. **Compile el proyecto**

    ```bash
    dotnet build
    ```

6. **Configura la cadena de conexión a la base de datos en el archivo de configuración**

    Editar el archivo `appsettings.Development.json` dentro de la carpeta `/ViajeColombia.API`:

    ```json
    "ConnectionStrings": {
        "postgresCnx": "Server=myServerAddress;Database=myDatabase;Username=myUsername;Password=myPassword;"
    }
    ```

    Reemplazar los parámetros de la cadena de conexión (`Server`, `Database`, `Username`, `Password`) de PostgreSQL por los propios del servidor.

7. **Instalar herramientas de desarrollo de Entity Framework Core**

    ```bash
    dotnet tool install --global dotnet-ef
    ```

8. **Ejecutar las migraciones de la base de datos**

    Diríjase al directorio `/ViajeColombia.API` y desde allí ejecute el siguiente comando:

    ```bash
    dotnet ef database update
    ```

    Esto creará las tablas en la base de datos y estará listo para probar la solución.

9. **Ejecutar la aplicación**

    Ejecute el siguiente comando dentro del directorio `/ViajeColombia.API`:

    ```bash
    dotnet run
    ```

    La aplicación tiene configurado Swagger como interfaz para llamar a los endpoints del API. Ingrese a la URL donde está escuchando la aplicación y agregue `/swagger` a la ruta. Ejemplo:

    ```bash
    http://localhost:5141/swagger
    ```
## Documentación del Endpoint HTTP POST : Calculate

A través de un método HTTP POST, nuestra API expone un endpoint que calcula todos los viajes posibles desde un origen hacia un destino. Este endpoint es útil para determinar las diferentes rutas disponibles para viajar de un lugar a otro.

### Endpoint

El endpoint para calcular los viajes posee la siguiente estructura: '../api/Journey/Calculate


### Parámetros

El endpoint espera que se le envíen los siguientes parámetros en el cuerpo de la solicitud:

- `origin`: El código del origen del viaje (por ejemplo, "BGA").
- `destination`: El código del destino del viaje (por ejemplo, "MED").
- `numberOfFlights` (opcional): El número máximo de vuelos permitidos en la ruta. Si se incluye este parámetro, el endpoint calculará solo las rutas que contengan hasta ese número máximo de vuelos entre el origen y el destino. Si no se incluye este parámetro, se calcularán todas las rutas posibles entre los dos puntos sin restricción en el número de vuelos.


### Ejemplo del Cuerpo de la Solicitud

```json
{
  "origin": "BGA",
  "destination": "MED",
  "numberOfFlights": 2
}
```
Después de realizar una solicitud al endpoint para calcular los viajes posibles, la API responderá con una respuesta que contiene información sobre las rutas encontradas.

### Estructura de la Respuesta

La respuesta del endpoint tendrá la siguiente estructura:

```json
{
  "response": [
    {
      "journey": "BGA-MED",
      "origin": "BGA",
      "destination": "MED",
      "price": 1000,
      "flights": [
        {
          "origin": "BGA",
          "destination": "MED",
          "price": 1000,
          "transport": {
            "flightCarrier": "AV",
            "flightNumber": "8060"
          }
        }
      ]
    }
  ],
  "isSuccess": true,
  "message": null
}
```
## Manejo de Errores

Cuando ocurren errores durante el procesamiento de las solicitudes, la API devuelve una respuesta adecuada junto con un código de estado HTTP correspondiente. A continuación, se detallan los posibles errores que pueden ocurrir y cómo se manejan:

### Error 400 - Bad Request

#### Causa Común:

- Los parámetros de la solicitud son inválidos o están ausentes.
- El origen o destino especificados no existen en el sistema.
- En caso de incluir un maximo de vuelos por viaje, no se pueden calcular con ese cantidad.

#### Ejemplo de Respuesta:

```json
{
  "response": null,
  "isSuccess": false,
  "message": "Origen o destino inexistente"
}
```
```json
{
  "response": null,
  "isSuccess": false,
  "message": "La ruta que intenta calcular no es posible de realizar"
}
