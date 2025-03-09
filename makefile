SOLUTION_NAME=Boilerplate.sln
INFRA_PROJECT="apps/backend/tools/Migrations/Migrations.csproj"
MIGRATIONS_DIR="History"
CONNECTION_STRING="Host=localhost;Port=5432;Username=local;Password=local;Database=Boilerplate;"

## General

format:
	dotnet format ./apps/backend/$(SOLUTION_NAME)

## EF Helpers

db-add:
	dotnet ef migrations add $${name:-InitDatabase} --project $(INFRA_PROJECT) -o $(MIGRATIONS_DIR) -- $(CONNECTION_STRING)

db-remove:
	dotnet ef migrations remove --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-script:
	dotnet ef migrations script --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-update:
	dotnet ef database update --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-drop:
	dotnet ef database drop --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)
