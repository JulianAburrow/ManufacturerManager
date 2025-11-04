[![Build ManufacturerManager](https://github.com/JulianAburrow/ManufacturerManager/actions/workflows/build.yml/badge.svg)](https://github.com/JulianAburrow/ManufacturerManager/actions/workflows/build.yml)

# ManufacturerManager

A scalable, modern .NET 10 solution for managing manufacturers, built with Blazor, Entity Framework Core, and MudBlazor. The project includes robust unit testing, and is delivered through automated CI/CD.

---

# Features

- **Blazor UI**: Responsive, interactive web interface using MudBlazor components.
- **CQRS Pattern**: Clean separation of command (write) and query (read) logic for maintainability and scalability.
- **Entity Framework Core**: Modern data access with code-first migrations and in-memory support for testing.
- **Comprehensive Testing**: Unit tests (xUnit).
- **Automated CI/CD**: GitHub Actions for build, test, and deployment automation.
- **Error Logging**: Centralized error handling and logging to the database for diagnostics.
- **Document-aware AI Assistant**: Search documents for help with functionality

---

## Tech Stack

- **.NET 10**
- **Blazor (Server)**
- **Entity Framework Core**
- **MudBlazor** (UI components)
- **xUnit** (unit testing)
- **SQL Server** (default, swapped for in-memory in unit tests)
- **GitHub Actions** (CI/CD)
- **Ollama/TinyLlama/gemma3:1b/qwen:0.5b** (AI Assistant)

---

## Local AI Assistant

This project includes support for a document-aware AI assistant powered by [Ollama](https://ollama.com).  
If Ollama is not installed, assistant features will be disabled gracefully.

The models in use are gemma3:1b, qwen:0.5b and tinyllama:latest

---

## Error Handling

- All exceptions in the UI are caught and logged to the database via command handlers.
- User-friendly error messages are displayed using MudBlazor snackbars.
- Errors can be reviewed and managed through the admin user interface.

---

## Setup

This solution contains a database project, which when run will create a seeded database. There are two connection strings in appsettings.json: one for a local SQL Server instance and the other for an instance running in a Docker container.

Create the database, adjust your connection strings and run the application.

If you wish to use the AI-Assistant in the Help page you will need to install Ollama and the TinyLlama model. Alternatively you can choose a model of your own and adjust the code accordingly, but be aware that this has only been tested using TinyLlama.

---
