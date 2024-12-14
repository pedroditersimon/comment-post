# CommentPost API

API backend desarrollada en ASP.Net C# utilizando EntityFramework, esta api brinda funcionalidad para operar la seccion de comentarios de una pagina o blog.

## Arquitectura
Se aplica un mix de la Arquitectura hexagonal (puerto/adaptador) y Clean architecture.
Desarrollada en C# con ASP.NET

## Patrones y  implementados
- Paginacion
- Mappers
- Uso de excepciones
- Authenticacion por JWT y hashing de contraseñas
- Test Unitarios

API:
- Metodos de extension
- Middleware
- Attributes
- Rutas protegidas por roles

Capa aplicacion y dominio:
- Patron repositorio
- Patron UnitOfWork
- Casos de uso con comando y handler
- Servicios

Infraestructura:
- EntityFramework
- Migraciones de EntityFramework
- PostgreSQL


## Autenticación
El servidor devuelve un jwt con el id interno del usuario y su rol. En cada petición se debe enviar este token y el servidor lo va a verificar.  