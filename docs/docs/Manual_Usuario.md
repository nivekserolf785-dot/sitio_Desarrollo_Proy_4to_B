# 📖 Manual de Usuario — Paradise Express

Guía rápida de operación del sistema comercial de escritorio para la administración de inventarios, control de clientes y emisión de facturas.

---

## 1. 🔐 Inicio de Sesión y Autenticación
1. Al ejecutar la aplicación, aparecerá el formulario de acceso (`Login`).
2. Ingrese su usuario y contraseña asignados. 
3. El sistema validará automáticamente su rol (**Gerente** o **Vendedor**) aplicando enmascaramiento de caracteres por seguridad.

---

## 2. 📦 Gestión de Inventario y Productos
* **Registro:** Ingrese el código, nombre, precio unitario, stock inicial y seleccione la **fecha de caducidad** mediante el selector tipado.
* **Control de Caducidad:** El sistema filtrará automáticamente los artículos críticos (próximos a vencer en 30 días o vencidos) para garantizar la calidad del producto (bolos y artículos de limpieza).

---

## 3. 🛒 Proceso de Facturación
1. Acceda al módulo de Facturación.
2. Seleccione el cliente registrado y el vendedor en turno.
3. Agregue los productos al **carrito de compras** especificando las cantidades.
4. El sistema calculará de forma automática los subtotales e impuestos, serializando los ítems en el archivo transaccional y actualizando el stock de bodega en tiempo real.

---

## 4. 📊 Módulo de Consultas y Reportes (LINQ)
* Ingrese al formulario unificado de consultas analíticas (`Frm_Consultas_linq`).
* Podrá visualizar instantáneamente:
  * Estado actual del stock en bodega.
  * Identificación del mejor cliente según el volumen monetario de compras.
  * Valor total de la mercancía almacenada.
  * Distribución porcentual del consumo por cliente y desglose por género.

---
<p align="center">Paradise Express — Sistema de Gestión Comercial.</p>
