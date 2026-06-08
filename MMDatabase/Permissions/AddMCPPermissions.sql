------------------------------------------------------------
-- 1. Create the server-level login if it doesn't exist
------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'mcp_sql_user')
BEGIN
    CREATE LOGIN mcp_sql_user WITH PASSWORD = 'Pwd12345!';
END
GO

------------------------------------------------------------
-- 2. Fix orphaned user if it exists but is not mapped
------------------------------------------------------------
USE ManufacturerManager;
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = 'mcp_sql_user')
BEGIN
    DROP USER mcp_sql_user;
END
GO

------------------------------------------------------------
-- 3. Create the database user if it doesn't exist
------------------------------------------------------------
USE ManufacturerManager;
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'mcp_sql_user')
BEGIN
    CREATE USER mcp_sql_user FOR LOGIN mcp_sql_user;
END
GO

------------------------------------------------------------
-- 4. Ensure the user can CONNECT to the database
------------------------------------------------------------
GRANT CONNECT TO mcp_sql_user;
GO

------------------------------------------------------------
-- 5. Grant read-only access
------------------------------------------------------------
ALTER ROLE db_datareader ADD MEMBER mcp_sql_user;
GO

------------------------------------------------------------
-- 6. Explicitly deny dangerous permissions
--    (CONTROL removed — it blocks CONNECT)
------------------------------------------------------------
DENY INSERT, UPDATE, DELETE, ALTER, REFERENCES, TAKE OWNERSHIP TO mcp_sql_user;
DENY EXECUTE TO mcp_sql_user;
GO
