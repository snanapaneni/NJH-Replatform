// Copyright (c) NJH. All rights reserved.

namespace Njh.Mvc;

using Autofac;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ReasonOne.AspNetCore.Mvc.ViewComponents;
using ReasonOne.KenticoXperience;
using ReasonOne.KenticoXperience.DependencyInjection;
using ReasonOne.KenticoXperience.Services;

/// <summary>
/// Configures the application startup.
/// </summary>
public class Startup
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="Startup"/> class.
    /// </summary>
    /// <param name="environment">
    /// The webhost environment.
    /// </param>
    public Startup(
        IWebHostEnvironment environment)
    {
        this.Environment = environment;
    }

    /// <summary>
    /// Gets the webhost environment.
    /// </summary>
    public IWebHostEnvironment Environment { get; }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="services">
    /// The service collection.
    /// </param>
    /// <remarks>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// Visit https://go.microsoft.com/fwlink/?LinkID=398940
    /// for more information on how to configure your application.
    /// </remarks>
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.

        // Enable desired Kentico Xperience features
        var kenticoServiceCollection = services.AddKentico(features =>
        {
            features.UsePageBuilder(
                new PageBuilderOptions()
                {
                    // TODO: Set up the page builder options
                });

            // features.UseActivityTracking();
            // features.UseABTesting();
            // features.UseWebAnalytics();
            // features.UseEmailTracking();
            // features.UseCampaignLogger();
            // features.UseScheduler();
            features.UsePageRouting();
        });

        // Enables upport for the administration and live site applications
        // to be hosted on separate domains.
        kenticoServiceCollection.SetAdminCookiesSameSiteNone();
        if (Environment.IsDevelopment())
        {
            // By default, Xperience sends cookies using SameSite=Lax. If the administration and live site applications
            // are hosted on separate domains, this ensures cookies are set with SameSite=None and Secure. The configuration
            // only applies when communicating with the Xperience administration via preview links. Both applications also need 
            // to use a secure connection (HTTPS) to ensure cookies are not rejected by the client.
            kenticoServiceCollection.SetAdminCookiesSameSiteNone();

            // By default, Xperience requires a secure connection (HTTPS) if administration and live site applications
            // are hosted on separate domains. This configuration simplifies the initial setup of the development
            // or evaluation environment without a the need for secure connection. The system ignores authentication
            // cookies and this information is taken from the URL.
            kenticoServiceCollection.DisableVirtualContextSecurityForLocalhost();
        }

        services.AddAuthentication();

        // services.AddAuthorization();
        services.AddControllersWithViews();
        services
               .AddRazorPages()
               .AddRazorRuntimeCompilation();

        services.AddKenticoExtensions();

        // Registers the view component error visibility
        // based on Edit Mode from Kentico
        services.AddScoped<IViewComponentErrorVisibility, EditModeViewComponentErrorVisibility>();

        services.AddHealthChecks();
    }

    /// <summary>
    /// Configures the application HTTP request pipeline.
    /// </summary>
    /// <param name="app">
    /// The application builder.
    /// </param>
    /// <remarks>
    /// This method gets called by the runtime.
    /// Use this method to configure the HTTP request pipeline.
    /// </remarks>
    public void Configure(IApplicationBuilder app)
    {
        // Configure the HTTP request pipeline.
        if (!this.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            // The default HSTS value is 30 days.
            // You may want to change this for production scenarios,
            // see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseKentico();

        //app.UseRouting();

        app.UseCookiePolicy();

        app.UseCors();

        app.UseAuthentication();

        // app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            //endpoints.MapHealthChecks("/health/live");

            endpoints.Kentico().MapRoutes();

            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("The site has not been configured yet.");
            });
        });
    }

    /// <summary>
    /// Configures the Autofac container.
    /// </summary>
    /// <param name="builder">
    /// The container builder.
    /// </param>
    /// <remarks>
    /// This method is automatic called by the startup pipeline.
    /// </remarks>
    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterAssemblies(
            "RO",
            "Njh",
            "CMSApp");

    }
}