INFRA_PROJECT="apps/backend/tools/Migrations/Migrations.csproj"
MIGRATIONS_DIR="History"

CONNECTION_STRING="Server=localhost;Database=postgres;User Id=postgres;Password=postgres;"

db-init:
	 dotnet ef migrations add InitDatabase --project $(INFRA_PROJECT) -o $(MIGRATIONS_DIR) -- $(CONNECTION_STRING)

db-script:
	 dotnet ef migrations script --project $(INFRA_PROJECT) -- connection-string $(CONNECTION_STRING)

db-update:
	 dotnet ef database update --project $(INFRA_PROJECT) -- connection-string $(CONNECTION_STRING)

db-drop:
	 dotnet ef database drop --project $(INFRA_PROJECT) -- connection-string $(CONNECTION_STRING)
