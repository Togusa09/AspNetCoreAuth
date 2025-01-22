Workshop to introduce basic and intermediate features of authentication and authorisation in ASP.net core.

## Topics
- [[1. Cookie Based Authentication]]
	- Events
- Sharing cookies between apps?
- [[1. OIDC Setup]]
	- Events
- Claims Identity and Role mapping
	- [[1. Claims]]
	- [[3. Roles]]
- ClaimsTransformation?
- Auth Attributes [[2. Attributes]]
- [[1. Custom Authentication Scheme Setup]]
	- Configuration of custom schemes
- [[1. Mock Auth Handler]]
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

## To add

Pros and cons of different authorisation methods - When you'd use polices vs just roles

Relate to previous to projects 
Verification of scopes? (too much OIDC?)

Ensure signout of oidc etc proper documented - HttpContext.Signout May be enough but need to ensure oidc signout config is correct

Other confusion part - If you don't specify authorisation attribute - Default Authentication, Fallback Auth Scheme 
- Default auth scheme
- Globally adding authorized
- Attribute hierarchy
- Challenge etc.

Niche case - Trigger re-auth for things like updating passwords

Logging of attempts? - Possibly Seq container  
PII configuration