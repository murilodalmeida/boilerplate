SERVICE_NAME=Boilerplate
INFRA_PROJECT=./backend/tools/Migrations/Migrations.csproj
MIGRATIONS_DIR=History
CONNECTION_STRING="Host=localhost;Port=5432;Username=local;Password=local;Database=$(SERVICE_NAME);"

## General

vs:
	/c/Program\ Files/Microsoft\ Visual\ Studio/2022/Community/Common7/IDE/devenv.exe ./backend/$(SERVICE_NAME).sln &

format:
	dotnet format ./backend/$(SERVICE_NAME).sln

build:
	dotnet build ./backend/$(SERVICE_NAME).sln

## EF Helpers

db-add:
	dotnet ef migrations add $(or $(name), InitialDb) --project $(INFRA_PROJECT) -o $(MIGRATIONS_DIR) -- $(CONNECTION_STRING)

db-remove:
	dotnet ef migrations remove --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-recreate:
	make db-remove
	make db-add $(name)

db-script:
	dotnet ef migrations script --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-update:
	dotnet ef database update --project $(INFRA_PROJECT) -- $(CONNECTION_STRING)

db-drop:
	dotnet ef database drop --project $(INFRA_PROJECT) --force -- $(CONNECTION_STRING)

db-reset:
	make db-remove
	make db-drop
