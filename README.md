# MiddlewareCenter

A .NET 8 Web API middleware hub using **Clean Architecture** that integrates Coursera, ERP (HR), K2 Nintex, ManageEngine ServiceDesk, and SharePoint On-Premises.

---

## Architecture

```
/src
├── MiddlewareCenter.Domain/          # Entities, base interfaces
├── MiddlewareCenter.Application/     # DTOs, service interfaces
├── MiddlewareCenter.Infrastructure/  # External API clients, DI extensions
└── MiddlewareCenter.API/             # Controllers, middleware, Program.cs
```

### Layer Dependencies
```
API → Application → Domain
API → Infrastructure → Application → Domain
```

---

## Tech Stack

| Item              | Technology                          |
|-------------------|-------------------------------------|
| Framework         | ASP.NET Core 8 Web API              |
| Language          | C# 12                               |
| ORM               | Entity Framework Core 8 + Dapper    |
| Auth              | Windows Authentication (Negotiate)  |
| Documentation     | Swagger / OpenAPI (Swashbuckle 6)   |
| Rate Limiting     | ASP.NET Core Rate Limiting          |
| Target Framework  | .NET 8.0 LTS                        |

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- Visual Studio 2022 (or VS Code with C# extension)
- SQL Server (optional — connection strings are placeholders)
- Windows environment for Windows Authentication

---

## Setup

### 1. Clone / Open the solution

```bash
git clone <repo-url>
cd MiddlewareCenter
```

### 2. Configure `appsettings.json`

Edit `src/MiddlewareCenter.API/appsettings.json` and replace all placeholder values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=MiddlewareCenterDB;..."
  },
  "ExternalApis": {
    "Coursera":    { "BaseUrl": "https://YOUR_COURSERA_URL/api" },
    "ERP":         { "BaseUrl": "https://YOUR_ERP_URL/api" },
    "K2":          { "BaseUrl": "https://YOUR_K2_URL/api" },
    "ServiceDesk": { "BaseUrl": "https://YOUR_SD_URL/api" },
    "SharePoint":  { "BaseUrl": "https://YOUR_SP_URL/api" }
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:3000", "http://localhost:4200"]
  },
  "RateLimiting": {
    "PermitLimit": 100,
    "QueueLimit": 20
  }
}
```

### 3. Build & Run

```bash
dotnet build MiddlewareCenter.sln
dotnet run --project src/MiddlewareCenter.API
```

The API will start on:
- HTTP:  `http://localhost:5000`
- HTTPS: `https://localhost:5001`

### 4. Swagger UI

Open `https://localhost:5001/swagger` in your browser.

---

## API Endpoints

### Coursera (`/api/v1/coursera`)
| Method | Endpoint                                          | Description          |
|--------|---------------------------------------------------|----------------------|
| GET    | `/courses`                                        | List all courses     |
| GET    | `/courses/{courseId}`                             | Get course by ID     |
| GET    | `/courses/{courseId}/progress`                    | Get course progress  |
| GET    | `/courses/{courseId}/content`                     | Get course modules   |
| GET    | `/courses/{courseId}/quizzes`                     | Get quizzes          |
| POST   | `/courses/{courseId}/quizzes/{quizId}/submit`     | Submit quiz          |

### ERP HR (`/api/v1/erp`)
| Method | Endpoint                          | Description              |
|--------|-----------------------------------|--------------------------|
| GET    | `/wfh`                            | List WFH requests        |
| GET    | `/wfh/{id}`                       | Get WFH by ID            |
| POST   | `/wfh`                            | Create WFH request       |
| PUT    | `/wfh/{id}/cancel`                | Cancel WFH               |
| PUT    | `/wfh/{id}/approve`               | Approve WFH              |
| GET    | `/leaves`                         | List leave requests      |
| GET    | `/leaves/balance`                 | Get leave balance        |
| POST   | `/leaves`                         | Create leave request     |
| PUT    | `/leaves/{id}/cancel`             | Cancel leave             |
| GET    | `/vacations`                      | List vacations           |
| POST   | `/vacations`                      | Create vacation          |
| PUT    | `/vacations/{id}/cancel`          | Cancel vacation          |
| GET    | `/letters`                        | List HR letters          |
| GET    | `/letters/{id}`                   | Get letter by ID         |
| POST   | `/letters`                        | Request HR letter        |
| GET    | `/letters/{id}/download`          | Download letter PDF      |
| GET    | `/work-location`                  | List work locations      |
| POST   | `/work-location`                  | Create work location     |
| PUT    | `/work-location/{id}/approve`     | Approve work location    |
| GET    | `/id-replacement`                 | List ID replacements     |
| GET    | `/id-replacement/{id}`            | Get ID replacement       |
| POST   | `/id-replacement`                 | Request ID replacement   |
| GET    | `/points`                         | Get HR points            |
| GET    | `/requests/received`              | Received requests        |
| GET    | `/requests/sent`                  | Sent requests            |

