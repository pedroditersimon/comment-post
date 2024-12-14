# Endpoints

Guia de todos los endpoints disponibles.

## Comentarios
Respuesta 'comentario'
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
### Postear un comentario
```HTPP
POST /comments
Authorization: 'Bearer <tu_token>'
```
```JSON
{
    "PageId": "string",
    "Text": "string"
}
```
Respuesta: comentario

---
### Editar un comentario
Se requiere rol Moderador para hacer esta peticion.
```HTPP
PATCH /comments  
Authorization: 'Bearer <tu_token>'
```
```JSON
{
	"CommentId": 1,

	// Valores a actualizar (opcionales)
	"UserId": 1,
	"PageId": "my-wow-page",
	"Text": "comment text",
	"Visibility": true,
	"ReplyId": 1,
}
```
Cada campo incluido en el cuerpo de la solicitud será lo que se va modificar en el comentario. Incluir unicamente los campos que quieres actualizar.  
Tambien puedes dejar un valor 'null' y seran ignorados.

Respuesta: comentario

---
### Responder a un comentario
```HTPP
POST /comments/reply  
Authorization: 'Bearer <tu_token>'
```
```JSON
{
	"Text": "comment text",
	"ReplyId": 1
}
```
El ReplyId es el id del comentario que se quiere a responder.

---

GET   /comments/{comment-id}  

GET   /comments/page/{page-id}?limit={limit}&offset={offset}

GET   /comments/replies/{comment-id}?limit={limit}&offset={offset}

## PageId
El id de la pagina (PageId) es un simple identificador para agrupar comentarios.
No existe ni se almacena una entidad 'pagina'.

## Autenticación
El servidor devuelve un jwt con el id interno del usuario y su rol. En cada petición se debe enviar este token y el servidor lo va a verificar.  
Incluir el token en el Header 'Authorization' con el siguiente formato:
```HTTP
Authorization: 'Bearer <tu_token>'
```

### Metodos
A continuacion, los metodos para autenticarse y obtener el token.

### Metodo 1: Local
---
### Login
```HTPP
POST /login
```
```JSON
{
    "Username": "<username>",
    "Password": "<password>"
}
```

### Register
```HTPP
POST /register
```
```JSON
{
    "Username": "<username>",
    "Password": "<password>"
}
```

### Metodo 2: Auth0
---
### Login
```HTPP
POST /auth0/login  
```
```JSON
{
    "AuthenticationCode": "<code>",
}
```

'authenticationCode' se extrae luego de que el usuario inicie sesion en Auth0 y devuelva un codigo en el callback.

---

### Respuestas
Todas las respuestas de cualquiera de los metodos de autenticacion, devuelven un token.
```JSON
{
    "Token": "<tu_token>"
}
```
Este es el token que necesitas incluir en el header de tus peticiones.
