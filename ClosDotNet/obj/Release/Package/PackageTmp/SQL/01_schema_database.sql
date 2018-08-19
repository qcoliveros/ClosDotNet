USE master

-- Cleaning up before we start
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'CLOSDB')
DROP DATABASE [CLOSDB]
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'clos')
DROP LOGIN [clos]

-- Creating a database
CREATE DATABASE [CLOSDB]
GO

-- Creating user
CREATE LOGIN [clos] WITH PASSWORD=N'clos', DEFAULT_DATABASE=[CLOSDB], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

-- Switching to our database
USE [CLOSDB]
GO

/*
 * Creating the user in this database
 *
 * We're going to create two users. One called startUser. This is the user 
 * that is going to have sufficient rights to run SqlDependency.Start.
 * The other user is called subscribeUser, and this is the user that is 
 * going to actually register for changes on the Users-table created earlier.
 * Technically, you're not obligated to make two different users naturally, 
 * but I did here anyway to make sure that I know the minimal rights required
 * for both operations
 *
 * Pay attention to the fact that the startUser-user has a default schema set.
 * This is critical for SqlDependency.Start to work. Below is explained why.
 */
CREATE USER [clos] FOR LOGIN [clos] WITH DEFAULT_SCHEMA = [clos]
GO

/*
 * Creating the schema
 *
 * It is vital that we create a schema specifically for startUser and that we
 * make this user the owner of this schema. We also need to make sure that 
 * the default schema of this user is set to this new schema (we have done 
 * this earlier)
 *
 * If we wouldn't do this, then SqlDependency.Start would attempt to create 
 * some queues and stored procedures in the user's default schema which is
 * dbo. This would fail since startUser does not have sufficient rights to 
 * control the dbo-schema. Since we want to know the minimum rights startUser
 * needs to run SqlDependency.Start, we don't want to give him dbo priviliges.
 * Creating a separate schema ensures that SqlDependency.Start can create the
 * necessary objects inside this startUser schema without compromising 
 * security.
 */
CREATE SCHEMA [clos] AUTHORIZATION [clos]
GO

-- Making sure that user is the db owner.
EXEC sp_addrolemember 'db_owner', 'clos'