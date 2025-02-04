1. (Option 1) - Completed code exists at https://github.com/Togusa09/, but need to create an initial state (this could be same as the release archive?)
2. (Option 2) - Manual setup
	1.  Create project 
> [!warning] Port numbers are important
> Later workshop steps are expecting these port numbers. Other ports can be used, but the OIDC docker configuration will also need updating

```
dotnet new sln
dotnet new mvc --output src/WebApp --kestrelHttpPort 5245 --kestrelHttpsPort 7089
dotnet sln add src/WebApp
```

1. Download latest release archive from https://github.com/Togusa09/AspNetCoreAuth/releases, and extract files to `src/WebApp`. This archive contains assets to assist in demonstrating auth, and allow the focus to remain on backend code.