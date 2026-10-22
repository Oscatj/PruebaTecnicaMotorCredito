PruebaTecnicaMotorCredito
API desarrollada con ASP.NET Core, Entity Framework Core y SQL Server para gestionar pedidos y productos.
Arquitectura
El proyecto está organizado por capas para mantener el código ordenado y facilitar su mantenimiento.
- Controllers: reciben las solicitudes HTTP y devuelven las respuestas de la API.
- Services: contienen la lógica de las operaciones. IPedidoService define el contrato y PedidoService lo implementa.
- Data: contiene DataContext, que permite trabajar con la base de datos mediante Entity Framework Core.
- Models: representan las entidades del sistema.
- DTOs: definen los datos que la API recibe y devuelve.
- AutoMapper: facilita la conversión entre entidades y DTOs.
- ServiceResponse: permite organizar las respuestas de los servicios.
  
El flujo principal es:

PedidosController → PedidoService → DataContext → SQL Server

Esta organización aplica la separación de responsabilidades y facilita el uso de principios SOLID. Actualmente, las capas están organizadas en carpetas dentro de un solo proyecto.

Cadena de conexión
La conexión se configura en appsettings.json, dentro de ConnectionStrings, usando la clave sql.
