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
	1. Download assets from sample repo and add to project https://github.com/Togusa09/AspNetCoreAuth/releases. Release contains scripts and views to simplify demonstrating and testing auth.
	2. Should container setup instruction go here, or leave to OIDC section
		   1. User docker compose file `Docker/docker-compose.json`