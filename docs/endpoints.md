# Endpoints

Esta guía detalla los endpoints disponibles, su propósito y ejemplos de uso. Estructurada de manera sencilla para facilitar la comprensión y rápida referencia.


## Autenticación
El servidor devuelve un `JWT` (JSON Web Token) con el ID interno del usuario y su rol. En cada petición, debes enviar este token en el Header `Authorization`, y el servidor lo verificará.

Incluir el token en el header de la siguiente manera:
```HTTP
Authorization: 'Bearer <tu_token>'
```

A continuación, se describen los métodos para autenticarse y obtener el token.


### Metodo 1: Local
Este método permite a los usuarios autenticarse utilizando su nombre de usuario y contraseña locales.

---
### Login

#### Ruta 
```HTPP
POST /login
```

#### Body - Cuerpo de la Solicitud
```JSON
{
    "Username": "<username>",
    "Password": "<password>"
}
```


---
### Register

#### Ruta 
```HTPP
POST /register
```

#### Body - Cuerpo de la Solicitud
```JSON
{
    "Username": "<username>",
    "Password": "<password>"
}
```


### Metodo 2: Auth0

---
### Login

#### Ruta 
```HTPP
POST /auth0/login  
```

#### Body - Cuerpo de la Solicitud
```JSON
{
    "AuthenticationCode": "<code>",
}
```

`AuthenticationCode`: El código de autenticación que se obtiene después de que el usuario inicie sesión en Auth0 y el sistema devuelva un código en el callback de autenticación.


---
### Respuestas
Todas las respuestas de los métodos de autenticación devuelven un token JWT, que se debe incluir en el header de las solicitudes para acceder a los recursos protegidos.

Ejemplo de respuesta
```JSON
{
    "Token": "<tu_token>"
}
```
`Token`: Este es el token que necesitas incluir en el header de tus peticiones para autenticarte.


# Endpoints de Comentarios

### Respuesta típica de un comentario
#### Campos
- `ID`: El identificador único del comentario.
- `CreationDate`: Fecha y hora de creación del comentario.
- `LastUpdatedDate`: Fecha de la última actualización del comentario (si aplica).
- `UserId`: El identificador del usuario que publicó el comentario.
- `PageId`: El identificador de la página donde se publicó el comentario.
- `Text`: El texto del comentario.
- `Visibility`: Indica si el comentario es visible.
- `ReplyId`: Si el comentario es una respuesta a otro comentario, este campo tendrá el ID del comentario original. Si no es una respuesta, será null.

> [!Note]  
> El id de la pagina (PageId) es un simple identificador unico para agrupar comentarios.
No existe ni se almacena una entidad 'pagina'.

Ejemplo de una respuesta típica de un comentario

```JSON
{
  "ID": 0,
  "CreationDate": "2024-12-13T00:00:00",
  "LastUpdatedDate": "2024-12-13T00:00:00",
  "UserId": 0,
  "PageId": "string",
  "Text": "string",
  "Visibility": true,
  "ReplyId": 0 // puede ser: null
}
```

---
### Respuesta típica de comentarios con paginacion

Cuando se solicita una lista de comentarios, la respuesta esta paginada. Donde se incluyen detalles sobre la cantidad de elementos, el desplazamiento y el límite de comentarios devueltos.

#### Campos
- `Elements`: Es un array que contiene los comentarios obtenidos en la página actual. Cada elemento en el arreglo es un objeto JSON que representa un comentario.
- `Count`: El número total de elementos.
- `Offset`: Desplazamiento - Número de comentarios omitidos.
- `Limit`: El número máximo de comentarios devueltos en la solicitud. Si el valor es 0, significa que no hay límite.


Ejemplo de una respuesta típica de comentarios con paginacion

```JSON
{
  "Elements": [
    {
      "ID": 1,
      "CreationDate": "2024-12-13T10:00:00",
      "LastUpdatedDate": "2024-12-13T10:00:00",
      "UserId": 456,
      "PageId": "home-page",
      "Text": "Comentario de ejemplo 1.",
      "Visibility": true,
      "ReplyId": null
    },
    // Otros comentarios...
  ],
  "Count": 5,
  "Offset": 0,
  "Limit": 10 // limite 0 es no limite
}
```

