# 🛒 E-commerce Backend – .NET (Clean Architecture)

Backend de un **e-commerce** desarrollado en **.NET**, aplicando **Clean Architecture**, principios **SOLID** y separación estricta de responsabilidades.  
Este proyecto surge como migración y re–diseño de un backend originalmente implementado en **Spring Boot (Java)** hacia el ecosistema **.NET**, manteniendo la lógica de negocio pero mejorando el desacoplamiento y la claridad arquitectónica.

---

##  Deploy 

🔗 Back en Render: https://ecommerce-backend-net.onrender.com

🔗 Frontend conectado a esta API: https://fedtasso-e-commerce-react-net.netlify.app/


---

## 🧱 Arquitectura

El proyecto está organizado siguiendo **Clean Architecture**, separando responsabilidades en capas bien definidas y evitando dependencias hacia frameworks desde el dominio.

---

## 📦 Capas

### Domain
- Entidades del negocio (`User`, `Product`, `Cart`, etc.)
- Enums y reglas centrales
- No depende de ninguna otra capa

### Application
- Casos de uso y lógica de negocio
- Servicios de dominio
- Interfaces (contratos) de repositorios y servicios externos
- DTOs, mappers y excepciones de negocio

### Infrastructure
- Implementaciones concretas de repositorios
- Acceso a datos y servicios externos
- Detalles técnicos (persistencia, hashing, etc.)

### API
- Controllers HTTP
- Middlewares y manejo de errores
- Configuración de la aplicación
- Punto de entrada (`Program.cs`)

---

## 📁 Estructura del proyecto

```text
.
├── Ecommerce.Api
│   ├── Controllers
│   ├── Exceptions
│   ├── Middlewares
│   ├── Properties
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Program.cs
│   └── Ecommerce.Api.csproj
│
├── Ecommerce.Application
│   ├── DTOs
│   ├── Exceptions
│   ├── Interfaces
│   ├── Mappers
│   ├── Services
│   └── Ecommerce.Application.csproj
│
├── Ecommerce.Domain
│   ├── Entities
│   ├── Enums
│   └── Ecommerce.Domain.csproj
│
├── Ecommerce.Infrastructure
│   └── Ecommerce.Infrastructure.csproj
│
├── Ecommerce.sln
├── Dockerfile
├── .gitignore
└── .dockerignore


## ⚠️ Manejo de errores

El proyecto implementa un **middleware global de excepciones**, encargado de:

- Interceptar excepciones de negocio  
- Traducirlas a respuestas HTTP coherentes  
- Evitar fugas de detalles internos  

Esto permite mantener los controllers simples y enfocados únicamente en la orquestación.

---

## 🔄 DTOs y Mappers

- La API **nunca expone entidades del dominio**
- Se utilizan **DTOs** para requests y responses
- Los **mappers** viven en la capa **Application**
- El dominio permanece aislado de detalles de transporte

---

## 🌐 Acceso a la API

### Producción

https://ecommerce-backend-net.onrender.com


### Swagger:

http://localhost:5028/swagger/index.html