USE master
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TestTemplate16Db')
BEGIN
  CREATE DATABASE TestTemplate16Db;
END;
GO

USE TestTemplate16Db;
GO

IF NOT EXISTS (SELECT 1
                 FROM sys.server_principals
                WHERE [name] = N'TestTemplate16Db_Login' 
                  AND [type] IN ('C','E', 'G', 'K', 'S', 'U'))
BEGIN
    CREATE LOGIN TestTemplate16Db_Login
        WITH PASSWORD = '<DB_PASSWORD>';
END;
GO  

IF NOT EXISTS (select * from sys.database_principals where name = 'TestTemplate16Db_User')
BEGIN
    CREATE USER TestTemplate16Db_User FOR LOGIN TestTemplate16Db_Login;
END;
GO  


EXEC sp_addrolemember N'db_datareader', N'TestTemplate16Db_User';
GO

EXEC sp_addrolemember N'db_datawriter', N'TestTemplate16Db_User';
GO

EXEC sp_addrolemember N'db_ddladmin', N'TestTemplate16Db_User';
GO
