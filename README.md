# 🚀 Paradise Express — Sistema de Gestión Comercial y Facturación

<p align="center">
  <b>Aplicación de escritorio robusta desarrollada en C# bajo arquitectura MVC con persistencia en archivos CSV y motor de consultas LINQ.</b>
</p>

---

## 📋 Descripción del Proyecto
**Paradise Express** es un sistema integral de escritorio diseñado para optimizar el control de inventarios, la administración de clientes y vendedores, y la emisión de facturas con carrito de compras detallado. El proyecto está construido bajo estrictos principios de **Programación Orientada a Objetos (POO)** y buenas prácticas de ingeniería de software, utilizando una arquitectura modular que separa la lógica de negocio de la interfaz gráfica en Windows Forms.

---

## ✨ Características Principales

* **Arquitectura MVC (Modelo-Vista-Controlador):** Separación clara entre entidades de negocio, vistas de Windows Forms y controladores de listas en memoria.
* **Persistencia Local en CSV:** Sistema sincronizado mediante archivos planos separados por punto y coma, con métodos de lectura y escritura automatizados (`Leer()` y `Guardar()`).
* **Motor de Facturación Avanzado:** Serialización de ítems de carrito dentro de las facturas utilizando delimitadores estructurados (`^` y `|`) para mantener el detalle de los productos vendidos.
* **Módulo de Consultas Analíticas (LINQ):** Formulario unificado para procesar información en tiempo real y generar reportes gerenciales:
  * Stock actual de productos en bodega.
  * Identificación del mejor cliente por volumen de compras.
  * Valor total del inventario.
  * Porcentaje de compras por cliente y distribución por género.
* **Control Inteligente de Caducidad:** Filtro automatizado de productos próximos a caducar (menor a 30 días) o vencidos utilizando tipos de datos `DateTime`.
* **Seguridad y Control de Roles:** Autenticación por roles (Gerente / Vendedor) con enmascaramiento de contraseñas (`PasswordChar`).

---

## 🛠️ Tecnologías y Herramientas
* **Lenguaje:** C# (.NET Framework / WinForms)
* **Paradigmas:** Programación Orientada a Objetos (POO), Expresiones LINQ, Arquitectura MVC.
* **Almacenamiento:** Archivos Planos CSV.
* **Control de Versiones:** Git y GitHub Desktop (con exclusión de datos locales mediante `.gitignore`).
* **Entorno de Desarrollo:** Visual Studio.

---

## 📂 Estructura del Código Fuente
```text
PROYECTO_PA_PARCIAL/
│
├── Controlador/          # Clases de gestión y listas en memoria (TLista_...)
├── Entidades/            # Clases de dominio (Cliente, Producto, Factura, ItemCarrito, Vendedor)
├── Formulario/           # Interfaces gráficas de Windows Forms y Reportes
│   └── Frm_Consultas_linq.cs # Módulo unificado de reportes analíticos con LINQ
├── Data/                 # Archivos CSV locales (Clientes, Productos, Facturas) [Excluidos en Git]
└── Program.cs            # Punto de entrada de la aplicación
