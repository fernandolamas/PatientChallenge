using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace PatientChallenge.Service.Initializer
{
    public static class DatabaseInitializer
    {
        public static async Task EnsureDatabaseInitializationAsync(string connectionString)
        {
            string adminPassword = BCrypt.Net.BCrypt.HashPassword("4dmin");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string createDatabaseQuery = $@"
                IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'PatientDB')
                BEGIN
                    CREATE DATABASE PatientDB
                END";
                
                string checkTableQuery = @"
                IF NOT EXISTS (SELECT * FROM PatientDB.sys.tables WHERE name = 'Patients')
                BEGIN
                    USE [PatientDB]
                    CREATE TABLE Patients (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name NVARCHAR(100) NOT NULL,
                        UserName NVARCHAR(255) NOT NULL,
                        Password NVARCHAR(255) NOT NULL,
                        IsActive BIT,
                        Role INT NOT NULL DEFAULT 1
                    )
                END";

                string firstUserQuery = $@"
                    USE [PatientDB]
                    IF NOT EXISTS (SELECT * FROM Patients WHERE UserName = 'admin')
                    BEGIN
                        INSERT INTO Patients (Name, UserName, Password, IsActive, Role)
                        VALUES ('Administrator', 'admin', '{adminPassword}', 1, 0)
                    END
                    ";

                await conn.ExecuteAsync(createDatabaseQuery);
                await conn.ExecuteAsync(checkTableQuery);
                await conn.ExecuteAsync(firstUserQuery);
            }
        }
    }
}
