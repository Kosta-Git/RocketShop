# RocketShop

## Migrations

Create: `dotnet ef migrations add -p DataAccess -s API <migration name>`

Remove: `dotnet ef migrations remove -p DataAccess -s API`

DB Update: `dotnet ef database update -p DataAccess -s API`
