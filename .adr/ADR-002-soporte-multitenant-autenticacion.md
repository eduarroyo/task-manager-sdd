# ADR-003: Soporte Multitenant y Autenticación

## Estado
Aceptado

## Contexto
El sistema debe permitir que múltiples usuarios gestionen sus propias listas y notas de forma aislada, garantizando la privacidad y seguridad de los datos. Se requiere un mecanismo de autenticación robusto y la posibilidad de compartir o hacer públicas ciertas listas o notas.

## Decisión
- El sistema será **multitenant**: cada usuario tendrá acceso únicamente a sus propias listas y notas.
- La **autenticación** se realizará mediante Microsoft Identity (login y contraseña).
- Por defecto, los datos de cada usuario serán **privados** y no accesibles por otros usuarios.
- Se permitirá **compartir** listas/notas o hacerlas **públicas** solo si el usuario lo decide explícitamente.
- La **autorización** garantizará que ningún usuario pueda acceder a datos de otros, salvo en los casos de compartición o publicación.

## Consecuencias
- Refuerza la privacidad y seguridad de los datos de los usuarios.
- Permite escenarios de colaboración y visibilidad pública bajo control del usuario.
- La lógica de autorización debe estar presente en todos los endpoints y consultas de datos.
