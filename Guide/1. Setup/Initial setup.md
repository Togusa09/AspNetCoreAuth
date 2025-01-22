1. (Option 1) - From repo - TODO: Upload repo somewhere
2. (Option 2) - Manual setup
	1.  Create project
		```
		dotnet new sln
		dotnet new mvc --output src/WebApp
		dotnet sln add src/WebApp
		dotnet new xunit --output src/Tests
		dotnet sln add src/Tests
		
		dotnet add src/Tests/Tests.csproj reference src/WebApp/WebApp.csproj
		```
	1. Download latest release archive from https://github.com/Togusa09/AspNetCoreAuth/releases, and extract files to `src/WebApp`. This archive contains assets to assist in demonstrating auth, and allow the focus to remain on backend code.
	2. Should container setup instruction go here, or leave to OIDC section
		   1. User docker compose file `Docker/docker-compose.json`