---
### 1. `POST` Postear un comentario

Rol minimo requerido: `User` (Usuario).

#### Ruta y Headers
```HTPP
POST /comments
Authorization: 'Bearer <tu_token>'
```

#### Body - Cuerpo de la Solicitud
- `PageId`: Identificador único de la página donde se publicará el comentario.
- `Text`: Contenido del comentario.

Ejemplo de cuerpo (Body) en formato JSON:
```JSON
{
  "PageId": "string",
  "Text": "string"
}
```

#### Respuesta
> Respuesta típica de un comentario


---
### 2. `PATCH` Editar un comentario

Rol minimo requerido: `Moderator` (Moderador).
> [!Important]  
> Requiere el rol de Moderador para realizar esta operación.

#### Ruta y Headers
```HTPP
PATCH /comments
Authorization: 'Bearer <tu_token>'
```

#### Body - Cuerpo de la Solicitud
- `CommentId`: Identificador del comentario que se desea editar.
- `Campos opcionales`: Incluye solo los campos que deseas actualizar. 

> [!NOTE]  
> Los campos enviados con null serán ignorados durante la actualización.  

Ejemplo de cuerpo (Body) en formato JSON:
```JSON
{
	"CommentId": 1,

	// Valores a actualizar (opcionales)
	"UserId": 1,
	"PageId": "my-wow-page",
	"Text": "comment text",
	"Visibility": null, // Ignorado si se deja en null
	"ReplyId": 1,
}
```

#### Respuesta
> Respuesta típica de un comentario


---
### 3. `POST` Responder a un comentario

Rol minimo requerido: `User` (Usuario).

#### Ruta y Headers
```HTPP
POST /comments/reply  
Authorization: 'Bearer <tu_token>'
```

#### Body - Cuerpo de la Solicitud
- `Text`: El texto del comentario.
- `ReplyId`: ID del comentario que se quiere a responder. 

Ejemplo de cuerpo (Body) en formato JSON:
```JSON
{
	"Text": "comment text",
	"ReplyId": 1
}
```

#### Respuesta
> Respuesta típica de un comentario


---
### 4. `GET` Obtener detalles de un comentario

Rol minimo requerido: `Ninguno`.
> [!NOTE]  
> Este endpoint no requiere autenticación.

#### Ruta y Headers
```HTPP
GET /comments/{comment-id}

GET /comments/1  
```

#### Parametros de la solicutd
- `comment-id`: Identificador del comentario que se desea obtener.


#### Respuesta
> Respuesta típica de un comentario


---
### 5. `GET` Obtener detalles de los comentarios de una pagina

Rol minimo requerido: `Ninguno`.
> [!NOTE]  
> Este endpoint no requiere autenticación.

#### Ruta y Headers
```HTPP
GET /comments/page/{page-id}?limit={limit}&offset={offset}

GET /comments/page/my-wow-page?limit=10&offset=5
```

#### Parametros de la solicutd
- `page-id`: Identificador único de la página cuyos comentarios deseas obtener.
- `limit`: Número máximo de comentarios a devolver.
- `offset`: Número de comentarios omitidos.

#### Respuesta
> Respuesta típica de comentarios con paginacion


---
### 6. `GET` Obtener detalles de las respuestas (Replies) de un comentario

Rol minimo requerido: `Ninguno`.
> [!NOTE]  
> Este endpoint no requiere autenticación.

#### Ruta y Headers
```HTPP
GET /comments/replies/{comment-id}?limit={limit}&offset={offset}

GET /comments/replies/3?limit=0&offset=10
```

#### Parametros de la solicutd
- `comment-id`: Identificador único del comentario para el cual deseas obtener las respuestas.
- `limit`: Número máximo de comentarios a devolver.
- `offset`: Número de comentarios omitidos.

#### Respuesta
> Respuesta típica de comentarios con paginacion

