IF NOT EXISTS (
    SELECT name
    FROM master.dbo.sysdatabases
    WHERE name = N'TutorProUmbraco'
)
BEGIN
    CREATE DATABASE TutorProUmbraco;
END;