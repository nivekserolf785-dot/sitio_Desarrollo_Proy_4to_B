#  Historial de Cambios (Changelog) — Paradise Express

Todas las modificaciones, actualizaciones y versiones del sistema **Paradise Express** se documentan en este archivo bajo el estándar de control de versiones semántico.

---

## [1.0.0] - 2026-10-28

* Estructuración inicial del proyecto bajo el patrón arquitectónico **MVC** en C# .NET WinForms.
* Módulo de gestión (CRUD) para Clientes, Vendedores y Productos con persistencia local en archivos planos **CSV**.
* Implementación del motor de facturación con carrito de compras detallado (`ItemCarrito`) y rebaja automática de stock.
* Módulo unificado de consultas analíticas avanzadas (**LINQ**) para control de stock, valor de inventario y mejor cliente.
* Algoritmo de control inteligente de caducidad (`DateTime`) para alerta de productos próximos a vencer o vencidos.
* Módulo de seguridad con autenticación de roles (Gerente / Vendedor) y enmascaramiento con `PasswordChar`.
* Documentación técnica completa con `README.md` y especificación de requerimientos en `docs/Requerimientos.md`.
