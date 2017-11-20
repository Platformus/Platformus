// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Npgsql;
using Platformus.Installer.ViewModels.Installation;

namespace Platformus.Installer.Controllers
{
  public class InstallationController : Controller
  {
    private IHostingEnvironment hostingEnvironment;

    public InstallationController(IHostingEnvironment hostingEnvironment)
    {
      this.hostingEnvironment = hostingEnvironment;
    }

    public ActionResult Index()
    {
      return this.View(new IndexViewModelFactory(this).Create());
    }

    [HttpGet]
    public ActionResult Complete()
    {
      return this.View();
    }

    [HttpPost]
    public ActionResult Complete(string usageScenarioCode, string storageTypeCode, string connectionString, string languagePacks)
    {
      Installation installation = ResourceManager.GetInstallation();
      UsageScenario usageScenario = installation.UsageScenarios.FirstOrDefault(us => string.Equals(us.Code, usageScenarioCode, StringComparison.OrdinalIgnoreCase));
      StorageType storageType = installation.StorageTypes.FirstOrDefault(st => string.Equals(st.Code, storageTypeCode, StringComparison.OrdinalIgnoreCase));

      this.ExecuteStorageScript(usageScenario.Code, storageType.Code, connectionString);

      foreach (Package package in usageScenario.Packages)
      {
        if (string.IsNullOrEmpty(package.Condition) || string.Equals(package.Condition.Replace("StorageType=", string.Empty), storageTypeCode, StringComparison.OrdinalIgnoreCase))
        {
          ResourceManager.WriteResourceToFile(
            "Platformus.Installer.Input.Packages." + package.Name,
            Path.Combine(this.hostingEnvironment.ContentRootPath, "Extensions", package.Name)
          );
        }
      }

      foreach (string contentEntry in usageScenario.Content)
      {
        string tempPath = Path.Combine(this.hostingEnvironment.ContentRootPath, "temp.zip");

        ResourceManager.WriteResourceToFile(
          "Platformus.Installer.Input.UsageScenarios." + usageScenario.Code + "." + contentEntry,
          tempPath
        );

        ZipFile.ExtractToDirectory(tempPath, this.hostingEnvironment.ContentRootPath);
        System.IO.File.Delete(tempPath);
      }

      System.IO.File.WriteAllText(
        Path.Combine(this.hostingEnvironment.ContentRootPath, "appsettings.json"),
        ResourceManager.GetAppSettingsTemplate().Replace("{connectionString}", connectionString)
      );

      if (!string.IsNullOrEmpty(languagePacks))
      {
        foreach (string languagePack in languagePacks.Split(',').Where(lp => !string.Equals(lp, "en", StringComparison.OrdinalIgnoreCase)))
        {
          string languagePackPath = Path.Combine(this.hostingEnvironment.ContentRootPath, languagePack + ".zip");

          ResourceManager.WriteResourceToFile(
            "Platformus.Installer.Input.LanguagePacks." + languagePack + ".zip",
            languagePackPath
          );

          ZipFile.ExtractToDirectory(languagePackPath, this.hostingEnvironment.ContentRootPath);
          System.IO.File.Delete(languagePackPath);
        }
      }

      return this.Ok();
    }

    [HttpPost]
    public ActionResult TestConnection(string storageTypeCode, string connectionString)
    {
      // TODO: move storage logic from the controller
      if (string.Equals(storageTypeCode, "PostgreSql", StringComparison.OrdinalIgnoreCase))
        return this.TestPostgreSqlConnection(connectionString);

      if (string.Equals(storageTypeCode, "Sqlite", StringComparison.OrdinalIgnoreCase))
        return this.TestSqliteConnection(connectionString);

      if (string.Equals(storageTypeCode, "SqlServer", StringComparison.OrdinalIgnoreCase))
        return this.TestSqlServerConnection(connectionString);

      return this.CreateTestConnectionActionResult(false);
    }

    private ActionResult TestPostgreSqlConnection(string connectionString)
    {
      try
      {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);

        connection.Open();
        connection.Close();
        return this.CreateTestConnectionActionResult(true);
      }

      catch
      {
        return this.CreateTestConnectionActionResult(false);
      }
    }

    private ActionResult TestSqliteConnection(string connectionString)
    {
      try
      {
        SqliteConnection connection = new SqliteConnection(connectionString);

        connection.Open();
        connection.Close();
        return this.CreateTestConnectionActionResult(true);
      }

      catch
      {
        return this.CreateTestConnectionActionResult(false);
      }
    }

    private ActionResult TestSqlServerConnection(string connectionString)
    {
      try
      {
        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();
        connection.Close();
        return this.CreateTestConnectionActionResult(true);
      }

      catch
      {
        return this.CreateTestConnectionActionResult(false);
      }
    }

    private ActionResult CreateTestConnectionActionResult(bool successfulConnection)
    {
      return this.Json(new { successfulConnection = successfulConnection });
    }

    private void ExecuteStorageScript(string usageScenarioCode, string storageTypeCode, string connectionString)
    {
      string storageScript = ResourceManager.GetResourceString(
        "Platformus.Installer.Input.UsageScenarios." + usageScenarioCode + "." + storageTypeCode + ".sql"
      );

      // TODO: move storage logic from the controller
      if (string.Equals(storageTypeCode, "PostgreSql", StringComparison.OrdinalIgnoreCase))
        this.ExecutePostgreSqlCommand(connectionString, storageScript);

      if (string.Equals(storageTypeCode, "Sqlite", StringComparison.OrdinalIgnoreCase))
        this.ExecuteSqliteCommand(connectionString, storageScript);

      if (string.Equals(storageTypeCode, "SqlServer", StringComparison.OrdinalIgnoreCase))
        this.ExecuteSqlServerCommand(connectionString, storageScript);
    }

    private void ExecutePostgreSqlCommand(string connectionString, string storageScript)
    {
      try
      {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);

        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand(storageScript, connection);

        command.ExecuteNonQuery();
        connection.Close();
      }

      catch
      {
        // TODO: implement error handling
      }
    }

    private void ExecuteSqliteCommand(string connectionString, string storageScript)
    {
      try
      {
        SqliteConnection connection = new SqliteConnection(connectionString);

        connection.Open();

        SqliteCommand command = new SqliteCommand(storageScript, connection);

        command.ExecuteNonQuery();
        connection.Close();
      }

      catch
      {
        // TODO: implement error handling
      }
    }

    private void ExecuteSqlServerCommand(string connectionString, string storageScript)
    {
      try
      {
        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();

        SqlCommand command = new SqlCommand(storageScript, connection);

        command.ExecuteNonQuery();
        connection.Close();
      }

      catch (Exception e)
      {
        // TODO: implement error handling
      }
    }
  }
}