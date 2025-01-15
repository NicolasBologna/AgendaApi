# AgendaApi

AgendaApi es una API RESTful diseñada para la gestión de contactos y usuarios, con funcionalidades de autenticación y exportación de datos.

## **Tabla de Contenidos**

- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Autenticación](#autenticación)
- [Endpoints Principales](#endpoints-principales)
  - [Authentication](#authentication)
  - [Contact](#contact)
  - [User](#user)
- [Esquemas de Datos](#esquemas-de-datos)
- [Seguridad](#seguridad)

---

## **Requisitos Previos**

- .NET Core 6.0 o superior

## **Instalación**

1. Clonar este repositorio.
2. Configurar la conexión a la base de datos en el archivo `appsettings.json`. (puede dejarse la por default que es SqLite)
3. Ejecutar desde Visual Studio

## **Autenticación**

Esta API utiliza autenticación basada en tokens **Bearer**.

- Antes de acceder a los endpoints protegidos, debés autenticarte para obtener un token.

---

## **Endpoints Principales**

### **Authentication**

| Método | Endpoint                           | Descripción                               |
| ------ | ---------------------------------- | ----------------------------------------- |
| POST   | `/api/authentication/authenticate` | Autentica a un usuario y genera un token. |

#### Ejemplo de cuerpo de solicitud:

```json
{
  "userName": "admin",
  "password": "password123"
}
```

---

### **Contact**

| Método | Endpoint                            | Descripción                           |
| ------ | ----------------------------------- | ------------------------------------- |
| GET    | `/api/Contact`                      | Obtiene todos los contactos.          |
| POST   | `/api/Contact`                      | Crea un nuevo contacto.               |
| GET    | `/api/Contact/{contactId}`          | Obtiene un contacto específico.       |
| PUT    | `/api/Contact/{contactId}`          | Actualiza un contacto existente.      |
| DELETE | `/api/Contact/{contactId}`          | Elimina un contacto.                  |
| GET    | `/api/Contact/export`               | Exporta los contactos en formato CSV. |
| POST   | `/api/Contact/{contactId}/favorite` | Marca un contacto como favorito.      |

#### Ejemplo de solicitud para crear un contacto:

```json
{
  "firstName": "John",
  "lastName": "Doe",
  "address": "123 Main St",
  "email": "john.doe@example.com",
  "number": "1234567890",
  "company": "TechCorp"
}
```

---

### **User**

| Método | Endpoint             | Descripción                                  |
| ------ | -------------------- | -------------------------------------------- |
| GET    | `/api/User`          | Obtiene todos los usuarios.                  |
| POST   | `/api/User`          | Crea un nuevo usuario.                       |
| DELETE | `/api/User?id={id}`  | Elimina un usuario.                          |
| GET    | `/api/User/{id}`     | Obtiene un usuario por ID.                   |
| PUT    | `/api/User/{userId}` | Actualiza un usuario existente.              |
| GET    | `/api/User/me`       | Obtiene información del usuario autenticado. |

#### Ejemplo de solicitud para crear un usuario:

```json
{
  "firstName": "Alice",
  "lastName": "Smith",
  "userName": "alice123",
  "password": "password123"
}
```

---

## **Esquemas de Datos**

### **AuthenticationRequestDto**

```json
{
  "userName": "string",
  "password": "string"
}
```

### **CreateAndUpdateContact**

```json
{
  "firstName": "string",
  "lastName": "string",
  "address": "string",
  "email": "string",
  "image": "string",
  "number": "string",
  "company": "string"
}
```

### **CreateAndUpdateUserDto**

```json
{
  "firstName": "string",
  "lastName": "string",
  "userName": "string",
  "password": "string"
}
```

### **UserDto**

```json
{
  "id": 1,
  "firstName": "string",
  "lastName": "string",
  "userName": "string",
  "state": 0,
  "role": 0
}
```

---

## **Seguridad**

Esta API utiliza autenticación basada en tokens **Bearer**.
Para autenticarte:

1. Envía las credenciales al endpoint `/api/authentication/authenticate`.
2. Usa el token recibido en el encabezado `Authorization` para las solicitudes protegidas:

   ```
   Authorization: Bearer <token>

   ```
