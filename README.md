# CRUD-Dapper

CRUD-Dapper is a simple API project that demonstrates CRUD (Create, Read, Update, Delete) operations using Dapper in .NET.

## Project Structure

The project structure is organized as follows:


- **BusinessLayer**: Contains business logic and services.
- **ModelLayer**: Contains domain models and DTOs (Data Transfer Objects).
  - **Dto**: Data Transfer Objects for input and output.
    - StudentCreateDto.cs: DTO for creating a new student.
    - StudentUpdateDto.cs: DTO for updating an existing student.
- **RepositoryLayer**: Contains data access logic and repositories.
  - **Context**: Database context.
  - **Entity**: Entity classes representing database tables.
  - **Interface**: Repository interfaces.
  - **Service**: Repository implementations.
- **SQLQuery1.sql**: SQL queries or scripts.

## Getting Started

1. Clone the repository: `git clone <repository_url>`
2. Open the solution file (`CRUD-Dapper.sln`) in Visual Studio.
3. Build the solution.
4. Run the project (`DapperAPI`).

## Usage

- The API endpoints can be accessed using HTTP requests (e.g., with tools like Postman or through client applications).
- CRUD operations are performed on the `Students` resource using the provided endpoints.

## Dependencies

- .NET Core
- Dapper
- SQL Server (or any other supported database)

