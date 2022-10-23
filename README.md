# Project: Nx + dotnet 6 + MFE Angular + Docker

## Descripcion

Este proyecto es una prueba de concepto de como seria montar un entorno funciona con Nx que contenga 3 aplicaciones:

    - Un microservicio API dotnet Backend que solo puede ser atacada por red interna. Es el origen de datos.
    - Un microservicio API dotnet ApiGateway que tiene acceso tanto a la red publica como a la red interna. Hace de enrutador.
    - Un microservicio SPA Angular Frontend que se le tiene acceso a traves de la red publica. Es el cliente con el que el usuario interactuara.

Todos los microservicios tendran su Dockerfile para poder contenerizarlo.


## Requerimientos

 Los requerimientos minimos para poder utilizar este proyecto seran:

    - NodeJS
    - SDK dotnet 6
    - Docker Desktop o similares (Rancher por ejemplo)

 Adicionalmente se recomienda:

    - VSCode
    - Windows Terminal (de la store de microsoft, te permite tener varios terminales agrupados por pestañas)

## Crearlo tu de 0

 Si quieres crearlo de 0, los pasos que tendras que seguir son los siguientes:
    
    Paso 1: Configurar Nx:

        ```
        npm init nx-workspace docker-mfe --preset=empty --no-nx-cloud
        cd docker-mfe
        ```
    
    Paso 2: Configurar el repo Nx:

        ```
        npx nx g @nx-dotnet/core:init
        npm install --save-dev @nx-dotnet/core
        npm install --save-dev @nrwl/angular
        npm install --global json
        json -I -f nx.json -e "this.affected.defaultBase = 'main';"
        ```
    
    Paso 3: Generar proyectos:

        ```
        nx generate @nrwl/angular:app Frontend --routing
        nx g @nx-dotnet/core:app ApiGateway
        nx g @nx-dotnet/core:app Backend
        ```
    
    Paso 4: Configurar el package.json. Utiliza el mismo package.json que se esta utilizando en el repo para tener los mismos comandos npm.

## Uso del proyecto

 Una vez que tengas ya el repositorio, ya sea creado o clonado, podras utilizar de diferentes maneras.

 Lo primero que hay que hacer, es instalar las dependencias si no las tienes:

 ```
 npm i
 ```

 Una vez hecho esto, puedes hacerle build a los diferentes proyectos:

 ```
 npm run build:frontend
 npm run build:api-gateway
 npm run build:backend
 ```
 Puedes levantar los diferentes proyectos:

 ```
 npm run serve:frontend
 npm run serve:api-gateway
 npm run serve:backend
 ```

 Para compilar las imagenes con Docker:

 ```
 build -f apps/ApiGateway/Dockerfile .
 build -f apps/Backend/Dockerfile .
 ```

Para montar las imagenes desde los compilados, como se hace en la pipeline de Azure:

 ```
 build -f Dockerfile.Frontend .
 build -f Dockerfile.ApiGateway .
 build -f Dockerfile.Backend .
 ```

 Y puedes levantar con docker compose para simular el entorno completo"

 TODO: Aun no esta configurado docker compose

## Uso en Azure Pipeline

 TODO: Por documentar (de momento podeis mirar/utilizar el mismo yml que en el repo)