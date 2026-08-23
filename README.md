# National Park Trail Booking System

A full-stack trail booking platform for exploring national parks, reserving trail slots, and managing bookings — covering the complete user journey from discovery to confirmed reservation.

## Features

- Browse and explore national parks and trails
- Reserve trail slots with real-time availability
- Secure payment processing via **Stripe**
- Automated booking confirmation alerts via **Twilio SMS**
- [ ] Add: user authentication details (if any)
- [ ] Add: any admin/management features

## Tech Stack

**Backend**
- ASP.NET Core 8 (Web API)
- Entity Framework Core
- SQL Server
- [ ] Confirm: Clean Architecture / Repository Pattern — used in this repo?

**Frontend**
- ASP.NET Core MVC

**Integrations**
- Stripe (payments)
- Twilio (SMS notifications)

## Project Structure

```
National_Park_API_Based/
├── National_Park_API/       # Backend Web API
├── NationalParkWebApp/      # MVC frontend
└── National_Park_API.slnx   # Solution file
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Stripe account + API keys
- Twilio account + API keys

### Setup
```bash
git clone https://github.com/Ash-spector/National_Park_API_Based.git
cd National_Park_API_Based
```

1. Update `appsettings.json` in `National_Park_API` with your SQL Server connection string, Stripe keys, and Twilio credentials.
2. Run EF Core migrations:
   ```bash
   dotnet ef database update
   ```
3. Run the API:
   ```bash
   cd National_Park_API
   dotnet run
   ```
4. Run the MVC frontend:
   ```bash
   cd NationalParkWebApp
   dotnet run
   ```

## API Endpoints

[ ] List your actual endpoints here, e.g.:
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/parks` | List all parks |
| GET | `/api/trails/{id}` | Get trail details |
| POST | `/api/bookings` | Create a booking |


## License

[ ] MIT / none specified
