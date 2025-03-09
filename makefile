INFRA_PROJECT="apps/backend/tools/Migrations/Migrations.csproj"
MIGRATIONS_DIR="History"

## General

format:
	dotnet format ./apps/backend/Boilerplate.sln

## EF Helpers

CONNECTION_STRING="Host=localhost;Port=5432;Username=local;Password=local;Database=Boilerplate;"

db-add:
	@echo "Adding migration: $${name:-InitDatabase}"
	dotnet ef migrations add $${name:-InitDatabase} --project $(INFRA_PROJECT) -o $(MIGRATIONS_DIR) -- $(CONNECTION_STRING)

db-remove:
	@echo "Removing last migration or: $${name:-latest}"
	dotnet ef migrations remove --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-script:
	dotnet ef migrations script --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-update:
	dotnet ef database update --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-drop:
	dotnet ef database drop --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)
