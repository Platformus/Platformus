// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Magicalizer.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Platformus.Core.Api.Constants;
using Platformus.Core.Api.Dto;
using Platformus.Core.Api.Services;
using Platformus.Core.Api.Services.Abstractions;

namespace Platformus.Core.Api.Extensions;

public static class ServiceCollectionExtensions
{
  public static void AddPlatformusCoreApi(this IServiceCollection services)
  {
    services.AddPlatformusCoreApi(null);
  }

  public static void AddPlatformusCoreApi(this IServiceCollection services, Action<AuthorizationOptions>? configureAuthorization)
  {
    AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Platformus.Core.Data.Entities"));
    AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Platformus.Core.Domain.Models"));
    AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Platformus.Core.Api.Dto"));
    services.AddScoped<IConfigurationReader, ConfigurationReader>();
    services.AddScoped<IFileManager, FileManager>();
    services.AddScoped<IAccessTokenService, EmailAndPasswordAccessTokenService>();
    services.AddScoped<IAccessTokenService, RefreshTokenAccessTokenService>();
    services.AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();
    services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddAuthentication(options => {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options => {
      AccessTokenGeneratorOptions accessTokenGeneratorOptions = services.BuildServiceProvider().GetRequiredService<IOptions<AccessTokenGeneratorOptions>>().Value;

      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidIssuer = accessTokenGeneratorOptions.Issuer,
        ValidAudience = accessTokenGeneratorOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenGeneratorOptions.ServerKey!))
      };
    });

    services.AddAuthorization(options => {
      options.AddCrudPolicies<User>(Permissions.AccessView, Permissions.AccessManage);
      options.AddRPolicies<CredentialType>(Permissions.AccessView, Permissions.AccessManage);
      options.AddCrudPolicies<Credential>(Permissions.AccessView, Permissions.AccessManage);
      options.AddCrudPolicies<Role>(Permissions.AccessView, Permissions.AccessManage);
      options.AddCrudPolicies<UserRole>(Permissions.AccessView, Permissions.AccessManage);
      options.AddRPolicies<Permission>(Permissions.AccessView, Permissions.AccessManage);
      options.AddCrudPolicies<RolePermission>(Permissions.AccessView, Permissions.AccessManage);
      options.AddCrudPolicies<Culture>(Permissions.ConfigurationView, Permissions.ConfigurationManage);
      options.AddCrudPolicies<Configuration>(Permissions.ConfigurationView, Permissions.ConfigurationManage);
      options.AddCrudPolicies<Variable>(Permissions.ConfigurationView, Permissions.ConfigurationManage);

      if (configureAuthorization != null)
        configureAuthorization(options);
    });
  }

  public static void AddPlatformusApi(this IServiceCollection services)
  {
    services.AddMagicalizer();
  }
}