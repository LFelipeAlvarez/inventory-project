# Proyecto de Inventario de Productos

## Requisitos previos
- .NET SDK
- SQL Server
- Node.js y npm
- Angular CLI

## Configuración y ejecución

### Backend

1. Clonar el repositorio:
   ```
   git clone https://github.com/LFelipeAlvarez/inventory-project.git
   ```

2. Crear la base de datos:
   - Nombre: `RIGO_STORE`
   - Ejecutar el script `script.sql`

3. Configurar y ejecutar la API:
   ```
   cd ProductInventory
   dotnet restore
   cd Presentation
   dotnet run
   ```

   La API estará disponible en: `http://localhost:5157/api/product`

### Frontend

1. Instalar dependencias y ejecutar:
   ```
   cd frontend
   npm install
   ng serve --open
   ```

   Se abrirá automáticamente una pestaña en el navegador con la aplicación en: `http://localhost:4200/`.
