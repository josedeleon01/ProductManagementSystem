# Product Management System
This repository hosts a web application built on ASP.NET Web API and Blazor UI, utilizing Sqlite as a portable database. The application serves as a Product Management System, allowing employees to manage customers, items, categories and assignments for customers and items.

## Features
- **Category Module:** CRUD operations for managing categories.
- **Item Module:** CRUD operations for managing items.
- **Customer Module:** CRUD operations for managing customers.
- **Customer Item Module:** CRUD operations for assigning items to customers.
- **Report Module:** Generate reports such as customers with their most expensive items and Item number range search.
- **Authentication:** Utilizes ASP.NET Identity and JWT for secure authentication.
- **Clean Front-End:** Built with Blazor for a clean and interactive user interface.
- **Form Validations:** Implements form validations on both back-end and front-end.
## Prerequisites

Before you begin, ensure you have the following prerequisites installed:

- **.NET 8 SDK**  download link here: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- ***Visual Studio updates*** Version 17.9.5 (or newer updates)
## How to Run the Project

To run the Product Management System locally, **we need to have running the 2 main projects** the API and the front-end client application. follow these steps:

1. Clone the repository:
   ```
   git clone https://github.com/josedeleon01/ProductManagementSystem.git
   ```
2. Navigate to the project directory: 
    Open **ProductManagementSystem** directory and open the solution file`ProductManagementSystem.sln` 

3. Set multiple startup projects in Visual Studio:
In Solution Explorer, select the solution (the top node). Right-click the solution then select select properties option. 
The Solution Property Pages dialog box appears.
Expand the Common Properties node, and choose Startup Project.
Choose the Multiple Startup Projects option and set the Action for ProductManagementSystem.API and  ProductManagementSystem.Client as Start. Click Apply and OK.
For more reference on how to set multiple startup projects in Visual Studio [click here](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2022)
4. Verify the **appsettings.json** configuration **ApiURL** node value for ProductManagementSystem.Client project is pointing to the port of the ProductManagementSystem.API project.

## Login Credentials

To access the application, you can use the following login credentials already stored in the database:

- **Username:** josealbertodeleon8@gmail.com
- **Password:** myPassw@ord
- 
## Sqlite Database

The Project uses Sqlite database provider. SQLite is a lightweight, file-based relational database management system that is used for embedded database applications due to its simplicity, ease of use, and minimal setup requirements.

You can find the database file named `ProductManagerDB.db` under the `ProductManagementSystem.API` project directory. This file serves as the SQLite database for storing all the data related to the product management system. If you need to inspect or query the database, you can use any SQLite viewer tool, such as [SQLite Browser](https://sqlitebrowser.org). These tools allow you to open the SQLite database file and perform various operations such as querying data, viewing tables, and making modifications directly to the database.

Using SQLite as the database provider offers several advantages, including:
- **Portability:** Since SQLite databases are stored in a single file, they are highly portable and can be easily transferred between different environments.
- **No Server Required:** Unlike traditional relational database management systems, SQLite does not require a separate server process, making it ideal for standalone applications.

## Documentation

For more information, please visit the Documentation:

- **Intro:** [Home](https://josedeleon01.github.io/ProductManagementSystem/index.html)
- **Docs:** [Documets](https://josedeleon01.github.io/ProductManagementSystem/api/ProductManagementSystem.Domain.CustomerItems.CustomerItem.html)






