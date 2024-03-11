
# Solución de Conexión de Viajes - API .NET
La solución de Conexión de Viajes es una aplicación Wep Api desarrollada en .NET que permite a la empresa "Viaje por Colombia" conectar viajes a través del Colombia.

Conexión de Viajes: La aplicación permite a los usuarios especificar un origen y destino de viaje, y consulta los vuelos asociados disponibles para establecer una ruta.

Persistencia de Rutas: Las rutas calculadas son guardadas en un sistema de persistencia para su posterior recuperación. Esto permite que si se ingresan los mismos parámetros de origen y destino en el futuro, la ruta se pueda obtener fácilmente sin volver a calcularse.




## Caracteristicas

- Conexión de Viajes: Permite a los usuarios especificar origen y destino para encontrar vuelos asociados.


- Consulta de Vuelos Disponibles: Consulta y muestra todos los vuelos disponibles entre el origen y destino especificados.



- Cálculo de Ruta de Viaje: Calcula la ruta de viaje basada en los vuelos disponibles, si es posible.



- Persistencia de Rutas: Guarda rutas calculadas en un sistema de persistencia para acceso futuro.


- Recuperación de Rutas Previas: Recupera rutas previamente calculadas si se ingresan los mismos parámetros de origen y destino.




## Ejecutar Localmente

Clone el repositorio


```bash
  git clone https://github.com/JnAcero/APIViajeColombia.git
```

Dirijase al directorio del proyecto

```bash
  cd APIViajeColombia
```

Abre el proyecto en tu IDE preferido (Visual Studio o Visual Studio Code).

Restaure las dependencias

```bash
  dotnet restore
```

Compile el proyecto

```bash
  dotnet build
```

Configura la cadena de conexión a la base de datos en el archivo de configuración (appsettings.Development.json dentro de la carpeta /ViajeColombia.API).

```bash
"ConnectionStrings": {
    "postgresCnx": "Server=myServerAddress;Database=myDatabase;Username=myUsername;Password=myPassword;"
}
```
Copie y pegue la cadena de conexíon.
Por facilidad deje el mismo nombre de la clave "postgresCnx".

Reemlpaza los parametros de la cadena de conexión(Server, Database, Username, Password) PostgreSQL por los propios del servidor.
Es nescesario que tenga instalado PostgreSql para la ejecución del proyecto.

Guarda los cambios en el archivo appsettings.json.

Ejecuta el siguiente comando dentro del directorio ./ViajeColombia.API

```bash
  dotnet run
```
