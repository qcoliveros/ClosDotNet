-- AspNet.Identity
INSERT INTO [AspNetUsers] (
    [Id], 
    [Email], 
    [EmailConfirmed], 
    [PasswordHash], 
    [SecurityStamp], 
    [PhoneNumber], 
    [PhoneNumberConfirmed], 
    [TwoFactorEnabled], 
    [LockoutEndDateUtc], 
    [LockoutEnabled], 
    [AccessFailedCount], 
    [UserName]) 
  VALUES (
    N'04A0F22E-7CE0-48AF-AE5F-22332C881715', 
    N'rmqco01@clos.net', 
    0, 
    N'AJYY9lvOaRfGnRSrmqrxNNwaGzLe1FFLi/WnSF5xvCeTmtFGEjbfnCqmkHFSL6jnZA==', 
    N'69489b51-918f-479b-a25b-bdd12a32539c', 
    NULL, 
    0, 
    0, 
    NULL, 
    1, 
    0, 
    N'rmqco01');
    
INSERT INTO [AspNetUsers] (
    [Id], 
    [Email], 
    [EmailConfirmed], 
    [PasswordHash], 
    [SecurityStamp], 
    [PhoneNumber], 
    [PhoneNumberConfirmed], 
    [TwoFactorEnabled], 
    [LockoutEndDateUtc], 
    [LockoutEnabled], 
    [AccessFailedCount], 
    [UserName]) 
  VALUES (
    N'21AECDF8-56B8-4C03-8273-B6FAA10A5FCA', 
    N'rmtlqco01@clos.net', 
    0, 
    N'AJYY9lvOaRfGnRSrmqrxNNwaGzLe1FFLi/WnSF5xvCeTmtFGEjbfnCqmkHFSL6jnZA==', 
    N'69489b51-918f-479b-a25b-bdd12a32539c', 
    NULL, 
    0, 
    0, 
    NULL, 
    1, 
    0, 
    N'rmtlqco01');
    
INSERT INTO [AspNetUsers] (
    [Id], 
    [Email], 
    [EmailConfirmed], 
    [PasswordHash], 
    [SecurityStamp], 
    [PhoneNumber], 
    [PhoneNumberConfirmed], 
    [TwoFactorEnabled], 
    [LockoutEndDateUtc], 
    [LockoutEnabled], 
    [AccessFailedCount], 
    [UserName]) 
  VALUES (
    N'54F85D85-FBE0-4B8C-A599-027FEDF1F0C2', 
    N'caqco01@clos.net', 
    0, 
    N'AJYY9lvOaRfGnRSrmqrxNNwaGzLe1FFLi/WnSF5xvCeTmtFGEjbfnCqmkHFSL6jnZA==', 
    N'69489b51-918f-479b-a25b-bdd12a32539c', 
    NULL, 
    0, 
    0, 
    NULL, 
    1, 
    0, 
    N'caqco01');

-- roles
INSERT INTO [AspNetRoles] (
    [Id], 
    [Name]) 
  VALUES (
    N'4F90AF3F-799B-4828-81B1-5042A92D2A32', 
    N'CA');
    
INSERT INTO [AspNetRoles] (
    [Id], 
    [Name]) 
  VALUES (
    N'06DCF26E-E0E7-4347-B792-15C0292BC26B', 
    N'RM');
    
INSERT INTO [AspNetRoles] (
    [Id], 
    [Name]) 
  VALUES (
    N'4F9CCADC-3B7A-4E79-A57B-0C4623F51D21', 
    N'RMTL');
    
-- user-role mapping
INSERT INTO [AspNetUserRoles] (
    [UserId], 
    [RoleId]) 
  VALUES (
    N'04A0F22E-7CE0-48AF-AE5F-22332C881715', 
    N'06DCF26E-E0E7-4347-B792-15C0292BC26B');
    
INSERT INTO [AspNetUserRoles] (
    [UserId], 
    [RoleId]) 
  VALUES (
    N'54F85D85-FBE0-4B8C-A599-027FEDF1F0C2', 
    N'4F90AF3F-799B-4828-81B1-5042A92D2A32');
    
INSERT INTO [AspNetUserRoles] (
    [UserId], 
    [RoleId]) 
  VALUES (
    N'21AECDF8-56B8-4C03-8273-B6FAA10A5FCA', 
    N'4F9CCADC-3B7A-4E79-A57B-0C4623F51D21');
    
-- SML_USER

