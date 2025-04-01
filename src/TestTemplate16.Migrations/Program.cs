using System;
using System.IO;
using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TestTemplate16.Migrations;

public class Program
{
    public static int Main(string[] args)
    {
        var connectionString = string.Empty;
        var dbUser = string.Empty;
        var dbPassword = string.Empty;
        var scriptsPath = string.Empty;
        var sqlUsersGroupName = string.Empty;

        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Production";
        Console.WriteLine($"Environment: {env}.");
        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env}.json", true, true)
            .AddEnvironmentVariables();

        var config = builder.Build();
        InitializeParameters();
        var connectionStringBuilderTestTemplate16 = new SqlConnectionStringBuilder(connectionString);
        if (env.Equals("Development"))
        {
            connectionStringBuilderTestTemplate16.UserID = dbUser;
            connectionStringBuilderTestTemplate16.Password = dbPassword;
        }
        else
        {
            connectionStringBuilderTestTemplate16.UserID = dbUser;
            connectionStringBuilderTestTemplate16.Password = dbPassword;
            connectionStringBuilderTestTemplate16.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
        }
        var upgraderTestTemplate16 =
            DeployChanges.To
                .SqlDatabase(connectionStringBuilderTestTemplate16.ConnectionString)
                .WithVariable("SqlUsersGroupNameVariable", sqlUsersGroupName)    // This is necessary to perform template variable replacement in the scripts.
                .WithScriptsFromFileSystem(
                    !string.IsNullOrWhiteSpace(scriptsPath)
                            ? Path.Combine(scriptsPath, "TestTemplate16Scripts")
                        : Path.Combine(Environment.CurrentDirectory, "TestTemplate16Scripts"))
                .LogToConsole()
                .Build();
        Console.WriteLine($"Now upgrading TestTemplate16.");
        if (env == "Development")
        {
            upgraderTestTemplate16.MarkAsExecuted("0000_AzureSqlContainedUser.sql");
        }
        var resultTestTemplate16 = upgraderTestTemplate16.PerformUpgrade();

        if (!resultTestTemplate16.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"TestTemplate16 upgrade error: {resultTestTemplate16.Error}");
            Console.ResetColor();
            return -1;
        }

        // Uncomment the below sections if you also have an Identity Server project in the solution.
        /*
        var connectionStringTestTemplate16Identity = string.IsNullOrWhiteSpace(args.FirstOrDefault())
            ? config["ConnectionStrings:TestTemplate16IdentityDb"]
            : args.FirstOrDefault();

        var upgraderTestTemplate16Identity =
            DeployChanges.To
                .SqlDatabase(connectionStringTestTemplate16Identity)
                .WithScriptsFromFileSystem(
                    scriptsPath != null
                        ? Path.Combine(scriptsPath, "TestTemplate16IdentityScripts")
                        : Path.Combine(Environment.CurrentDirectory, "TestTemplate16IdentityScripts"))
                .LogToConsole()
                .Build();
        Console.WriteLine($"Now upgrading TestTemplate16 Identity.");
        if (env != "Development")
        {
            upgraderTestTemplate16Identity.MarkAsExecuted("0004_InitialData.sql");
            Console.WriteLine($"Skipping 0004_InitialData.sql since we are not in Development environment.");
            upgraderTestTemplate16Identity.MarkAsExecuted("0005_Initial_Configuration_Data.sql");
            Console.WriteLine($"Skipping 0005_Initial_Configuration_Data.sql since we are not in Development environment.");
        }
        var resultTestTemplate16Identity = upgraderTestTemplate16Identity.PerformUpgrade();

        if (!resultTestTemplate16Identity.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"TestTemplate16 Identity upgrade error: {resultTestTemplate16Identity.Error}");
            Console.ResetColor();
            return -1;
        }
        */

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;

        void InitializeParameters()
        {
            // Local database, populated from .env file.
            if (args.Length == 0)
            {
                connectionString = config["TestTemplate16Db_Migrations_Connection"];
                dbUser = config["DbUser"];
                dbPassword = config["DbPassword"];
            }

            // Deployed database
            else if (args.Length == 5)
            {
                connectionString = args[0];
                dbUser = args[1];
                dbPassword = args[2];
                scriptsPath = args[3];
                sqlUsersGroupName = args[4];
            }
        }
    }
}
