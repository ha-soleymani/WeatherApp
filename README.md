# 🌦️ Weather App Challenge

A clean, testable, and production-grade microservice built with **.NET Core 8**, designed to fetch and serve weather data via RESTful APIs. This solution demonstrates engineering maturity through modular design, structured logging, full test coverage, and Dockerized deployment.

---

### 🚀 Features

| Feature                                             | Description                                                                 |
|-----------------------------------------------------|-----------------------------------------------------------------------------|
| REST API                                            | Retrieve current weather by city                                            |
| Microservices Architecture                         | Separation of concerns across services                                      |
| MongoDB Integration                                | Caching and persistence layer                                               |
| Structured Logging                                 | Serilog with `DiagnosticContext`                                            |
| Test Coverage                                      | Full unit and integration tests                                             |
| Docker Support                                     | Ready for local and cloud deployment                                        |
| Clean Architecture                                 | Domain, Application, Infrastructure layers                                  |

---

### 🧱 Tech Stack

| Layer            | Technology                              |
|------------------|------------------------------------------|
| Backend API      | .NET Core 8                              |
| Database         | MongoDB                                  |
| Logging          | Serilog + DiagnosticContext              |
| Testing          | xUnit + Moq + TestContainers             |
| Containerization | Docker + Docker Compose                  |
| Observability    | Custom middleware + structured logs      |

---

### 📦 Setup Instructions

| Step            | Command / Requirement                                      |
|-----------------|------------------------------------------------------------|
| Prerequisites   | .NET 8 SDK, Docker, MongoDB                                |
| Clone Repo      | `git clone https://github.com/your-username/weather-app-challenge.git` |
| Navigate        | `cd weather-app-challenge`                                 |
| Build & Run     | `docker-compose up --build`                                |
| Run Tests       | `dotnet test`                                              |

---

### 🔍 API Endpoints

| Method | Endpoint                                      | Description                          |
|--------|-----------------------------------------------|--------------------------------------|
| GET    | http://localhost:5162/api/Weather/{city}      | Get current weather by city          |
| POST   | http://localhost:5128/api/Location            | Save a city to the location cache    |
| GET    | http://localhost:5128/api/location            | Retrieve all saved locations         |

---

### 🧪 Testing Strategy

| Strategy                     | Details                                                                 |
|------------------------------|-------------------------------------------------------------------------|
| Unit Tests                   | Business logic and service layers                                       |
| Mocking                      | External dependencies mocked for deterministic results                 |

---

### 📈 Observability

| Aspect             | Implementation Details                                                       |
|--------------------|-------------------------------------------------------------------------------|
| Request Logging     | Correlation IDs for traceability                                             |
| Error Handling      | Full context captured for exceptions                                         |
| Log Format          | Structured logs for ELK or Seq parsing                                       |

---

### 📄 License

| License | MIT License |

---

### 🙋‍♂️ Author

| Name   | Role                          | Bio                                                                 |
|--------|-------------------------------|----------------------------------------------------------------------|
| Hamed  | Senior Software Engineer & Architect | Passionate about clean architecture, scalable systems, and empowering teams |
