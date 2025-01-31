using System;
using System.Collections.Generic;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Adliance.AspNetCore.Buddy.Testing;

public interface IFixtureOptions
{
    WebAppOptions WebApp { get; }
    IWaitForContainerOS? WebAppWaitStrategy { get; }
    string WebAppNetworkAlias { get; }
    string? ContentRootPath { get; }
    string? DockerFileDirectory { get; }
    string? DockerFileName { get; }
    Dictionary<string, string?> WebAppConfiguration { get; }
    Action<IServiceCollection>? ConfigureWebAppServices { get; }
    Action<IServiceCollection>? ConfigureWebAppTestServices { get; }

    PlaywrightOptions Playwright { get; }
    DbOptions Db { get; }
    IWaitForContainerOS? DbWaitStrategy { get; }
    string? DbConnectionStringConfigurationKey { get; }
}

public class DefaultFixtureOptions : IFixtureOptions
{
    public virtual WebAppOptions WebApp => WebAppOptions.InProcess;
    public string WebAppNetworkAlias => "webapp";
    public virtual string? ContentRootPath => null;
    public virtual string? DockerFileDirectory => CommonDirectoryPath.GetSolutionDirectory().DirectoryPath;
    public virtual string DockerFileName => "dockerfile";
    public virtual Dictionary<string, string?> WebAppConfiguration { get; } = new();
    public virtual Action<IServiceCollection>? ConfigureWebAppServices => null;
    public virtual Action<IServiceCollection>? ConfigureWebAppTestServices => null;
    public virtual PlaywrightOptions Playwright => PlaywrightOptions.None;
    public virtual DbOptions Db => DbOptions.None;
    public virtual IWaitForContainerOS? DbWaitStrategy => null;
    public virtual string DbConnectionStringConfigurationKey => "";
    public virtual IWaitForContainerOS WebAppWaitStrategy => Wait.ForUnixContainer().UntilPortIsAvailable(80);
}

public enum WebAppOptions
{
    InProcess,
    InContainer
}

public enum DbOptions
{
    None,
    UseSqlServer
}

public enum PlaywrightOptions
{
    None,
    Headless,
    Headed
}
