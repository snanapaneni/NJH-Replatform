// Copyright (c) NJH. All rights reserved.

namespace Njh.Mvc;

using Autofac.Extensions.DependencyInjection;

/// <summary>
/// Defines the entry point for the application.
/// </summary>
public static class Program
{
    /// <summary>
    /// Implements the entry point for the application.
    /// </summary>
    /// <param name="args">
    /// The command line arguments.
    /// </param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    /// <summary>
    /// Creates a host builder.
    /// </summary>
    /// <param name="args">
    /// The command line arguments.
    /// </param>
    /// <returns>
    /// A host builder instance.
    /// </returns>
    public static IHostBuilder CreateHostBuilder(
        string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)

            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}