### K2 Nintex (`/api/v1/k2`)
| Method | Endpoint                    | Description           |
|--------|-----------------------------|-----------------------|
| GET    | `/memos/pending`            | Pending memos         |
| GET    | `/memos/pending/count`      | Pending memo count    |
| GET    | `/memos/{id}`               | Get memo by ID        |
| POST   | `/memos/{id}/approve`       | Approve memo          |
| POST   | `/memos/{id}/reject`        | Reject memo           |
| GET    | `/memos/history`            | Memo history          |

### ServiceDesk (`/api/v1/servicedesk`)
| Method | Endpoint                        | Description              |
|--------|---------------------------------|--------------------------|
| GET    | `/tickets/pending`              | Pending tickets          |
| GET    | `/tickets/pending/count`        | Pending ticket count     |
| GET    | `/tickets/{id}`                 | Get ticket by ID         |
| POST   | `/tickets/{id}/approve`         | Approve ticket           |
| POST   | `/tickets/{id}/reject`          | Reject ticket            |
| GET    | `/tickets/{id}/history`         | Ticket history           |
| GET    | `/tickets`                      | List tickets (filtered)  |

### SharePoint (`/api/v1/sharepoint`)
| Method | Endpoint                                          | Description         |
|--------|---------------------------------------------------|---------------------|
| GET    | `/sites`                                          | List sites          |
| GET    | `/sites/{siteId}/lists`                           | List site lists     |
| GET    | `/sites/{siteId}/lists/{listId}/items`            | List items          |
| POST   | `/sites/{siteId}/lists/{listId}/items`            | Create item         |
| PUT    | `/sites/{siteId}/lists/{listId}/items/{itemId}`   | Update item         |
| DELETE | `/sites/{siteId}/lists/{listId}/items/{itemId}`   | Delete item         |
| GET    | `/sites/{siteId}/files`                           | List files          |
| GET    | `/sites/{siteId}/files/download`                  | Download file       |
| POST   | `/sites/{siteId}/files/upload`                    | Upload file         |

### Health Check
```
GET /health
```

---

## Response Format

All endpoints return:

```json
{
  "success": true,
  "transaction_id": "a1b2c3d4e5f6...",
  "error_message": null,
  "data": { ... }
}
```

Every response includes the `X-Transaction-Id` header for tracing.

---

## Features

| Feature                    | Implementation                                      |
|----------------------------|-----------------------------------------------------|
| Windows Authentication     | `Microsoft.AspNetCore.Authentication.Negotiate`     |
| CORS                       | Configurable origins from `appsettings.json`        |
| Swagger UI                 | Swashbuckle 6 at `/swagger`                         |
| Rate Limiting              | Per-user fixed-window (100 req/min default)         |
| Request/Response Logging   | `RequestResponseLoggingMiddleware` with timing      |
| Global Error Handling      | `GlobalExceptionHandlingMiddleware`                 |
| Transaction ID             | `TransactionIdMiddleware` + `X-Transaction-Id` header |
| Health Check               | `/health` endpoint                                  |

---

## Adding Authentication to External APIs

The `ExternalApiClientBase` does not add auth headers by default. To add per-client auth, override in the client constructor:

```csharp
// Example: Bearer token
_httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", "YOUR_TOKEN");

// Example: API Key
_httpClient.DefaultRequestHeaders.Add("X-Api-Key", "YOUR_KEY");
```

---

## Project Structure

```
MiddlewareCenter.sln
src/
├── MiddlewareCenter.Domain/
│   ├── Common/BaseEntity.cs
│   ├── Entities/RequestLog.cs
│   └── Interfaces/IExternalApiClient.cs
│
├── MiddlewareCenter.Application/
│   ├── DTOs/
│   │   ├── Common/ApiResponse.cs, CommonRequests.cs
│   │   ├── Coursera/CourseraDtos.cs
│   │   ├── ERP/ErpDtos.cs
│   │   ├── K2/K2Dtos.cs
│   │   ├── ServiceDesk/ServiceDeskDtos.cs
│   │   └── SharePoint/SharePointDtos.cs
│   └── Interfaces/
│       ├── ICourseraApiClient.cs
│       ├── IErpApiClient.cs
│       ├── IK2ApiClient.cs
│       ├── IServiceDeskApiClient.cs
│       └── ISharePointApiClient.cs
│
├── MiddlewareCenter.Infrastructure/
│   ├── Clients/
│   │   ├── ExternalApiClientBase.cs
│   │   ├── CourseraApiClient.cs
│   │   ├── ErpApiClient.cs
│   │   ├── K2ApiClient.cs
│   │   ├── ServiceDeskApiClient.cs
│   │   └── SharePointApiClient.cs
│   └── Extensions/InfrastructureServiceExtensions.cs
│
└── MiddlewareCenter.API/
    ├── Controllers/
    │   ├── ApiControllerBase.cs
    │   └── v1/
    │       ├── CourseraController.cs
    │       ├── ErpController.cs
    │       ├── K2Controller.cs
    │       ├── ServiceDeskController.cs
    │       └── SharePointController.cs
    ├── Middleware/
    │   ├── TransactionIdMiddleware.cs
    │   ├── GlobalExceptionHandlingMiddleware.cs
    │   └── RequestResponseLoggingMiddleware.cs
    ├── Program.cs
    ├── appsettings.json
    └── appsettings.Development.json
```
