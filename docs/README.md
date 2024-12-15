# CommentPost API

API backend que proporciona funcionalidades para gestionar los comentarios de una página o blog.


## Arquitectura

Se aplica un mix de la Arquitectura hexagonal (puerto/adaptador) y Clean architecture para una separación clara de responsabilidades.  
Desarrollada en C# con ASP.NET
La aplicación está desarrollada en ASP.NET C# utilizando EntityFramework,


## Patrones y funcionalidades implementados

Generales:
- Paginación
- Mappers
- Uso de excepciones
- Autenticacion por JWT y hashing de contraseñas
- Test Unitarios

API:
- Metodos de extensión
- Middleware
- Attributes
- Rutas protegidas por roles

Capa aplicacion y dominio:
- Patrón repositorio
- Patrón UnitOfWork
- Casos de uso con comando y handler
- Servicios

Infraestructura:
- EntityFramework
- Migraciones de EntityFramework
- PostgreSQL


## Autenticación

El servidor utiliza JWT (JSON Web Tokens) para autenticar las solicitudes. Cada vez que se realiza una petición a la API, se debe incluir un token Bearer en el header de la solicitud. Este token contiene el ID interno del usuario y su rol, y el servidor lo valida en cada petición.  
Puedes consultar el proceso para [autenticarse y consumir la API](./endpoints.md#Autenticación) en la guía de endpoints.


## Endpoints

La guía de endpoints proporciona toda la información sobre cómo autenticarte y consumir la API.
[Ver endpoints](./endpoints.md)


## Diagrama

Puedes ver un diagrama de todo el proceso de diseño del proyecto.
[Ver diagrama](./CommentPostDiagram.png)