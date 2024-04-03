IF NOT EXISTS (
    SELECT name
    FROM master.dbo.sysdatabases
    WHERE name = N'TestUmbraco'
)
BEGIN
    CREATE DATABASE TestUmbraco;
END;