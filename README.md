# Activities API

A **.NET 10 Minimal API** implementing **Clean Architecture** and **CQRS** using **MediatR**.  
This project demonstrates a clean separation of concerns with layers for Domain, Application, and API.

## Features

- Clean Architecture structure: Domain, Application, API
- CQRS pattern with separate query and command handlers
- Minimal API endpoints for simplicity and performance
- Fully built with .NET 10
- DTOs and MediatR handlers for Activities domain

## Repository Structure

Activities/
├── API/                         # Minimal API project
├── Activities.sln               # Solution file
├── README.md
└── .gitignore

Inside the API folder you will typically see:

- Program.cs – configures endpoints and DI
- Application/ – Commands, Queries, DTOs, Handlers
- Domain/ – Core entities like Activity, ActivityAttendee, Comment
- Infrastructure/ – Repository interfaces
- Presentation/ End point

## Getting Started

### Prerequisites

- .NET 10 SDK
- Visual Studio 2022 / VS Code
- Optional: Postman or any REST client

### Run Locally

1. Clone the repository:

git clone https://github.com/assiaKd/Activities.git

2. Navigate to the API folder:

cd Activities/API

3. Run the Minimal API:

dotnet run

The API will start on https://localhost:5001 by default.

## Architecture Overview

Clean Architecture Layers:

- Domain – Core business entities and logic
- Application – Use cases (Commands, Queries), DTOs, Interfaces
- API – Minimal API endpoints, dependency injection, MediatR configuration

This separation improves maintainability, testability, and scalability.

## How It Works

### CQRS with MediatR

- Commands → represent writes
- Queries → represent reads
- Handlers implement IRequestHandler<TRequest, TResponse>

Minimal API endpoints call MediatR handlers directly, keeping the controllers thin.

Example:

public class CreateActivity
{
    public class Command : IRequest<string> 
    {
        public required Activity Activity { get; set; }
    }

    public class Handler(IActivityRepository repository) 
        : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            return await repository.CreateActivity(request.Activity, cancellationToken);
        }
    }
}


## Deployment

You can deploy this Minimal API anywhere .NET 10 is supported:

- Azure App Service
- Docker container
- Linux hosting with systemd

## Future Improvements

- Add authentication (JWT or Identity)
- Add Swagger / OpenAPI documentation