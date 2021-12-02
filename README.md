# RocketShop

[![Build and test](https://github.com/Kosta-Git/RocketShop/actions/workflows/build.yml/badge.svg)](https://github.com/Kosta-Git/RocketShop/actions/workflows/build.yml)

## Migrations

Create: `dotnet ef migrations add -p DataAccess -s API <migration name>`

Remove: `dotnet ef migrations remove -p DataAccess -s API`

DB Update: `dotnet ef database update -p DataAccess -s API`

## Configuration

```
ConnectionStrings:Default=host=;port=;database=;user id=;password=;SearchPath=
Binance:Key=
Binance:Secret=
Binance:Domain=api.binance.com
```