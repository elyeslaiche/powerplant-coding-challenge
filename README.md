# Powerplant Coding Challenge

This project is a **.NET 10 Web API** that computes an optimal production plan for a set of power plants based on a given electricity demand.

The objective is to **meet the required load at minimal cost** while respecting each plant’s operational constraints.

---

## Problem Overview

The API receives:

* A target **load (MW)**
* A list of **power plants**:

  * Type (`gasfired`, `turbojet`, `windturbine`)
  * Efficiency
  * Minimum and maximum production (`pmin`, `pmax`)
* Fuel data:

  * Gas price
  * Kerosine price
  * CO₂ price
  * Wind percentage

The system computes how much each plant should produce such that:

* Total production = required load
* Total cost is minimized
* All constraints are respected

---

## Cost Model

Each power plant is assigned a **cost per MWh**, which determines its priority.

### Base cost

For thermal plants:

```csharp
cost = fuelPrice / efficiency;
```

### CO₂ Cost Integration

Gas-fired power plants emit CO₂ and must include an additional cost:

* Emission factor: **0.3 ton CO₂ per MWh**
* CO₂ price: provided in input

```csharp
co2Cost = 0.3 * co2Price;
totalCost = (fuelPrice / efficiency) + co2Cost;
```

Wind turbines:

* No fuel cost
* No CO₂ emissions
* Only based on wind % => Pmax = Pmax * wind(%)

This introduces a **carbon pricing mechanism**, where higher emissions lead to higher production costs.

---

## Algorithm

The solution follows a **merit-order dispatch** approach:

1. **Adjust wind capacity**

   * `pmax` scaled by wind percentage

2. **Compute cost per MWh**

   * Includes fuel and CO₂ cost where applicable

3. **Sort power plants by cost (ascending)**

4. **Dispatch production**

   * Allocate load starting from the cheapest plant
   * Respect `pmin` and `pmax`
   * Continue until total load is satisfied

---

## API Endpoints

### Health Check

**GET /ping**

Response:

```
"pong"
```

---

### Production Plan

**POST /load**

Computes the optimal production plan.

Example payloads are available in the `payload_examples` directory.

---

## How to Run

### Clone the repository

```bash
git clone https://github.com/elyeslaiche/powerplant-coding-challenge.git
cd powerplant-coding-challenge
```

### Run the API

```bash
dotnet run
```

API will be available at:

```
http://localhost:8888
```

---

## Project Structure

```
Controllers/   # API endpoints
Models/        # Data transfer objects (DTOs)
Services/      # Core business logic (load calculation)
Program.cs     # Application entry point
appsettings.json
```

---

## Design Choices

* **Merit-order algorithm**
  Simple and efficient approach aligned with real-world electricity markets

* **Cost-driven optimization**
  CO₂ is modeled as a *price component*, not a hard constraint

* **Service-based architecture**
  Clear separation between API layer and business logic

---

## Possible Improvements

* Introduce unit commitment constraints (startup costs, ramping)
* Replace greedy approach with linear programming
* Add validation and error handling for edge cases
* Extend CO₂ modeling with emission caps

---

## Author

Elyes Laiche
