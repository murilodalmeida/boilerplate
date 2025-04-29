# fwksLabs Boilerplate Service

## Add NuGet Package Repository

- Generate your PAT token with `read:packages` permission here.

dotnet nuget add source \
  --name Github \
  --username YOUR_GITHUB_USERNAME \
  --password YOUR_GITHUB_PAT \
  https://nuget.pkg.github.com/fwkslabs/index.json
