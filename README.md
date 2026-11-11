# Esencia Retro 2.0 — Sistema de Gestión Comercial

## Resumen del Proyecto

**Esencia Retro 2.0** es una aplicación de escritorio desarrollada en C# utilizando Windows Forms, diseñada para la gestión comercial integral de un comercio dedicado a la venta de indumentaria y camisetas de fútbol retro. El sistema centraliza el control de inventario, la gestión de clientes, el punto de venta , el análisis de métricas en tiempo real, los balances contables y la administración de la seguridad bajo una arquitectura robusta en 4 capas.

## Autores

* **Nestor Fabiano Samuel Ibarra**

* **Luca Joaquin Saucedo Nielsen**

*Carrera:* Licenciatura en Sistemas de Información — Universidad Nacional del Nordeste (UNNE)

*Materia:* Taller de Programación II

## Características Principales

* **Punto de Venta :** Interfaz optimizada para transacciones ágiles, selección de clientes y métodos de pago (Efectivo, Transferencia, Débito, Crédito).

* **Control de Inventario:** Administración de catálogo de prendas con filtrado por nombre, control estricto de existencias y actualización automática de stock.

* **Dashboard Analítico:** Panel en tiempo real accesible para todos los roles que muestra la recaudación diaria, el volumen de prendas vendidas y una grilla interactiva de transacciones.

* **Balances Financieros:** Módulo contable con filtros por rango de fechas para consultar los 5 diseños más vendidos, la facturación mensual y el desglose por método de pago.

* **Seguridad y Control de Acceso:** Autenticación de usuarios mediante roles jerárquicos (Administrador, Supervisor y Vendedor) que delimitan los permisos del sistema.

* **Copias de Seguridad:** Utilidad integrada para la generación de respaldos (Backup SQL) y restauración de la base de datos ante contingencias.

## Arquitectura de Software

El proyecto implementa estrictamente una **Arquitectura en 4 Capas** para garantizar la escalabilidad, mantenibilidad y separación de incumbencias:

1. **Capa de Presentación (`Presentacion`):** Formularios gráficos y controles de usuario de Windows Forms (`Form1`, `FormLogin`, `FormBuscarProducto`, `UC_Ventas`, `UC_Productos`, `UC_Dashboard`, `UC_Balance`, `UC_Admin`, `UC_Backup`).

2. **Capa de Negocio (`Negocio`):** Clases encargadas de procesar la lógica del dominio y aplicar las reglas comerciales antes de persistir los datos.

3. **Capa de Entidades (`Entidades`):** Clases modelo y DTOs que representan las estructuras de datos transversales que fluyen entre las distintas capas.

4. **Capa de Datos (`Datos`):** Repositorios y componentes de acceso a datos encargados de la comunicación con el servidor y la ejecución de consultas y procedimientos almacenados.

## Tecnologías Utilizadas

* **Lenguaje:** C# (.NET Framework 4.8 / .NET compatible)

* **Interfaz Gráfica:** Windows Forms

* **Base de Datos:** MariaDB Server 10.4+

* **Gestor de Base de Datos:** DBeaver

* **Entorno de Desarrollo:** Visual Studio Code / Visual Studio

* **Conectividad:** MySqlConnector

## Puesta en Marcha y Configuración

### 1. Requisitos Previos

* Tener instalado **Visual Studio Code** (con la extensión C# / .NET Dev Kit) o **Visual Studio**.

* Tener en ejecución un servidor **MariaDB** (local o contenedor Docker).

* Tener instalado **DBeaver** como cliente administrador.

### 2. Despliegue de la Base de Datos

1. Abra **DBeaver** y conéctese a su servidor MariaDB.

2. Cree la base de datos ejecutando el siguiente comando SQL:

   ```
   CREATE DATABASE `esenciaretro2.0`;
   USE `esenciaretro2.0`;
   
   ```

3. Importe el archivo de respaldo del repositorio (`Backup_Datos_10-11-2026_16-14-24.sql`) o la mas actual para poblar la estructura y los registros iniciales.

### 3. Configuración de Conexión

Verifique que la cadena de conexión en la clase `Conexion.cs` dentro de la Capa de Datos coincida con sus credenciales locales:

```
public static readonly string connectionString = "Server=localhost;Port=3306;Database=esenciaretro2.0;Uid=root;Pwd=puntoybarraroot;";

```

### 4. Ejecución del Proyecto

1. Abra la carpeta del proyecto en **Visual Studio Code**.

2. Ejecute la aplicación.
