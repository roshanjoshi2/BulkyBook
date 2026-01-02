# BulkyBook – Enterprise ASP.NET Core Application

BulkyBook is a production-ready, enterprise-level ASP.NET Core MVC application designed to demonstrate real-world 
software engineering practices used in professional environments. The project follows clean architecture and layered 
design principles, ensuring scalability, maintainability, and testability. It implements industry-standard patterns
such as Repository and Unit of Work, uses interfaces for abstraction, and applies core OOP principles including
encapsulation and polymorphism. The application includes secure cookie-based authentication, role-based authorization,
complete CRUD operations, centralized database management using Entity Framework Core, and a well-organized folder 
structure that mirrors enterprise standards. This project showcases my ability to design and build end-to-end systems 
that are production-ready and easy to extend.

BulkyBook

 BulkyBook.Web          ? Presentation Layer (MVC)
 BulkyBooks.Models     ? Domain & View Models
 BulkyBook.DataAccess  ? Data Access Layer
 BulkyBooks.Utility    ? Common Utilities & Constants
 BulkyBook.sln         ? Solution Entry Point

 Solution Architecture (Layered Design)

 BulkyBook.Web – Presentation Layer
Handles all user interactions and UI logic using ASP.NET Core MVC. This layer manages controllers, 
views, authentication flow, authorization checks, and request/response handling while keeping business 
logic isolated from the UI.

 BulkyBooks.Models – Domain & View Models
Contains domain entities and view models used throughout the application. 
This layer represents the core business objects and ensures strong typing, validation, and clean data transfer between layers.

 BulkyBook.DataAccess – Data Access Layer
Implements the Repository and Unit of Work patterns to manage all database operations. 
This layer handles Entity Framework Core, database context, migrations, and transactions while keeping data logic 
abstracted from the rest of the application.

 BulkyBooks.Utility – Common Utilities
Stores shared constants, helper classes, and reusable utilities used across the solution. 
This layer helps eliminate duplication and keeps cross-cutting concerns centralized.

 Security & Authentication
The application uses secure cookie-based authentication with role-based authorization. 
Access to controllers and actions is restricted based on user roles, ensuring proper security boundaries 
similar to enterprise applications.

Tech Stack
ASP.NET Core MVC
Entity Framework Core
SQL Server
C#
Razor Views
Cookie Authentication
Repository + Unit of Work
Git Version Control


Design Principles & Patterns Used
Clean Architecture
Repository Pattern
Unit of Work Pattern
SOLID Principles
Abstraction via Interfaces
Encapsulation & Polymorphism
Separation of Concerns

 Features Implemented
 Full CRUD operations
 Authentication & authorization
 Role management
 Database migrations & management
 Strongly typed models
 Clean MVC flow
 Centralized configuration
 Reusable utilities & constants
 Well-structured folders and naming conventions


 About the Developer
This project was built over three years ago and reflects the architectural foundation of my engineering approach. 
Since then, I have advanced significantly by delivering and maintaining production systems for clients, 
with a strong focus on scalability, security, and clean design. 
I now apply these principles at a higher level of depth and maturity across real-world, business-critical applications.
