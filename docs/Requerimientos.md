#  Especificación de Requerimientos — Paradise Express

Este documento detalla los requerimientos funcionales y no funcionales que definen el alcance, el comportamiento y los estándares técnicos del sistema de gestión comercial **Paradise Express**.

---

##  1. Requerimientos Funcionales (RF)

Los requerimientos funcionales especifican las operaciones lógicas, los flujos de datos y los comportamientos que el sistema debe ejecutar de manera automatizada:

* **RF-01: Gestión Integral de Clientes (CRUD):** El sistema debe permitir el registro, modificación, consulta y baja lógica de clientes, almacenando estrictamente atributos de dominio como cédula, nombres, apellidos, sexo, celular, dirección, ciudad y correo electrónico.
* **RF-02: Gestión del Inventario de Productos:** El sistema debe administrar el catálogo de artículos, controlando de forma concurrente código, nombre, precio unitario, stock disponible y fecha de caducidad tipada mediante objetos de fecha (`DateTime`).
* **RF-03: Módulo de Facturación y Carrito de Compras:** El sistema debe procesar la emisión de facturas asociadas a un cliente y un vendedor, calculando de forma precisa subtotales, impuestos y totales generales, e integrando un carrito dinámico (`ItemCarrito`) que ejecute la rebaja automática de inventario al consolidar la venta.
* **RF-04: Serialización y Persistencia de Facturas:** El sistema debe serializar de manera estructurada los ítems del carrito dentro del archivo plano `facturas.csv` utilizando delimitadores específicos (`^` y `|`), asegurando la integridad transaccional y la compatibilidad retroactiva.
* **RF-05: Módulo de Consultas Analíticas (LINQ):** El sistema debe proporcionar un formulario unificado (`Frm_Consultas_linq`) capaz de procesar consultas en memoria mediante expresiones LINQ para generar reportes gerenciales en tiempo real, incluyendo:
  * Control y visualización del stock actual en bodega.
  * Identificación automatizada del mejor cliente por volumen monetario de compras.
  * Cálculo dinámico del valor total del inventario.
  * Distribución porcentual del volumen de compras por cliente y desglose por género.
* **RF-06: Control Inteligente de Caducidad:** El sistema debe aplicar filtros algorítmicos sobre el inventario para detectar y alertar de forma visual aquellos productos próximos a vencer (menor o igual a 30 días) o cuyo estado se encuentre vencido respecto a la fecha actual del sistema.
* **RF-07: Autenticación Basada en Roles y Seguridad:** El sistema debe validar credenciales de acceso restringidas según el rol del usuario (Gerente / Vendedor), implementando enmascaramiento visual de caracteres sensibles mediante la propiedad `PasswordChar`.

---

##  2. Requerimientos No Funcionales (RNF)

Los requerimientos no funcionales establecen los atributos de calidad, restricciones arquitectónicas y criterios de rendimiento del software:

* **RNF-01: Arquitectura y Modularidad (MVC):** El sistema debe regirse estrictamente bajo el patrón arquitectónico **Modelo-Vista-Controlador**, desacoplando las entidades de dominio de las interfaces gráficas en Windows Forms y centralizando la lógica transaccional en controladores de listas en memoria (`TLista_...`).
* **RNF-02: Persistencia Local en Archivos Planos (CSV):** El sistema debe operar de forma autónoma mediante almacenamiento local en archivos **CSV** separados por punto y coma, sincronizados dinámicamente mediante métodos estáticos de lectura y escritura (`Leer()` y `Guardar()`).
* **RNF-03: Rendimiento y Optimización en Memoria:** El procesamiento y filtrado de grandes volúmenes de datos transaccionales debe resolverse mediante consultas optimizadas en memoria con **LINQ**, garantizando una respuesta inmediata en los controles `DataGridView`.
* **RNF-04: Control de Versiones y Trazabilidad:** El desarrollo del código fuente debe gestionarse a través de **Git y GitHub**, manteniendo un flujo estructurado de ramas y excluyendo los archivos transaccionales locales mediante un archivo `.gitignore` para mitigar conflictos en entornos colaborativos.
* **RNF-05: Mantenibilidad, Tipado y Estándares de POO:** El código fuente debe atenerse rigurosamente a los principios de la Programación Orientada a Objetos (POO), aplicando encapsulamiento, constructores sobrecargados y tipado estricto (como `DateTime` para fechas y `char` para caracteres de género).

---
<p align="center">Documentación oficial de ingeniería de software — Paradise Express.</p>
