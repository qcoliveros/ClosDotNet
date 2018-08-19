-- This is to retrieve all the data to new aspnet.identity.
-- Todo: Need to think how to auto create the corresponding sml_user.

INSERT INTO AspNetUsers 
    SELECT au.UserId AS Id,
        am.Email,
        0 AS EmailConfirmed,
        'AJYY9lvOaRfGnRSrmqrxNNwaGzLe1FFLi/WnSF5xvCeTmtFGEjbfnCqmkHFSL6jnZA==' AS PasswordHash,
        '69489b51-918f-479b-a25b-bdd12a32539c' AS SecurityStamp,
        NULL AS PhoneNumber,
        0 AS PhoneNumberConfirmed,
        0 AS TwoFactorEnabled,
        NULL AS LockoutEndDateUtc,
        1 AS LockoutEnabled,
        0 AS AccessFailedCount,
        au.UserName
        FROM aspnet_Users au
        INNER JOIN aspnet_Membership am
    ON au.UserId = am.UserId
;
GO

INSERT INTO AspNetRoles
    SELECT ar.RoleId AS Id,
        ar.RoleName AS Name
    FROM aspnet_Roles ar
; 
GO

INSERT INTO AspNetUserRoles
    SELECT auir.UserId,
        auir.RoleId
    FROM aspnet_UsersInRoles auir
;
GO