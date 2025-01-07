Workshop to introduce basic and intermediate features of authentication and authorisation in ASP.net core.

## Topics
- [[Cookie Based Authentication]]
	- Events
- Sharing cookies between apps?
- [[OIDC Setup]]
	- Events
- Claims Identity and Role mapping
	- [[1. Claims]]
	- [[3. Roles]]
- ClaimsTransformation?
- Auth Attributes [[2. Attributes]]
- [[Custom Authentication Scheme Setup]]
	- Configuration of custom schemes
- [[Mock Auth Handler]]
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

