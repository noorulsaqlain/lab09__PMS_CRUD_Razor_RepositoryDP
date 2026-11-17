# Product Management System (PMS) - CRUD with Repository Pattern

An ASP.NET Core MVC web application implementing full CRUD (Create, Read, Update, Delete) operations for a Product Management System (PMS) using Razor views, ADO.NET (`Microsoft.Data.SqlClient`), SQL Server, and the **Repository Design Pattern**.

---

## 🛠️ Tech Stack & Architecture

- **Framework:** ASP.NET Core (MVC / Razor Views)
- **Language:** C# (.NET 10.0)
- **Data Access:** ADO.NET (`Microsoft.Data.SqlClient`)
- **Database:** Microsoft SQL Server / LocalDB (`pmsdb`)
- **Pattern:** Repository Design Pattern (`IProductRepository` & `ProductRepository`)
- **UI:** Bootstrap 5, Responsive Design, Form Validation

---

## 📂 Project Structure

```text
Lab09/
├── Controllers/
│   ├── HomeController.cs
│   └── ProductsController.cs       # Controller using IProductRepository
├── Data/
│   ├── IProductRepository.cs      # Repository interface abstraction
│   └── ProductRepository.cs       # ADO.NET implementation with parameterized queries
├── Models/
│   ├── ErrorViewModel.cs
│   └── Product.cs                 # Domain model with Data Annotations validation
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml           # Landing page / Dashboard
│   │   └── Privacy.cshtml
│   ├── Products/
│   │   ├── Create.cshtml          # Form to create new product
│   │   ├── Delete.cshtml          # Confirmation page for deleting product
│   │   ├── Edit.cshtml            # Form to update existing product
│   │   └── Index.cshtml           # Product catalog list with action buttons
│   └── Shared/
│       ├── _Layout.cshtml
│       └── _ValidationScriptsPartial.cshtml
├── appsettings.json               # Database connection string
├── sql_script.sql                 # SQL script for database and table setup
└── Program.cs                     # Dependency injection and middleware setup
```

---

## 🗄️ Database Setup

1. Open SQL Server Management Studio (SSMS) or Visual Studio SQL Server Object Explorer.
2. Execute the script found in `Lab09/sql_script.sql` to create the `pmsdb` database, `products` table, and optional seed data.

```sql
CREATE DATABASE [pmsdb];
GO

USE [pmsdb];
GO

CREATE TABLE [dbo].[products] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [description] VARCHAR (50)  NOT NULL,
    [image]       VARCHAR (255) NOT NULL,
    [price]       INT           NOT NULL,
    [quantity]    INT           NOT NULL,
    [status]      VARCHAR (150) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

3. Ensure your connection string in `Lab09/appsettings.json` matches your local SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=pmsdb;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False"
}
```

---

## 🚀 Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/noorulsaqlain/lab09__PMS_CRUD_Razor_RepositoryDP.git
   cd lab09__PMS_CRUD_Razor_RepositoryDP
   ```

2. Restore and build the project:
   ```bash
   dotnet restore
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run --project Lab09
   ```

4. Open your browser and navigate to `https://localhost:7196` (or the port specified in terminal).

---

## ✨ Features

- **Decoupled Architecture:** Clean separation of concerns with `IProductRepository` registered via ASP.NET Core Dependency Injection.
- **Full CRUD:**
  - **Index:** View all products in a styled responsive table with status badges and images.
  - **Create:** Add new products with client and server-side validation.
  - **Edit:** Update product details and preview current images.
  - **Delete:** Safe delete confirmation dialog to prevent accidental deletion.
- **SQL Injection Prevention:** All SQL queries use strictly parameterized `SqlCommand` parameters.
