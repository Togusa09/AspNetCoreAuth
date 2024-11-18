1. From repo - TODO: Upload repo somewhere
2. Manual setup
	1.  Create project
		```
		dotnet new sln
		dotnet new mvc --output src/WebApp
		dotnet sln add src/WebApp
		dotnet new xunit --output src/Tests
		dotnet sln add src/Tests
		
		dotnet add src/Tests/Tests.csproj reference src/WebApp/WebApp.csproj
		```
	1. Container setup?