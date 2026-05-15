# ADR-001: Separación de Contextos y Modularidad

## Estado
Aceptado

## Contexto
El proyecto "Task Manager" es una aplicación web en .NET 10 para la gestión de listas, con MinimalApi, DDD, Vertical Slices, Postgres y Aspire para orquestación. Se busca una arquitectura que permita escalar y mantener el sistema de forma sencilla, evitando acoplamientos innecesarios entre módulos.

## Decisión
- Se opta por un **monolito modular** con enfoque Vertical Slices.
- Existirá un único proyecto principal (API) que orquestará la ejecución y expondrá la capa de presentación.
- Cada módulo/contexto tendrá sus propias capas de aplicación y dominio, implementadas como bibliotecas enlazadas al proyecto principal.
- La capa de presentación será común para todos los módulos.
- La infraestructura será **común** pero organizada de forma desacoplada, permitiendo su futura extracción si algún módulo lo requiere.
- Se utilizarán **esquemas independientes en Postgres** para cada módulo/contexto, garantizando el aislamiento lógico y evitando dependencias cruzadas a nivel de base de datos.

## Consecuencias
- Permite evolucionar hacia microservicios o mayor modularidad si el dominio lo requiere.
- Facilita el mantenimiento y la escalabilidad del sistema.
- El uso de esquemas y roles en Postgres refuerza la separación y seguridad entre módulos.

---

# ADR-002: Organización de Carpetas y Proyectos

## Estado
Aceptado

## Contexto
Se requiere una estructura de carpetas y proyectos que facilite la modularidad, el aislamiento de contextos y la claridad en la evolución del sistema.

## Decisión
- Estructura base:

```
/src
  /Api                # Proyecto principal (MinimalApi, capa de presentación común)
  /Modules
    /Lists            # Biblioteca: aplicación y dominio del módulo de listas
    /Users            # Biblioteca: aplicación y dominio del módulo de usuarios
  /Infrastructure     # Biblioteca común de infraestructura (acceso a datos, servicios externos, etc.)
  /Shared             # Utilidades y contratos compartidos
```

- Cada módulo bajo `/Modules` tendrá su propia lógica de aplicación y dominio, sin acceso directo a los datos de otros módulos.
- La infraestructura común estará desacoplada y organizada por áreas funcionales.
- Los esquemas de base de datos seguirán la misma lógica de separación.

## Consecuencias
- Facilita la evolución y el mantenimiento.
- Permite extraer módulos a microservicios si es necesario.
- Refuerza la separación de responsabilidades y la claridad del código.
