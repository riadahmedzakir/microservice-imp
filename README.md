# Learning microservice architecture 

Replicating identity micro service with JWT token based authentication

    # Role based authentication
    # Role base data access
    # Adding new feature on the go to learn in deapths

## Done so far
        # Implemented Identityserver4 to get Token
        # Multi tenant/client from datbase as configurables
        # Working with custom claims from tenant User
        # Custom Role based authorization
        # Roles are configurable from dabase.
        # Each action uses CustomAuthorizeAttribute to check request permission from database as configurable
        # Implement password hassing
        # Created user creation endpoint with FluentValidation
        # Added self-signed certificate for production enviorment.
        # Created client/tenant to create and login with user. - https://github.com/riadahmedzakir/jwt-token-base-authenticaion
## To do        
        # Make token and refresh token validity configurable.
        # Implement social login.
        # Look into Azure MFA.        
        # Generic database service with rolebased data.
        # Configurable entities and permissions.
        # Data validation