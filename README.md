# 🌐 IoTDeviceManager - Backend API (.NET 9)

![Build Status](https://img.shields.io/badge/build-passing-brightgreen) ![.NET Version](https://img.shields.io/badge/.NET-9.0-purple) ![License](https://img.shields.io/badge/license-MIT-blue)

**IoTDeviceManager** es una solución Backend robusta y escalable construida con **ASP.NET Core (.NET 9)** diseñada para orquestar, monitorear y administrar dispositivos IoT inteligentes.

Este proyecto destaca por su implementación del **Patrón de Diseño Memento**, permitiendo una gestión segura del estado de los dispositivos (snapshots), facilitando la reversión de configuraciones (Undo/Rollback) y la auditoría de cambios históricos.

---

## 🚀 Características Principales

* **Gestión de Estado (Memento Pattern):** Capacidad para guardar "instantáneas" (snapshots) de la configuración de un dispositivo antes de aplicar cambios críticos y restaurarlos si la comunicación con la API IoT falla.
* **Conectividad IoT:** Integración abstracta para comunicar con APIs de terceros (ej. Azure IoT Hub, AWS IoT, o APIs propietarias de dispositivos).
* **High Performance:** Construido sobre las mejoras de rendimiento de .NET 9.
* **Arquitectura Limpia:** Separación de responsabilidades entre Controladores, Servicios, Modelos de Dominio y Capa de Datos.
* **Documentación API:** Swagger/OpenAPI integrado para pruebas y exploración de endpoints.

---

## 🏗 Arquitectura y Patrones

### El Patrón Memento en IoT
En este proyecto, el patrón Memento es crucial para la tolerancia a fallos.


1.  **Originator (El Dispositivo):** Mantiene el estado actual (temperatura, versión de firmware, estado de encendido).
2.  **Memento (El Snapshot):** Objeto inmutable que almacena el estado del dispositivo en un momento específico.
3.  **Caretaker (DeviceManagerService):** Gestiona la lista de historiales y decide cuándo guardar o restaurar un estado, sin conocer los detalles internos del dispositivo.

**Flujo de Ejemplo:**
> *El usuario envía una actualización de firmware -> El sistema crea un Memento -> Se intenta actualizar -> Si falla, el Caretaker restaura el estado anterior usando el Memento.*

---

## 🛠 Tech Stack

* **Framework:** .NET 9 (ASP.NET Core Web API)
* **Lenguaje:** C# 13
* **Base de Datos:** SQL Server / PostgreSQL (Configurable vía EF Core)
* **ORM:** Entity Framework Core 9.0
* **API Documentation:** Scalar / Swagger UI
* **Testing:** xUnit + Moq

---
