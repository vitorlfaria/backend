# Ambev.DeveloperEvaluation

## Project Description

This project, `Ambev.DeveloperEvaluation`, is a .NET Solution. It follows a layered architecture, separating concerns into distinct projects for domain logic, application services, infrastructure, and presentation (API).

## Solution Structure

The solution is organized into the following projects:

- **Ambev.DeveloperEvaluation.sln**: The main solution file.
- **src/**: Contains the source code for the application.
  - **Ambev.DeveloperEvaluation.Application**: Implements the application layer, handling use cases and business logic.
  - **Ambev.DeveloperEvaluation.Domain**: Defines the core domain entities, repositories, and interfaces.
  - **Ambev.DeveloperEvaluation.ORM**: Manages database interactions using an ORM (likely Entity Framework Core).
  - **Ambev.DeveloperEvaluation.Common**: Contains common utilities, extensions, and cross-cutting concerns.
  - **Ambev.DeveloperEvaluation.IoC**: Handles dependency injection and service registration.
  - **Ambev.DeveloperEvaluation.WebApi**: Exposes the application's functionality through a RESTful API.
- **tests/**: Contains various types of tests.
  - **Ambev.DeveloperEvaluation.Functional**: Functional tests to verify end-to-end behavior.
  - **Ambev.DeveloperEvaluation.Integration**: Integration tests to test interactions between different parts of the system.
  - **Ambev.DeveloperEvaluation.Unit**: Unit tests to test individual components in isolation.
- **docker-compose.dcproj**:  Docker Compose project for containerizing the application.

## Getting Started

### Prerequisites

- .NET SDK (version 8.0 or later)
- Docker (if you want to run the application in a container)

### Setup and running

1. Clone the repository: `git clone [repository_url]`
2. Navigate to the project directory: `cd Ambev.DeveloperEvaluation`
3. Run the docker containers: `docker compose up --build -d`
4. Install dotnet ef: `dotnet tool install --global dotnet-ef`
5. Run the migrations to create the database tables: `dotnet ef database update --project src/Ambev.DeveloperEvaluation.ORM --startup-project src/Ambev.DeveloperEvaluation.WebApi --context DefaultContext`
6. Now the application is running and ready to use. To access it open the browser at `localhost:8080/swagger`