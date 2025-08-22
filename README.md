# ISP Backend API - .NET Web API
A RESTful API backend for Internet Service Provider management system built with .NET Web API.

> [!NOTE]
> This API is designed to work with the Flutter-based ISP application. Ensure that CORS is properly configured if accessing from different domains.

## Features
- User authentication (login/logout)
- User data management
- Data usage tracking
- Plan management
- RESTful API endpoints
- MySQL database integration

## Technology Stack
- .NET Core 8.0
- Entity Framework Core
- MySQL with Pomelo.EntityFrameworkCore.MySql
- Riok.Mapperly for object mapping
- Swagger/OpenAPI for documentation

## Requisites
- .NET 8.0 SDK
- MySQL Server (8.0 or higher)

## Installation & Setup
1. Clone the Repository
```bash
git clone https://github.com/vjm-dev/isp_backend_dotnet.git
cd isp_backend_dotnet
```
2. Database Setup
Run the SQL script from `isp_app.sql` in MySQL.

Config the database connection in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=isp_app;user=your_username;password=your_password;"
  }
}
```
4. Run the Application
```bash
dotnet run
```
The API will be available at http://localhost:8080

## Building
### Running in Development Mode
```bash
dotnet run --environment Development
```
Access Swagger documentation at: http://localhost:8080/swagger

### Building for Production
```bash
dotnet publish -c Release
```

## Database Schema
### Tables
- users - User accounts and information
- plans - Internet service plans
- data_usages - Monthly data usage records
- daily_usages - Daily data consumption details

### Sample Data
Default users:

- Guest User: guest@isp.com / password: `1234`

- Premium User: premium@isp.com / password: `1234`

## API Endpoints
Base URL:
```
http://localhost:8080/v1
```

### Authentication
#### Login
```http
POST /auth/login
Content-Type: application/json
{
    "email": "guest@isp.com"
    "password": "1234"
}
```
Login response: 
```json
{
  "id": "user_guest",
  "name": "Guest User",
  "email": "guest@isp.com",
  "phone": "+1234567890",
  "planName": "Internet 100 Mbps",
  "monthlyPayment": 29.99,
  "data_usage": {
    "start_date": "2025-07-22T00:00:00",
    "end_date": "2025-08-26T00:00:00",
    "used": 179.47,
    "limit": 500,
    "daily_usage": [
      {
        "date": "2025-08-20T00:00:00",
        "download": 5.2,
        "upload": 1.1
      },
      {
        "date": "2025-08-21T00:00:00",
        "download": 18.05,
        "upload": 2.9
      },
      {
        "date": "2025-08-22T00:00:00",
        "download": 1.5,
        "upload": 0.1
      }
    ]
  },
  "lastUpdated": "2025-08-22T20:47:17"
}
```
### User data
#### Get User data
```http
GET /users/{user_id}/data
```
Response: Same structure as login response

### Data usage
#### Update Data usage
```http
POST /users/{user_id}/usage
Content-Type: application/json
{
    "amount": 5.5
}
```
Response:
```json
{
    "status": "success"
}
```
### Plans
#### Get all plans
```http
GET /plans
```
Response:
```json
[
  {
    "id": 1,
    "name": "Internet 100 Mbps",
    "speed": "100 Mbps",
    "monthly_payment": 29.99,
    "data_limit": 500,
    "created_at": "2025-08-21T16:12:28",
    "updated_at": "2025-08-21T16:12:28"
  },
  {
    "id": 2,
    "name": "Internet 300 Mbps",
    "speed": "300 Mbps",
    "monthly_payment": 49.99,
    "data_limit": 1000,
    "created_at": "2025-08-21T16:12:28",
    "updated_at": "2025-08-21T16:12:28"
  },
  {
    "id": 3,
    "name": "Internet 1 Gbps",
    "speed": "1 Gbps",
    "monthly_payment": 79.99,
    "data_limit": 2000,
    "created_at": "2025-08-21T16:12:28",
    "updated_at": "2025-08-21T16:12:28"
  }
]
```
#### API Calls

- Login: POST to http://localhost:8080/v1/auth/login

- User data: GET to http://localhost:8080/v1/users/user_guest/data

- Plans: GET to http://localhost:8080/v1/plans

- Update usage: POST to http://localhost:8080/v1/users/user_guest/usage
