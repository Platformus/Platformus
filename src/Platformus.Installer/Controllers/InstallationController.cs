// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

      foreach (Package package in usageScenario.Packages)
      {
        if (string.IsNullOrEmpty(package.Condition) || string.Equals(package.Condition.Replace("StorageType=", string.Empty), storageTypeCode, StringComparison.OrdinalIgnoreCase))
        {
          ResourceManager.WriteResourceToFile(
            "Platformus.Installer.Sources.Packages." + package.Name,
            this.hostingEnvironment.ContentRootPath + "\\Extensions\\" + package.Name
          );
        }
      }

      foreach (string contentEntry in usageScenario.Content)
      {
        string tempPath = this.hostingEnvironment.ContentRootPath + "\\temp.zip";

        ResourceManager.WriteResourceToFile(
          "Platformus.Installer.Sources.Content." + contentEntry,
          tempPath
        );

        ZipFile.ExtractToDirectory(tempPath, this.hostingEnvironment.ContentRootPath);
        System.IO.File.Delete(tempPath);
      }

      System.IO.File.WriteAllText(
        this.hostingEnvironment.ContentRootPath + "\\appsettings.json",
        ResourceManager.GetAppSettingsTemplate().Replace("{connectionString}", connectionString)
      );

      if (!string.IsNullOrEmpty(languagePacks))
      {
        foreach (string languagePack in languagePacks.Split(',').Where(lp => !string.Equals(lp, "en", StringComparison.OrdinalIgnoreCase)))
        {
          string languagePackPath = this.hostingEnvironment.ContentRootPath + "\\Resources\\" + languagePack + ".zip";

          ResourceManager.WriteResourceToFile(
            "Platformus.Installer.Sources.LanguagePacks." + languagePack + ".zip",
            languagePackPath
          );

          ZipFile.ExtractToDirectory(languagePackPath, this.hostingEnvironment.ContentRootPath + "\\Resources\\");
          System.IO.File.Delete(languagePackPath);
        }
      }

      return this.Ok();
    }
  }
}