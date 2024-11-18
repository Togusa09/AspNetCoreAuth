Workshop to introduce basic and intermediate features of authentication and authorisation in ASP.net core.

## Topics
- [[Cookie Based Authentication]]
	- Events
- Sharing cookies between apps?
- Connect to OIDC provide (using docker container)
	- Events
- Claims Identity and Role mapping
- ClaimsTransformation?
- Custom auth scheme (AuthenticationHandler) (from header/query)
	- Configuration of custom schemes
- Mocking authentication for integration tests
-  Custom Policies
	- Requiring roles, schemes, custom assertions
- Custom AuthorizationHandler
	- Policy
	- Resource
	- OperationAuthorizationRequirement
- Backend For Frontend?

## Does not cover (at this stage)
- ASP.net core identity framework https://github.com/dotnet/AspNetCore/tree/main/src/Identity
- Persisting user state to/from database

## Dependencies
- Database? I don't think there's a need, static data should be enough
- Docker for OIDC provider - https://github.com/Soluto/oidc-server-mock
	- Will need to make up sample users and docker compose file
- Azure? Don't anticipate use of any azure resources
- Azurite? Maybe, if end up using data protection framework

