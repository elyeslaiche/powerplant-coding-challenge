# Powerplant Coding Challenge

This project is a **.NET 10 Web API solution** for the Powerplant Coding Challenge.  
It exposes an API that computes power plant production plans based on input payloads.

---

## Tech Stack

- ASP.NET Core Web API (.NET 10)
- C#
- Dependency Injection (services architecture)
- REST API

---

## Project Structure

powerplant-coding-challenge
│
├── Controllers/ # API endpoints
├── Models/ # Data models (DTOs)
├── Services/ # Business logic (load calculation)
├── Properties/ # Launch settings
├── Program.cs # Entry point
├── appsettings.json # Configuration
└── loadCalculator.csproj


---

## How to Run

### Clone the repo

```bash
git clone https://github.com/elyeslaiche/powerplant-coding-challenge.git
cd powerplant-coding-challenge
```

### Run the API
dotnet run
API will be available at:
http://localhost:8888


## API Endpoints
### Health check


**GET /ping**
Response:
"pong"

### Load calculation
POST /load

You will find examples in the payload_examples directory.
